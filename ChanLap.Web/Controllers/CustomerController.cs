﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanLap.Web.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}