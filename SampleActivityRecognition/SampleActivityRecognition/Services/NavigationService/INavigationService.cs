namespace SampleActivityRecognition.Services.NavigationService
{
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// Navigation service.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        void NavigateTo<T>() where T : Page;
    }
}
