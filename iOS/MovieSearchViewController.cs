using System;
using System.Collections.Generic;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieSearchViewController : UIViewController
    {
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

        public MovieSearchViewController(){
            // RegisterSettings only needs to be called one time when your application starts-up.
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
        }

            public override void ViewDidLoad()
            {
                base.ViewDidLoad();

                this.Title = "Movie search";

                this.View.BackgroundColor = UIColor.White;

                var titleLabel = CreateTitleLabel();

                var searchField = CreateSearchField();

                var getMovie = CreateGetMovieButton();

                var loading = CreateLoading();
                
                this.View.AddSubviews(new UIView[] { titleLabel, searchField, getMovie, loading});
                

                getMovie.TouchUpInside += async (sender, args) =>
                {
                    searchField.ResignFirstResponder();
                  
                    loading.StartAnimating();

                    var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

                    string m = searchField.Text;
                    List<string> movieTitles = new List<string>();
                    if (m == "")
                    {
                        loading.StopAnimating();
                        this.NavigationController.PushViewController(new MovieListController(movieTitles), true);
                    }
                    else
                    {
                        ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(m);

                        foreach (MovieInfo info in response.Results)
                        {
                            movieTitles.Add(info.Title);
                        }
                        
                        movieTitles.Sort();
                        loading.StopAnimating();
                        this.NavigationController.PushViewController(new MovieListController(movieTitles), true);
                    }
                };
            }

        UILabel CreateTitleLabel(){
            var titleLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: "
            };
            return titleLabel;
        }
        UITextField CreateSearchField(){
            var searchField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
            return searchField;
        }

        UIActivityIndicatorView CreateLoading(){
            var i = new UIActivityIndicatorView();
            i.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge;
            i.Frame = new System.Drawing.RectangleF((float)StartX, (float)(StartY + 3 * Height), (float)(this.View.Bounds.Width - 2 * StartX), (float)Height);
            i.Color = UIColor.Gray;
            return i;
        }

        UIButton CreateGetMovieButton(){
            var getMovie = UIButton.FromType(UIButtonType.RoundedRect);
            getMovie.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            getMovie.SetTitle("Get Movies", UIControlState.Normal);
            return getMovie;
        }
    }
}
