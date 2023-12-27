using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Input;

namespace FinalProject.ViewModels;
public class HomeViewModel : INotifyPropertyChanged
{
    private Dictionary<string, ObservableCollection<Movie>> _movieResults;

    public Dictionary<string, ObservableCollection<Movie>> MovieResults
    {
        get { return _movieResults; }
        set
        {
            _movieResults = value;
            OnPropertyChanged(nameof(MovieResults));
        }
    }

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

                LoadMovies(value);
            }
        }
    }

    public HomeViewModel()
    {
        PickerItems = new List<string>
        {
            "Now Playing",
            "Popular",
            "Top Rated",
            "Upcoming",
        };

        SelectedPickerItem = PickerItems[0];

        MovieResults = new Dictionary<string, ObservableCollection<Movie>>();

        LoadMovies(SelectedPickerItem);

        SearchQuery = "";
        SearchCommand = new Command(SearchMovies);
    }

    public async void LoadMovies(string selectedPickerItem)
    {
        string apiLink = GetApiLinkForSelectedPickerItem(selectedPickerItem);
        if (MovieResults == null)
        {
            // Initialize the dictionary if not initialized
            MovieResults = new Dictionary<string, ObservableCollection<Movie>>();
        }

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(apiLink);

        var content = await response.Content.ReadAsStringAsync();

        var movieResponse = JsonConvert.DeserializeObject<Result>(content);

        foreach (var movie in movieResponse.Results)
        {
            movie.ReleaseDate = convertDate(movie.ReleaseDate);
        }

        var movies = new ObservableCollection<Movie>(movieResponse.Results);

        // Store the result in the dictionary
        MovieResults[apiLink] = movies;

        Movies = movies;
    }

    private string convertDate(string date)
    {
        if (DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            // Format the date as mm/dd/yy
            return parsedDate.ToString("MM/dd/yy");
        }
        return "";
    }

    private string GetApiLinkForSelectedPickerItem(string selectedItem)
    {
        switch (selectedItem)
        {
            case "Popular":
                return Constants.PopularLink;
            case "Top Rated":
                return Constants.TopRatedLink;
            case "Upcoming":
                return Constants.UpcomingLink;
            default:
                return Constants.NowPlayingLink; // Default to Now Playing if no match
        }
    }

    private string searchQuery;

    public string SearchQuery
    {
        get { return searchQuery; }
        set
        {
            if (searchQuery != value)
            {
                searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }
    }

    public async void SearchMovies()
    {
        if (SearchQuery.Length < 1)
        {
            return;
        }
        
        var apiLink = Constants.SearchLink + SearchQuery + $"&api_key={Constants.APIKey}";

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(apiLink);

        var content = await response.Content.ReadAsStringAsync();

        var movieResponse = JsonConvert.DeserializeObject<Result>(content);
        foreach (var movie in movieResponse.Results)
        {
            movie.ReleaseDate = convertDate(movie.ReleaseDate);
        }
        var movies = new ObservableCollection<Movie>(movieResponse.Results);

        // Store the result in the dictionary
        MovieResults[apiLink] = movies;

        Movies = movies;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private ObservableCollection<Movie> _movies;

    public ObservableCollection<Movie> Movies
    {
        get { return _movies; }
        set
        {
            _movies = value;
            OnPropertyChanged(nameof(Movies));
        }
    }

    private void OnPageAppearing(object sender, EventArgs e)
    {
        LoadMovies(SelectedPickerItem);
    }

    public ICommand SearchCommand { get; set; }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
