using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanLap.Web.Controllers
{
    public class BusinessController : BaseController
    {
        // GET: Business
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Requirement()
        {
            return View();
        }

    }
}