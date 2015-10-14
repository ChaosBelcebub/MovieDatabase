using MovieDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieDatabase.ViewModel
{
    class EditMovieViewModel : BaseViewModel
    {
        // private varibles for displayed input
        private int id;
        private string nameP;
        private string directorP;
        private string locationP;

        private ICommand editCommandP;
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
        /// ICommand addCommand. Calls the addmember() method
        /// </summary>
        public ICommand editCommand
        {
            get
            {
                if (this.editCommandP == null)
                {
                    this.editCommandP = new RelayCommand(param => this.editMovie());
                }
                return this.editCommandP;
            }
        }

        private void editMovie()
        {
            Movie movie = new Movie { name = name, director = director, location = location };
            movie.setID(id);
            db.edit(movie);
            OnRequestClose(this, new EventArgs());
        }

        /// <summary>
        /// Initializes a new empty instance of the AddMovieViewModel class
        /// </summary>
        public EditMovieViewModel()
        {
            db = new Database();

            name = string.Empty;
            director = string.Empty;
            location = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the AddMovieViewModel class
        /// </summary>
        public EditMovieViewModel(Movie movie)
        {
            db = new Database();

            id = movie.getID();
            name = movie.name;
            director = movie.director;
            location = movie.location;
        }
    }
}
