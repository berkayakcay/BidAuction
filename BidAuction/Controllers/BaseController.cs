using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BidAuction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BidAuction.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db;

        protected string CurrentUserId { get; set; } = string.Empty;

        public BaseController()
        {

            db = new ApplicationDbContext();

        }


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUserId = requestContext.HttpContext.User.Identity.GetUserId();
            }
        }


        #region notifications
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertHelper.AlertStyles.Success, message, dismissable);
        }
        public void Information(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertHelper.AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertHelper.AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(Helpers.AlertHelper.AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Helpers.AlertHelper.Alert.TempDataKey)
                ? (List<Helpers.AlertHelper.Alert>)TempData[Helpers.AlertHelper.Alert.TempDataKey]
                : new List<Helpers.AlertHelper.Alert>();

            alerts.Add(new Helpers.AlertHelper.Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Helpers.AlertHelper.Alert.TempDataKey] = alerts;
        }
        #endregion

    }
}