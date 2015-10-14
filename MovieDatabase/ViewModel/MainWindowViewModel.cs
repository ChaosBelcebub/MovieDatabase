using MovieDatabase.Model;
using MovieDatabase.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieDatabase.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private MovieList moviesP;
        private Movie selectedMovieP;
        private Database db;

        public MovieList movies
        {
            get
            {
                return moviesP;
            }

            set
            {
                if (value != moviesP)
                {
                    moviesP = value;
                    RaisePropertyChanged("movies");
                }
            }
        }

        public Movie selectedMovie
        {
            get
            {
                return selectedMovieP;
            }

            set
            {
                if (value != selectedMovieP)
                {
                    selectedMovieP = value;
                    RaisePropertyChanged("selectedMovie");
                }
            }
        }

        public MainWindowViewModel()
        {
            this.db = new Database();
            this.moviesP = new MovieList();
            this.loadDatabase();
        }

        public void loadDatabase()
        {
            this.movies = this.db.content();
        }

        private ICommand _loadCommand;

        public ICommand loadDatabaseCommand
        {
            get
            {
                if (this._loadCommand == null)
                {
                    this._loadCommand = new RelayCommand(param => this.loadDatabase());
                }
                return this._loadCommand;
            }
        }

        public void openEdit()
        {
            var vm = new EditMovieViewModel(selectedMovieP);
            var win = new EditMovieView { DataContext = vm };
            vm.OnRequestClose += (s, ev) => win.Close();

            win.ShowDialog();
            this.loadDatabase();
        }

        private ICommand _openEditCommand;

        public ICommand openEditCommand
        {
            get
            {
                if (this._openEditCommand == null)
                {
                    this._openEditCommand = new RelayCommand(param => this.openEdit());
                }
                return this._openEditCommand;
            }
        }

        public void deleteMovie()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Remove '" + selectedMovieP.name + "'?", "Remove Movie", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                this.db.delete(selectedMovieP);
                this.loadDatabase();
            } 
        }

        private ICommand _deleteMovieCommand;

        public ICommand deleteMovieCommand
        {
            get
            {
                if (this._deleteMovieCommand == null)
                {
                    this._deleteMovieCommand = new RelayCommand(param => this.deleteMovie());
                }
                return this._deleteMovieCommand;
            }
        }
    }
}
