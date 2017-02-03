using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BidAuction.Models
{
    public class Auction
    {
        [Key]
        [Required, StringLength(maximumLength: 128)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name="Image")]
        public string ImagePath { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double StartingPrice { get; set; }

        public bool IsComplated { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<AuctionResult> AuctionResults { get; set; }

        //public virtual ICollection<AuctionRoom> AuctionRooms { get; set; }
    }

    public class AuctionResult
    {
        [Key]
        [Required, StringLength(maximumLength: 128)]
        public string Id { get; set; }
        public double FinalPrice { get; set; }
        public string UserId { get; set; }
        public string AuctionId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; }
    }

    public class AuctionRoom
    {
        [Key]
        [Required, StringLength(maximumLength: 128)]
        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ApplicationUser User { get; set; }
        //[ForeignKey("AuctionId")]
        //public virtual Auction Auction { get; set; }

    }

    public class Bid
    {
        [Key]
        [Required, StringLength(maximumLength: 128)]
        public string Id { get; set; }

        public double Amount { get; set; }
        public DateTime TimeStamp { get; set; }

        public string UserId { get; set; }
        public string AuctionId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; }
    }

}