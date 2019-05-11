using System;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu
{
    public class App
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "http://localhost:5000";

        public static void Initialize()
        {
            if (UseMockDataStore)
                ServiceLocator.Instance.Register<IDataStore<Ingredient>, IngredientDataStore>();
            else
                ServiceLocator.Instance.Register<IDataStore<Ingredient>, CloudDataStore>();
        }
    }
}
