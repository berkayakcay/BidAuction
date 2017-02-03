using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using BidAuction.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;


namespace BidAuction.Controllers
{
    [Authorize]
    public class AuctionController : BaseController
    {

        /// <summary>
        /// Get all auctions
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var auctions = db.Auctions.ToList();

            if (!auctions.Any())
            {
                Information("There is no open auction!", false);
            }

            return View(auctions);
        }


        public ActionResult Details(string id)
        {
            if (!id.IsNullOrWhiteSpace())
            {
                var auction = db.Auctions.FirstOrDefault(x => x.Id == id);

                return View(auction);
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<JsonResult> GetAllBids(string id)
        {
            
            if (!id.IsNullOrWhiteSpace())
            {
                var bids = await db.Bids.Where(w => w.AuctionId == id).Select(s => new
                {
                    Id = s.Id,
                    TimeStamp = s.TimeStamp,
                    Amount = s.Amount
                    
                }).OrderByDescending(o => o.TimeStamp).ToListAsync();

                return Json(bids, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult OwnAuctions()
        {
            
            var auctions = db.Auctions.Where(x => x.UserId == CurrentUserId).ToList();
            
            return View(auctions);
        }
    }
}