using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aukro.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public decimal PriceAct { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string LastBidder { get; set; }

        public byte[] ImageData { get; set; }

    }
}
