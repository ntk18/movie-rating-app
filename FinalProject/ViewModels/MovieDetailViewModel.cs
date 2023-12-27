using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Input;

namespace FinalProject.ViewModels;
public class MovieDetailViewModel : INotifyPropertyChanged
{
    int movieId;
    MovieDetails _movie;

    public MovieDetailViewModel(int movieId)
    {
        MovieId = movieId;
        GetMovieDetails(movieId);
    }

    private string _movieTitle;
    private string _movieOverview;
    private string _movieStatus;
    private string _posterPath;
    private string _releaseDate;

    private List<Genre> _movieGenres;
    private string _genreNames;
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

    public string MovieOverview
    {
        get { return _movieOverview; }
        set
        {
            if (_movieOverview != value)
            {
                _movieOverview = value;
                OnPropertyChanged();
            }
        }
    }

    public string MovieStatus
    {
        get { return _movieStatus; }
        set
        {
            if (_movieStatus != value)
            {
                _movieStatus = value;
                OnPropertyChanged();
            }
        }
    }
    public string PosterPath
    {
        get { return _posterPath; }
        set
        {
            if (_posterPath != value)
            {
                _posterPath = value;
                OnPropertyChanged();
            }
        }
    }

    public string ReleaseDate
    {
        get { return _releaseDate; }
        set
        {
            if (_releaseDate != value)
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }
    }

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

    public List<Genre> MovieGenres
    {
        get { return _movieGenres; }
        set
        {
            if (_movieGenres != value)
            {
                _movieGenres = value;
                OnPropertyChanged();
                UpdateGenreNames();
            }
        }
    }

    public string GenreNames
    {
        get { return _genreNames; }
        set
        {
            if (_genreNames != value)
            {
                _genreNames = value;
                OnPropertyChanged();
            }
        }
    }

    public int MovieId
    {
        get
        {
            return movieId;
        }
        set
        {
            if (movieId != value)
            {
                movieId = value;
                OnPropertyChanged();
            }
        }
    }

    private void UpdateGenreNames()
    {
        GenreNames = string.Join(", ", MovieGenres?.Select(genre => genre.Name));
    }

    private async void GetMovieDetails(int movieId)
    {
        var httpClient = new HttpClient();
        string apiLink = Constants.MovieDetailLink + movieId.ToString() + $"?api_key={Constants.APIKey}";
        var response = await httpClient.GetAsync(apiLink);

        var content = await response.Content.ReadAsStringAsync();

        var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(content);
        MovieDetails movie = movieDetails;

        Movie = movie;
        MovieTitle = movieDetails?.Title;
        MovieOverview = movieDetails?.Overview;
        MovieStatus = movieDetails?.Status;
        MovieGenres = movieDetails?.Genres;
        PosterPath = Constants.PosterPrefix + movieDetails?.Poster;
        ReleaseDate = convertDate(movieDetails?.ReleaseDate);
    }

    /*
     * Convert API date format of YYYY-MM-DD to US standard MM/DD/YYYY 
     */
    private string convertDate(string date)
    {
        if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            // Format the date as mm/dd/yy
            return parsedDate.ToString("MM/dd/yy");
        }
        return "";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
