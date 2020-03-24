using COVID19.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COVID19.Controllers
{
    public class BaseController : Controller
    {
        private Covid19Entities _covid19Entities;
        protected Covid19Entities COVID19Entities => _covid19Entities;

        public BaseController()
        {
            this._covid19Entities = new Covid19Entities();
        }
    }
}