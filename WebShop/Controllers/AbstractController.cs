﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public abstract class AbstractController : Controller
    {
        private readonly IAccountService _accountService;

        protected AbstractController(IAccountService service)
        {
            this._accountService = service;
        }

        protected ApplicationUser GetUser()
        {
            return _accountService.GetAccount(User.Identity.GetUserId());
        }
    }
}