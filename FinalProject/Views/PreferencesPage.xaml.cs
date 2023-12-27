namespace FinalProject.Views;

public partial class PreferencesPage : ContentPage
{
	public PreferencesPage(AppTheme theme)
    {
		InitializeComponent();

        if (!Preferences.ContainsKey("LightMode"))
            Preferences.Set("LightMode", true);
        lightDark.IsToggled = Preferences.Get("LightMode", false);

        if (!Preferences.ContainsKey("DetailedLists"))
            Preferences.Set("DetailedLists", false);
        detailedLists.IsToggled = Preferences.Get("DetailedLists", false);
    }

    private void lightDark_Toggled(object sender, ToggledEventArgs e)
    {
        App app = (App)App.Current;
        app.SetTheme(lightDark.IsToggled ? AppTheme.Light : AppTheme.Dark);
        Preferences.Set("LightMode", lightDark.IsToggled);
    }

    private void detailedLists_Toggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("DetailedLists", detailedLists.IsToggled);
    }

    private async void backBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}