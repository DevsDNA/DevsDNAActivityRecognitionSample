[assembly: Xamarin.Forms.Dependency(typeof(SampleActivityRecognition.Services.NavigationService.NavigationService))]
namespace SampleActivityRecognition.Services.NavigationService
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// Navigation service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:SampleActivityRecognition.Services.NavigationService.NavigationService"/> class.
        /// </summary>
        public NavigationService()
        {
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void NavigateTo<T>() where T : Page
        {
            T page = Activator.CreateInstance<T>();
            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);
        }
    }
}
