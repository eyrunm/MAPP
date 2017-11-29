using DM.MovieApi;

namespace MovieSearch
{
    /// <summary>
    /// <para>Interface consumers must implement to acccess any of the API's exposed against themoviedb.org.</para>
    /// <para>The concrete implementation can be used with <see cref="DM.MovieApi.MovieDbFactory"/> to register your specific settings.</para>
    /// <para>Alternatively, you can use MEF to expose your settings and import as needed. See our online documentation for more information.</para>
    /// </summary>
    public class MovieDbSettings : IMovieDbSettings
    {
        string IMovieDbSettings.ApiKey => "9d034c7b8556a700f3151ae3345b5c94";

        string IMovieDbSettings.ApiUrl => "http://api.themoviedb.org/3/";
    }
}