using FinalProject.Themes;

namespace FinalProject
{
    public partial class App : Application
    {
        public AppTheme currentTheme;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            SetTheme(GetSavedTheme());

            Application.Current.RequestedThemeChanged += (s, a) => {
                SetTheme(a.RequestedTheme);
            };

            DB.OpenConnection();
        }

        private AppTheme GetSavedTheme()
        {
            return Preferences.Get("LightMode", true) ? AppTheme.Light : AppTheme.Dark;
        }

        public void SetTheme(AppTheme theme)
        {
            App app = (App)App.Current;
            ICollection<ResourceDictionary> mergedDictionaries = app.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (theme == AppTheme.Light)
                {
                    mergedDictionaries.Add(new LightTheme());
                    currentTheme = AppTheme.Light;
                }
                else
                {
                    mergedDictionaries.Add(new DarkTheme());
                    currentTheme = AppTheme.Dark;
                }
            }
        }
    }
}
