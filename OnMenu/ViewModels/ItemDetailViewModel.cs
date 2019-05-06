using System;
using OnMenu.Models;

namespace OnMenu
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            if (item != null)
            {
                Title = item.Name;
                Item = item;
            }
        }
    }
}