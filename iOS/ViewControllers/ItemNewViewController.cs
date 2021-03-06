﻿using OnMenu.Models.Items;
using System;

using UIKit;

namespace OnMenu.iOS
{
    public partial class ItemNewViewController : UIViewController
    {
        public IngredientsViewModel ViewModel { get; set; }

        public ItemNewViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnSaveItem.TouchUpInside += (sender, e) =>
            {
                /*var item = new Item
                {
                    Text = txtTitle.Text,
                    Description = txtDesc.Text
                };
                ViewModel.AddIngredientsCommand.Execute(item);
                NavigationController.PopToRootViewController(true);*/
            };
        }
    }
}
