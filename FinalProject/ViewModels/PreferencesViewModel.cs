using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject.ViewModels
{
    public class PreferencesViewModel : INotifyPropertyChanged
    {
        private string welcomeMessage;

        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set
            {
                if (welcomeMessage != value)
                {
                    welcomeMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public PreferencesViewModel()
        {
            WelcomeMessage = "Welcome to .NET MAUI!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
