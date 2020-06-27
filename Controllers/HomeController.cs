using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Razorpay.Api;
namespace RazorPayIntegration.Controllers
{
    public class HomeController : Controller
    {
        static string key = "Paste Your Key";
        static string secret = "Paste Your Secret";
        RazorpayClient client = new RazorpayClient(key, secret);
        // [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            return View();
        }

        public string CreateOrder()
        {
            string result = string.Empty;
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", 1000); // amount in the smallest currency unit
                options.Add("receipt", "Testing_order" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_"));
                options.Add("currency", "INR");
                options.Add("payment_capture", "0");
                Order order = client.Order.Create(options);
                if (order != null)
                {
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(order.Attributes);
                }
                else
                {
                    result = "Failed to create order!";
                }
            }
            catch (Exception ex)
            {
                //log exception
                result = ex.Message;
            }
            return result;
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