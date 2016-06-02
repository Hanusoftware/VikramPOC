using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class blobcController : Controller
    {
        ConnectionString ConnectionObj = new ConnectionString();
        // GET: blobc
        public ActionResult Index()
        {
            return View();
        }
        #region Upload -Download Document
        public ActionResult UploadBlob()
        {
            BlobModel mdl = null;
            List<BlobModel> lstmdl = new List<BlobModel>();
                foreach (IListBlobItem item in ConnectionObj.Container.ListBlobs(null, false))
                {
                    mdl = new BlobModel();
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        ViewBag.BlobFile = blob.Uri;
                        mdl.DocUrl = blob.Uri.ToString();
                        mdl.FileName = blob.Name;
                        lstmdl.Add(mdl);
                        mdl.Urls = lstmdl;
                    }
                }
            return View(lstmdl);
        }

        [HttpPost]
        public ActionResult UploadDoc()
        {
            ConnectionObj.Container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    CloudBlockBlob blockBlob = ConnectionObj.Container.GetBlockBlobReference(fileName);
                    blockBlob.UploadFromStream(file.InputStream);
                }
            }

            return RedirectToAction("UploadBlob");
        }
        public FileResult DownloadBlob(string docfile, string docname)
        {
            string contentType = string.Empty;
            if (docname.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }

            else if (docname.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else
            {
                contentType = MimeMapping.GetMimeMapping(docfile); ;

            }
            HttpWebRequest aRequest = (HttpWebRequest)WebRequest.Create(docfile);
            HttpWebResponse aResponse = (HttpWebResponse)aRequest.GetResponse();
            return File(aResponse.GetResponseStream(), contentType, docname);
        }
        #endregion
    }
}