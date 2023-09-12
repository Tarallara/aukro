
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aukro.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Collection<Item> Items { get; set; }
    }
}
