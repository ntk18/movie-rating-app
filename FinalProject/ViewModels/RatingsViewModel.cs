using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject.ViewModels
{
    public class RatingsViewModel : INotifyPropertyChanged
    {
        private List<string> pickerItems;
        private string selectedPickerItem;

        public List<string> PickerItems
        {
            get { return pickerItems; }
            set
            {
                if (pickerItems != value)
                {
                    pickerItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedPickerItem
        {
            get { return selectedPickerItem; }
            set
            {
                if (selectedPickerItem != value)
                {
                    selectedPickerItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<MovieDetails> _movieList;

        public List<MovieDetails> MovieList
        {
            get { return _movieList; }
            set
            {
                if (_movieList != value)
                {
                    _movieList = value;
                    OnPropertyChanged(nameof(MovieList));
                }
            }
        }

        public RatingsViewModel()
        {
            PickerItems = new List<string>
        {
            "Last Updated",
            "Title",
            "Rating"
        };

            SelectedPickerItem = PickerItems[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
