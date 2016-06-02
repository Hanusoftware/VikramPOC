using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class rediscController : Controller
    {
        // GET: redisc
        public ActionResult Index()
        {
            return View();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("VPSredis.redis.cache.windows.net:6380,password=W7d39VsSjkd9fBF14r0pC0WodqFo/XEgLntQh6cyEsk=,ssl=True,abortConnect=False");
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        //[HttpPost]
        public ActionResult RedishCache(RedisCacheModel Data)
        {
            IDatabase cache = Connection.GetDatabase();
            if (Connection.IsConnected)
            {
                try
                {
                    var serializedRedisCacheModel = JsonConvert.SerializeObject(Data);
                    cache.StringSet("Key", serializedRedisCacheModel, TimeSpan.FromMinutes(100));
                }
                catch (Exception ex) { }
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult DisplayRedisCache()
        {
            IDatabase cache = Connection.GetDatabase();
            var RedisObj = JsonConvert.DeserializeObject<RedisCacheModel>(cache.StringGet("Key"));
            return View("RedishCache", RedisObj);
        }
    }
}