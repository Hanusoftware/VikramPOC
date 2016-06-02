using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class blobrestcController : Controller
    {
        ConnectionString ConnectionObj = new ConnectionString();
        // GET: blobrestc
        public ActionResult Index()
        {
            return View();
        }

        
        #region Upload blob Through REST API
        public ActionResult BlobByRestAPI()
        {
            ViewBag.Message = "Your app description page.";

            //Set the expiry time and permissions for the container.
            //In this case no start time is specified, so the shared access signature becomes valid immediately.
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Write;

            //Generate the shared access signature on the container, setting the constraints directly on the signature.
            string sasContainerToken = ConnectionObj.Container.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            string s = ConnectionObj.Container.Uri + sasContainerToken;

            ServiceProperties sp = ConnectionObj.BlobClient.GetServiceProperties();
            sp.DefaultServiceVersion = "2013-08-15";
            CorsRule cr = new CorsRule();
            cr.AllowedHeaders.Add("*");
            cr.AllowedMethods = CorsHttpMethods.Get | CorsHttpMethods.Put | CorsHttpMethods.Post;
            cr.AllowedOrigins.Add("http://localhost:60819");
            cr.ExposedHeaders.Add("x-ms-*");
            cr.MaxAgeInSeconds = 5;
            sp.Cors.CorsRules.Clear();
            sp.Cors.CorsRules.Add(cr);
            ConnectionObj.BlobClient.SetServiceProperties(sp);
            ViewBag.SASurl = s;
            return View();
        }
        #endregion
    }
}