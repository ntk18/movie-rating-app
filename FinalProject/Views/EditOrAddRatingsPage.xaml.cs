using FinalProject.ViewModels;
namespace FinalProject.Views;

public partial class EditOrAddRatingsPage : ContentPage
{
    EditOrAddRatingsViewModel _viewModel;

    public EditOrAddRatingsPage(EditOrAddRatingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    private async void cancelBtn_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "Record Not Modified", "Ok");
        await Navigation.PopModalAsync();
    }

    private async void saveBtn_Clicked(object sender, EventArgs e)
    {
        if (notesEntry.Text.Length == 0)
        {
            await DisplayAlert("Alert", "Please enter a note", "Ok");
            return;
        }
        MovieDetails movFromVM = _viewModel.Movie;
        MovieDetails newMovie = new MovieDetails { Id = movFromVM.Id, Overview = movFromVM.Overview, Title = movFromVM.Title, Notes = notesEntry.Text, Rating = Double.Parse(ratingLabel.Text), LastUpdated = DateTime.Now};
        DB.conn.InsertOrReplace(newMovie);
        await DisplayAlert("Alert", "Rating and notes saved", "Ok");
        await Navigation.PopModalAsync();
    }

    private void IncrementButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.MovieRating < 5.0)
        {
            _viewModel.MovieRating += 0.25;
            UpdateRatingLabel();
        }
    }

    private void DecrementButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.MovieRating > 0.0)
        {
            _viewModel.MovieRating -= 0.25;
            UpdateRatingLabel();
        }
    }

    private void UpdateRatingLabel()
    {
        ratingLabel.Text = _viewModel.MovieRating.ToString("N2");
    }

}