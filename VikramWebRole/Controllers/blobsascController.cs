using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class blobsascController : Controller
    {
        ConnectionString ConnectionObj = new ConnectionString();
        // GET: blobsasc
        public ActionResult Index()
        {
            return View();
        }
        #region Shared Access Signature Implementation
        public ActionResult UploadBlobBySAS()
        {
            BlobModel mdl = null;
            List<BlobModel> lstmdl = new List<BlobModel>();
            foreach (IListBlobItem item in ConnectionObj.PContainer.ListBlobs(null, false))
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
        public ActionResult UploadBlobSign()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    CloudBlockBlob blockBlob = ConnectionObj.PContainer.GetBlockBlobReference(fileName);

                    blockBlob.UploadFromStream(file.InputStream);
                }
            }
            return RedirectToAction("UploadBlobBySAS");
        }
        public ActionResult DownloadDocShared(string docfile, string docname)
        {
            string reslink = string.Empty;
            string tmpval = Convert.ToString(TempData["docfile"]);
            if (tmpval == docfile)
            {
                reslink = TempData["link"].ToString();
            }
            else
            {
                reslink = GetBlobSasUri(ConnectionObj.PContainer, docname);
                TempData["docfile"] = docfile;
                TempData["link"] = reslink;
            }
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
                contentType = MimeMapping.GetMimeMapping(docfile);
            }
            return Redirect(reslink);
        }
        static string GetBlobSasUri(CloudBlobContainer Pcontainer, string filename)
        {
            CloudBlockBlob blob = Pcontainer.GetBlockBlobReference(filename);
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddSeconds(30);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);
            return blob.Uri + sasBlobToken;
        }
        #endregion
    }
}