using System;
using System.Collections.Generic;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
        private readonly List<string> _movieTitles;

        public MovieListController(List<string> movieTitles)
        {
            this._movieTitles = movieTitles;
        }

        public override void ViewDidLoad(){

            base.ViewDidLoad();

            this.Title = "Movie List";

            this.TableView.Source = new MovieListDataSource(this._movieTitles);

        }
    
    }
}
