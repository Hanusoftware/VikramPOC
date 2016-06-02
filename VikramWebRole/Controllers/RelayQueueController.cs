using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace VikramWebRole.Controllers
{
    public class RelayQueueController : Controller
    {
        // GET: RelayQueue
        public ActionResult Index()
        {
            return View();
        }

        //static ChannelFactory<IProductsChannel> channelFactory;
        //static ServiceRelayController()
        //{
        //    // Create shared secret token credentials for authentication.
        //    channelFactory = new ChannelFactory<IProductsChannel>(new NetTcpRelayBinding(),
        //        "sb://amitrelay.servicebus.windows.net/products");
        //    channelFactory.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior
        //    {
        //        TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(
        //            "RootManageSharedAccessKey", "tVOr/2tSHQmXtiAEiilbV4Tc4q+bAWyPADAdmJ0u/uA=")
        //    });
        //}
        //// GET: ServiceRelay
        //public ActionResult BusRelay()
        //{
        //    using (IProductsChannel channel = channelFactory.CreateChannel())
        //    {
        //        // Return a view of the products inventory.
        //        return this.View(from prod in channel.GetProducts()
        //                         select
        //                             new Product
        //                             {
        //                                 Id = prod.Id,
        //                                 Name = prod.Name,
        //                                 Quantity = prod.Quantity
        //                             });
        //    }
        //}
    }
}