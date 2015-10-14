using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Model
{
    class Movie : BaseModel
    {
        // private variables to hold information about a movie 
        int _id;
        string _name;
        string _director;
        string _location;

        // ID as public method. This should prevent the automatic displaying in the datagrid
        public void setID(int id)
        {
            this._id = id;
        }

        public int getID()
        {
            return this._id;
        }

        // The name of a movie
        public string name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        // The director
        public string director
        {
            get
            {
                return this._director;
            }

            set
            {
                if (this._director != value)
                {
                    this._director = value;
                    this.RaisePropertyChanged("Director");
                }
            }
        }

        // The location of the movie on the disk
        public string location
        {
            get
            {
                return this._location;
            }

            set
            {
                if (this._location != value)
                {
                    this._location = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
    }
}
