using PitchOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchOnline
{
    /// <summary>
    /// Locates view models from the IoC for use in binding in Xaml files
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Properties

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        public static SettingsViewModel SettingsViewModel => IoC.Get<SettingsViewModel>();
        public static LobbiesListViewModel LobbiesListViewModel => IoC.Get<LobbiesListViewModel>();
        public static CreateLobbyViewModel CreateLobbyViewModel => IoC.Get<CreateLobbyViewModel>();

        #endregion
    }
}
