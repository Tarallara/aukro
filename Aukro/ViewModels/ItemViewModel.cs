using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aukro.Models;

namespace Aukro.ViewModels
{
    class ItemViewModel
    {
        public List<Item> Items { get; set; }
        public ItemViewModel(List<Item> items)
        {
            Items = items;
        }
    }
}
