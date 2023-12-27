using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject.ViewModels
{
    public class EditOrAddRatingsViewModel : INotifyPropertyChanged
    {

        public EditOrAddRatingsViewModel(MovieDetails movie)
        {
            MovieTitle = movie.Title;
            Movie = movie;
            GetSavedData();
        }

        private MovieDetails _movie;
        private string _movieTitle;
        private double _movieRating;
        private string _movieNotes;

        public MovieDetails Movie
        {
            get { return _movie; }
            set
            {
                if (_movie != value)
                {
                    _movie = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MovieTitle
        {
            get { return _movieTitle; }
            set
            {
                if (_movieTitle != value)
                {
                    _movieTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public double MovieRating
        {
            get { return _movieRating; }
            set
            {
                if (_movieRating != value)
                {
                    _movieRating = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MovieNotes
        {
            get { return _movieNotes; }
            set
            {
                if (_movieNotes != value)
                {
                    _movieNotes = value;
                    OnPropertyChanged();
                }
            }
        }

        private void GetSavedData()
        {
            if (Movie.HasNotes)
            {
                MovieNotes = Movie.Notes;
                MovieRating = Movie.Rating;
            } else
            {
                //Default values for binding
                MovieNotes = "";
                MovieRating = 2.50;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
