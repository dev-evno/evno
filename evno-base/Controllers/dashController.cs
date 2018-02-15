using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Linq;
using System.Data.SqlClient;
using MVC01.Code;
using System;

namespace MVC01.Controllers
{
    public class dashController : Controller
    {
        GolbalDBAccess GDBA = new GolbalDBAccess();
        // GET: dash
        public ActionResult Dash()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LeftNavs()
        {
            return GDBA.getNavs();
        }
    }
}