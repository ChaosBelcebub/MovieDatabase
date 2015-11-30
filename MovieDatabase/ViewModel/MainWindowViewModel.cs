using MovieDatabase.Model;
using MovieDatabase.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFLocalizeExtension.Engine;

namespace MovieDatabase.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private MovieList moviesP;
        private Movie selectedMovieP;
        private Database db;
        private bool isMovieSelectedP;
        private bool isPathNotEmptyP;
        private String searchP;

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
                    if (selectedMovieP != null)
                    {
                        isMovieSelected = true;
                        isPathNotEmpty = !string.IsNullOrEmpty(selectedMovie.location);
                    }
                    else
                    {
                        isMovieSelected = false;
                    }
                }
            }
        }

        public bool isMovieSelected
        {
            get
            {
                return isMovieSelectedP;
            }

            set
            {
                if (value != isMovieSelectedP)
                {
                    isMovieSelectedP = value;
                    RaisePropertyChanged("isMovieSelected");
                }
            }
        }

        public bool isPathNotEmpty
        {
            get
            {
                return isPathNotEmptyP;
            }

            set
            {
                if (value != isPathNotEmptyP)
                {
                    isPathNotEmptyP = value;
                    RaisePropertyChanged("isPathNotEmpty");
                }
            }
        }

        public String search
        {
            get
            {
                return searchP;
            }

            set
            {
                if (value != searchP)
                {
                    searchP = value;
                    RaisePropertyChanged("search");
                    this.movies = this.db.content(searchP);
                }
            }
        }

        public MainWindowViewModel()
        {
            this.db = new Database();
            this.moviesP = new MovieList();
            this.loadDatabase();
            this.isMovieSelected = false;
            this.isPathNotEmpty = false;
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo("de-DE");
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

        private void openFile()
        {
            if (selectedMovieP == null)
            {
                displayError("No movie selected!");
                return;
            }
            try
            {
                System.Diagnostics.Process.Start(selectedMovie.location);
            }
            catch (Win32Exception)
            {
                displayError("Path not found!");
                return;
            }
            catch
            {
                displayError("Unknown error occured!");
                return;
            }
            selectedMovie.watched = true;
            db.edit(selectedMovie);
        }

        private ICommand _openFileCommand;

        public ICommand openFileCommand
        {
            get
            {
                if (this._openFileCommand == null)
                {
                    this._openFileCommand = new RelayCommand(param => this.openFile());
                }
                return this._openFileCommand;
            }
        }

        private void openEdit()
        {
            if (selectedMovieP == null)
            {
                displayError("No movie selected!");
                return;
            }
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

        private void deleteMovie()
        {
            if (selectedMovieP == null)
            {
                displayError("No movie selected!");
                return;
            }
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

        private void toggleWatched()
        {
            selectedMovie.watched = !selectedMovie.watched;
            db.edit(selectedMovie);
        }

        private ICommand _toggleWatchedCommand;

        public ICommand toggleWatchedCommand
        {
            get
            {
                if (this._toggleWatchedCommand == null)
                {
                    this._toggleWatchedCommand = new RelayCommand(param => this.toggleWatched());
                }
                return this._toggleWatchedCommand;
            }
        }

        private void displayError(String message)
        {
            MessageBox.Show(message, "Error");
        }
    }
}
