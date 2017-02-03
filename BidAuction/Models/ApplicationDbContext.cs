using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BidAuction.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionResult> AuctionResults { get; set; }
        public DbSet<AuctionRoom> AuctionRooms { get; set; }
        public DbSet<Bid> Bids { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}