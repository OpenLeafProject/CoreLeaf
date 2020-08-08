using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebPush;

namespace Leaf.Controllers
{
    [Route("api/v0.1/[controller]")]
    [ApiController]
    public class webPushController : ControllerBase
    {
        private readonly ILogger<webPushController> _logger;
        private readonly IConfiguration _config;
        private readonly webPushController _context;

        public webPushController(webPushController context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

       /*
       [HttpPost]
       [Route("send")]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Send(int id)
       {

           var payload = Request.Form["payload"];
           var device = await _context.Devices.SingleOrDefaultAsync(m => m.Id == id);

           string vapidPublicKey = _configuration.GetSection("VapidKeys")["PublicKey"];
           string vapidPrivateKey = _configuration.GetSection("VapidKeys")["PrivateKey"];

           var pushSubscription = new PushSubscription(device.PushEndpoint, device.PushP256DH, device.PushAuth);
           var vapidDetails = new VapidDetails("mailto:example@example.com", vapidPublicKey, vapidPrivateKey);

           var webPushClient = new WebPushClient();
           webPushClient.SendNotification(pushSubscription, payload, vapidDetails);

           return View();
           
    }

    [HttpPost]
        [Route("subscribe")]
        public void StoreSubscription(string[] publicKey, string[] auth, string notificationEndPoint)
        {
            /*
            var pushEndpoint = notificationEndPoint;
            var pushAuth = auth[0].ToString();
            var pushP256DH = publicKey[0].ToString();

            var subject = "mailTo:hhhhhhh@gmail.com";
            var uPublicKey = "yyzzxx";
            var privateKey = "xxyyzz";

            System.IO.File.WriteAllText(@"\Desktop\Subscription.txt", pushEndpoint);
            var subscription = new PushSubscription(pushEndpoint, pushP256DH, pushAuth);
            var gcmAPIKey = "AAAA";
            var vapidDetails = new VapidDetails(subject, uPublicKey, privateKey);

            var webPushClient = new WebPushClient();

            try
            {
                webPushClient.SetGCMAPIKey(gcmAPIKey);
                webPushClient.SendNotification(subscription, "payload", gcmAPIKey);

            }
            catch (WebPushException exception)
            {
                Console.WriteLine("HTTP status Code:" + exception.StatusCode);
            }

        }

        public IActionResult GenerateKeys()
        {
            var keys = VapidHelper.GenerateVapidKeys();
            ViewBag.PublicKey = keys.PublicKey;
            ViewBag.PrivateKey = keys.PrivateKey;
            return View();
        }
        */
    }
}
