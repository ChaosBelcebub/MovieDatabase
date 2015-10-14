using MovieDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieDatabase.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private MovieList moviesP;
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
    }
}
