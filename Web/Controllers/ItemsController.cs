﻿using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;
using Web.Models;
using Web.Persistence;
using System.Linq;

namespace Web.Controllers
{
    [ActAsRestfulie]
    public class ItemsController : Controller
    {
 
        private static IList<IBehaveAsResource> List(IEnumerable<Item> items)
        {
            var list = new List<IBehaveAsResource>();
            foreach(var item in items)
            {
                list.Add(item);
            }
            return list;
        }

        public ActionResult Index()
        {
            var database = new MemoryDatabase();
            return new Success(List(database.List()));
        }

        public ActionResult Get(int id)
        {
            var database = new MemoryDatabase();
            var item = database.List().Where(i => i.Id == id).SingleOrDefault();

            if (item == null) return new NotFound();
            return new Success(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(Item item)
        {
            var database = new MemoryDatabase();
            database.Add(item);

            return new Created("http://localhost:1198/Items/" + item.Id);
        }
    }
}
