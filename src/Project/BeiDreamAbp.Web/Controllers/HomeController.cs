using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Abp.Web.Mvc.Controllers;
using BeiDreamAbp.Service.Users;
using BeiDreamAbp.Service.Users.Dtos;

namespace BeiDreamAbp.Web.Controllers
{
    public class HomeController : AbpController
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            //LocalizationSourceName = "SimpleTaskSystem";
            _userService = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var users = _userService.GetUsers(new GetUsersInput() { Name = "" });
            return View();
        }
    }
}