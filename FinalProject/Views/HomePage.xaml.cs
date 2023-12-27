using FinalProject.ViewModels;
namespace FinalProject.Views;

public partial class HomePage : ContentPage
{
    private HomeViewModel _viewModel;
    public HomePage()
	{
		InitializeComponent();
        _viewModel = new HomeViewModel(); // Initialize viewModel
        BindingContext = _viewModel;

        _viewModel.SelectedPickerItem = "Now Playing";
    }

    private async void preferencesBtn_Clicked(object sender, EventArgs e)
    {
        App a = (App)Application.Current;
        PreferencesPage preferencesPage = new PreferencesPage(a.currentTheme);
		await Navigation.PushModalAsync(preferencesPage);
        _viewModel.LoadMovies(_viewModel.SelectedPickerItem);
    }

    private async void lv_Movies_ItemSelectedOrTapped(object sender, EventArgs e)
    {
        if (sender is ListView listView)
        {
            if (listView.SelectedItem != null && listView.SelectedItem is Movie selectedMovie)
            {
                int movieId = selectedMovie.Id;
                MovieDetailViewModel detailViewModel = new MovieDetailViewModel(movieId);
                MovieDetailPage newPage = new MovieDetailPage(detailViewModel);
                await Navigation.PushModalAsync(newPage);
            }

            // Clear the selection
            listView.SelectedItem = null;
        }
    }
}