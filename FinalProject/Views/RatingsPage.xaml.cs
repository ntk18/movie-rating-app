using FinalProject.ViewModels;
using System.Linq;

namespace FinalProject.Views;

public partial class RatingsPage : ContentPage
{
    private RatingsViewModel _viewModel;

    public RatingsPage()
    {
        InitializeComponent();
        _viewModel = new RatingsViewModel();
        PopulateList("Last Updated");
        BindingContext = _viewModel;
        _viewModel.SelectedPickerItem = "Last Updated";

        this.Appearing += OnPageAppearing;
    }

    private void PopulateList(string sortingMethod)
    {
        _viewModel.MovieList = DB.conn.Table<MovieDetails>().ToList();

        switch (sortingMethod)
        {
            case "Title":
                _viewModel.MovieList = DB.conn.Table<MovieDetails>().OrderBy(m => m.Title).ToList();
                break;
            case "Rating":
                _viewModel.MovieList = DB.conn.Table<MovieDetails>().OrderByDescending(m => m.Rating).ToList();
                break;
            default:
                _viewModel.MovieList = DB.conn.Table<MovieDetails>().OrderByDescending(m => m.LastUpdated).ToList(); // Default to Last Updated if no match
                break;
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateList(_viewModel.SelectedPickerItem);
    }

    private void OnPageAppearing(object sender, EventArgs e)
    {
        PopulateList(_viewModel.SelectedPickerItem);
    }

    private async void viewBtn_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;

        if (clickedButton.BindingContext is MovieDetails selectedMovie)
        {
            int movieId = selectedMovie.Id;
            MovieDetailViewModel detailViewModel = new MovieDetailViewModel(movieId);
            MovieDetailPage newPage = new MovieDetailPage(detailViewModel);
            await Navigation.PushModalAsync(newPage);
        }
    }

    private async void EditBtn_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;

        if (clickedButton.BindingContext is MovieDetails selectedMovie)
        {
            EditOrAddRatingsViewModel newViewModel = new EditOrAddRatingsViewModel(selectedMovie);
            EditOrAddRatingsPage newPage = new EditOrAddRatingsPage(newViewModel);
            await Navigation.PushModalAsync(newPage);
        }
    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;

        if (clickedButton.BindingContext is MovieDetails selectedMovie)
        {
            bool deleteConfirmation = await DisplayAlert("Warning", "Do you wish to delete this record?", "Yes", "No");
            if (deleteConfirmation)
            {
                int v = DB.conn.Delete(selectedMovie);
                if (v > 0)
                {
                    PopulateList(_viewModel.SelectedPickerItem);
                }
                await DisplayAlert("Alert", "Record deleted", "Ok");
                return;
            }
            await DisplayAlert("Alert", "Record Not Deleted", "Ok");
        }
    }
}
