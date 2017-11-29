using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListDataSource : UITableViewSource
    {
        private List<string> _movieTitles;

        public readonly NSString MovieTitlesCellID = new NSString("MovieListCell");

        public MovieListDataSource(List<string> movieTitles)
        {
            this._movieTitles = movieTitles;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell((NSString)this.MovieTitlesCellID);
            if(cell == null){ //we are doing the first cell
                cell = new UITableViewCell(UITableViewCellStyle.Default, this.MovieTitlesCellID);
            }

            cell.TextLabel.Text = this._movieTitles[indexPath.Row];

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movieTitles.Count;
        }
    }
}
