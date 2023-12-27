using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Popularity { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate
        {
            get; set;
        }

        public bool DetailedListPreference => Preferences.Get("DetailedLists", false);
    }

    public class Result
    {
        public List<Movie> Results { get; set; }
    }

    /** 
     * Fields above are for API calls for the Home Page
     * Fields below are for API calls for the Movie Details Page
     */
    public class MovieDetails
    {
        [PrimaryKey, Unique]
        public int Id { get; set; }
        [Ignore]
        public List<Genre> Genres { get; set; }
        public string GenreNames
        {
            get
            {
                if (Genres == null)
                {
                    return "";
                }

                return string.Join(", ", Genres?.Select(genre => genre.Name));
            }
        }
        public string Overview { get; set; }
        [JsonProperty("poster_path")]
        public string Poster { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Title { get; set; }

        public double Rating { get; set; }
        public string Notes { get; set; }
        public bool HasNotes => Notes != null;
        public DateTime LastUpdated { get; set; }
        [Ignore]
        public bool DetailedListPreference => Preferences.Get("DetailedLists", false);
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
