using FinalProject.ViewModels;
namespace FinalProject.Views;

public partial class MovieDetailPage : ContentPage
{
    MovieDetailViewModel _viewModel;
    public MovieDetailPage(MovieDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void ratingsBtn_Clicked(object sender, EventArgs e)
    {
        MovieDetails currentMovie;
        var movieInDatabase = DB.conn.Table<MovieDetails>().FirstOrDefault(m => m.Id == _viewModel.Movie.Id);
        if (movieInDatabase != null)
        {
            currentMovie = movieInDatabase;
        } else
        {
            currentMovie = _viewModel.Movie;
        }

        EditOrAddRatingsViewModel newViewModel = new EditOrAddRatingsViewModel(currentMovie);
        EditOrAddRatingsPage newPage = new EditOrAddRatingsPage(newViewModel);
        await Navigation.PushModalAsync(newPage);
    }
}