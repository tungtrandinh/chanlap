using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ChanLap.Infrastructure.Repository;
using ChanLap.Service;

namespace ChanLap.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IUserRepository repository;
        //private readonly IUserService service;
        //public HomeController(IUserRepository _repository, IUserService _service) {
        //    this.repository = _repository;
        //    this.service    = _service;
        //}

        public ActionResult Index()
        {
            //this.repository.GetAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}