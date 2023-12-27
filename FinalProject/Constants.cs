using FinalProject.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public static class Constants
    {
        public static string APIKey = GetAPIKey();
        
        public static string GetAPIKey()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<App>().Build();
            string key = configuration["APIKEY"];
            return key;		// Pulled out of your secrets.json
        }



        public static string SearchLink = "https://api.themoviedb.org/3/search/movie?query=";

        public static string NowPlayingLink = "https://api.themoviedb.org/3/movie/now_playing" + $"?api_key={APIKey}";
        public static string PopularLink = "https://api.themoviedb.org/3/movie/popular" + $"?api_key={APIKey}";
        public static string TopRatedLink = "https://api.themoviedb.org/3/movie/top_rated" + $"?api_key={APIKey}";
        public static string UpcomingLink = "https://api.themoviedb.org/3/movie/upcoming" + $"?api_key={APIKey}";

        public static string MovieDetailLink = "https://api.themoviedb.org/3/movie/";

        public static string PosterPrefix = "https://image.tmdb.org/t/p/w500";
    }
}
