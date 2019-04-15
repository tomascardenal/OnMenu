
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace OnMenu.Droid.Activities
{
    [Activity(Label = "AddIngredientActivity")]
    public class AddIngredientActivity : Activity
    {
        FloatingActionButton saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}

/* [Activity(Label = "AddItemActivity")]
    public class AddItemActivity : Activity
    {
        FloatingActionButton saveButton;
        EditText title, description;

        public ItemsViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = BrowseFragment.ViewModel;

            // Create your application here
            SetContentView(Resource.Layout.activity_add_item);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button);
            title = FindViewById<EditText>(Resource.Id.txtTitle);
            description = FindViewById<EditText>(Resource.Id.txtDesc);

            saveButton.Click += SaveButton_Click;
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            var item = new Item
            {
                Text = title.Text,
                Description = description.Text
            };
            ViewModel.AddItemCommand.Execute(item);

            Finish();
        }
    }*/
