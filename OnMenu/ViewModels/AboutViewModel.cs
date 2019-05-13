using System.Windows.Input;

namespace OnMenu
{
    /// <summary>
    /// Viewmodel for the about view
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        /// <summary>
        /// Instantiates a new viewmodel
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Plugin.Share.CrossShare.Current.OpenBrowser("https://xamarin.com/platform"));
        }

        /// <summary>
        /// Opens a web
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
