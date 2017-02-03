using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BidAuction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System.Collections;
using System.Security.Policy;
using System.Web.Mvc;

namespace BidAuction.Hubs
{
    [Microsoft.AspNet.SignalR.Authorize]
    public class AnnounceHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        
        private static ApplicationDbContext db = new ApplicationDbContext();

        public void Announce()
        {
            string name = Context.User.Identity.Name;
        }


        public void addBid(Bid bid)
        {
            bid.TimeStamp = DateTime.Now;
            bid.UserId = Context.User.Identity.GetUserId();
            db.Bids.Add(bid);
            db.SaveChanges();
            Clients.All.addBid(bid);
        }

        public override async Task OnConnected()
        {
            string clientId = Context.User.Identity.GetUserId();
       
            _connections.Add(clientId, Context.ConnectionId);

            try
            {
                //string clientId = Context.User.Identity.GetUserId();
                var auctionId = Context.QueryString["AuctionId"];

                AuctionRoom auctionRoom = new AuctionRoom();
                
                auctionRoom.Id = Guid.NewGuid().ToString();
                auctionRoom.AuctionId = auctionId.ToString();
                auctionRoom.UserId = clientId.ToString();

                db.AuctionRooms.Add(auctionRoom);
                db.SaveChanges();

                var userIds = db.AuctionRooms.Where(x => x.AuctionId == auctionId).Select(s => s.UserId).ToList();
                var users = db.Users.Where(x => userIds.Contains(x.Id)).Select(n => new { n.Id, n.UserName }).ToList();

                await Clients.All.announce(users);
            }
            catch (Exception e)
            {

                throw e;
            }

 
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            string clientId = Context.User.Identity.GetUserId();

            _connections.Remove(clientId, Context.ConnectionId);

            var auctionId = Context.QueryString["AuctionId"];

            db.AuctionRooms.RemoveRange(db.AuctionRooms.Where(x => x.UserId == clientId));
            db.SaveChanges();

            var userIds = db.AuctionRooms.Where(x => x.AuctionId == auctionId).Select(s => s.UserId).ToList();
            var users = db.Users.Where(x => userIds.Contains(x.Id)).Select(n => new { n.Id, n.UserName}).ToList();

            await Clients.All.announce(users);
            //return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string clientId = Context.User.Identity.GetUserId();

            if (!_connections.GetConnections(clientId).Contains(Context.ConnectionId))
            {
                _connections.Add(clientId, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

    }
}