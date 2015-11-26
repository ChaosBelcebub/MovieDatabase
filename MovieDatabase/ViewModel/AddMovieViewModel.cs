using MovieDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieDatabase.ViewModel
{
    class AddMovieViewModel : BaseViewModel
    {
        // private varibles for displayed input
        private string nameP;
        private string directorP;
        private string locationP;
        private bool watchedP;

        private ICommand addCommandP;
        private Database db;

        public event EventHandler OnRequestClose;

        /// <summary>
        /// String name. Contains the name of the new movie
        /// </summary>
        public string name
        {
            get
            {
                return nameP;
            }

            set
            {
                if (nameP != value)
                {
                    nameP = value;
                    RaisePropertyChanged("name");
                }
            }
        }

        /// <summary>
        /// String director. Contains the director of the new movie
        /// </summary>
        public string director
        {
            get
            {
                return directorP;
            }

            set
            {
                if (directorP != value)
                {
                    directorP = value;
                    RaisePropertyChanged("director");
                }
            }
        }

        /// <summary>
        /// String name. Contains the name of the new movie
        /// </summary>
        public string location
        {
            get
            {
                return locationP;
            }

            set
            {
                if (locationP != value)
                {
                    locationP = value;
                    RaisePropertyChanged("location");
                }
            }
        }

        /// <summary>
        /// Bool watched. Contains the watched status of the movie
        /// </summary>
        public bool watched
        {
            get
            {
                return watchedP;
            }

            set
            {
                if (watchedP != value)
                {
                    watchedP = value;
                    RaisePropertyChanged("watched");
                }
            }
        }

        /// <summary>
        /// ICommand addCommand. Calls the addmember() method
        /// </summary>
        public ICommand addCommand
        {
            get
            {
                if (this.addCommandP == null)
                {
                    this.addCommandP = new RelayCommand(param => this.addMovie());
                }
                return this.addCommandP;
            }
        }

        private void addMovie()
        {
            Movie movie = new Movie { name = name, director = director, location = location, watched = watched };
            db.add(movie);
            OnRequestClose(this, new EventArgs());
        }

        /// <summary>
        /// Initializes a new instance of the AddMovieViewModel class
        /// </summary>
        public AddMovieViewModel()
        {
            db = new Database();

            name = string.Empty;
            director = string.Empty;
            location = string.Empty;
            watched = false;
        }
    }
}
