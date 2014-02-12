using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MelonReader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (Session["AccessToken"] != null)
                {
                    try
                    {
                        var accessToken = Session["AccessToken"].ToString();
                        var client = new FacebookClient(accessToken);
                        return View("Main");
                    }
                    catch(FacebookOAuthException)
                    {
                        Session.Abandon();
                        return View("LoggedInIndex");
                    }
                }
                else
                {
                    //how'd we get here?
                    string method = HttpContext.Request.HttpMethod;
                    if (method == "POST")
                    {
                        var accessToken = Request["accessToken"];
                        Session["AccessToken"] = accessToken;
                        if (Session["AccessToken"] != null)
                        {
                            return View("Main");
                        }
                        else
                        {
                            return View("LoggedInIndex");
                        }
                        //Response.Redirect("LoggedInIndex");
                        
                    }
                    else
                    {
                        return View("LoggedInIndex");
                    }
                }

                //return View("LoggedInIndex");
            }
            else
            {
                return View("Index");
            }
            //return View();
        }

        public void Friends()
        {
            var accessToken = Session["AccessToken"].ToString();
            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me/friends");
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(result);
            Response.End();
        }

        public void UserInfo()
        {
            var accessToken = Session["AccessToken"].ToString();
            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me");
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(result);
            Response.End();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}