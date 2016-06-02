using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class queuecController : Controller
    {
        ConnectionString ConnectionObj = new ConnectionString();
        // GET: queuec
        public ActionResult Index()
        {
            return View();
        }

        #region Queue Implementation
        public ActionResult CreateQueue()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QueuePost(QueueModel qm)
        {
            CloudQueueMessage message = new CloudQueueMessage(qm.Message);
            ConnectionObj.Queue.AddMessage(message);
            qm.Message = string.Empty;


            CloudQueueMessage peekedMessage = ConnectionObj.Queue.PeekMessage();
            if (peekedMessage != null)
                ViewData["msg"] = peekedMessage.AsString;

            return RedirectToAction("CreateQueue");
        }
        public FileResult DownloadQueueFile()
        {
            ConnectionString Con = new ConnectionString();
            string appendblob = "https://vikramastoragedb.blob.core.windows.net/vikramcontainer/VikramQueue.txt";
            string contentType = MimeMapping.GetMimeMapping(appendblob);
            HttpWebRequest aRequest = (HttpWebRequest)WebRequest.Create(appendblob);
            HttpWebResponse aResponse = (HttpWebResponse)aRequest.GetResponse();
            return File(aResponse.GetResponseStream(), contentType, "VikramQueue.txt");
        }
        #endregion
    }
}