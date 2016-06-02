using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class servicebuscController : Controller
    {
        string ServiceBusConStr = ConfigurationManager.AppSettings["ConStr"].ToString();
        // GET: servicebusc
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ServiceBus(ServiceBusQueueModel model)
        {
            ServiceBusQueueModel objq = new ServiceBusQueueModel();
            objq.qlist = GetMsg();
            return View(objq);
        }

        [HttpPost]
        public ActionResult ServiceBus(ServiceBusQueueModel model, string servisebus)
        {

            if (servisebus == "Create")
            {
                string QueueName = ConfigurationManager.AppSettings["sbqueue"].ToString();
                var namespaceManager = NamespaceManager.CreateFromConnectionString(ServiceBusConStr);

                if (!namespaceManager.QueueExists(QueueName))
                {
                    namespaceManager.CreateQueue(QueueName);
                }
                QueueClient Client = QueueClient.CreateFromConnectionString(ServiceBusConStr, QueueName);
                BrokeredMessage message = new BrokeredMessage("ServiceBusQ-Body");
                message.Properties["Msg"] = model.Message;

                Client.Send(message);
            }
            ServiceBusQueueModel objq = new ServiceBusQueueModel();
            return RedirectToAction("ServiceBus");
        }

        public List<ServiceBusQueueModel> GetMsg()
        {
            List<ServiceBusQueueModel> objlist = new List<ServiceBusQueueModel>();
            ServiceBusQueueModel objq = null;
            while (true)
            {
                try
                {
                    QueueClient Client = QueueClient.CreateFromConnectionString(ServiceBusConStr, ConfigurationManager.AppSettings["sbqueue"].ToString());
                    OnMessageOptions options = new OnMessageOptions();
                    options.AutoComplete = false;
                    options.AutoRenewTimeout = TimeSpan.FromMinutes(1);
                    BrokeredMessage msg = null;

                    msg = Client.Receive(TimeSpan.FromSeconds(5));
                    if (msg != null)
                    {
                        objq = new ServiceBusQueueModel();
                        objq.Message = "Message Number:" + msg.SequenceNumber + " Message :" + msg.Properties["Msg"];
                        objlist.Add(objq);
                        //msg.Complete();
                    }
                    else
                    {
                        break;
                    }
                }
                catch { break; }
            }
            return objlist;
        }
    }
}