using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Model
{
    class Database
    {
        public Database()
        {
            this.initializeDatabase();
        }

        private void initializeDatabase()
        {
            if (!File.Exists("Movie.db"))
            {
                SQLiteConnection.CreateFile("Movie.db");
                string sql = "create table movies (name varchar(40) not null, director varchar(40) not null, location varchar(40) not null);";
                SQLiteCommand command = new SQLiteCommand(sql, DatabaseConnection.instance);
                command.ExecuteNonQuery();
            }
        }

        public MovieList content()
        {
            MovieList importedMovies = new MovieList();

            string sql = "select rowid, name, director, location from movies order by name asc;";
            SQLiteCommand command = new SQLiteCommand(sql, DatabaseConnection.instance);
            SQLiteDataReader reader = command.ExecuteReader();
            Movie movie;

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["rowid"]);
                string name = Convert.ToString(reader["name"]);
                string director = Convert.ToString(reader["director"]);
                string location = Convert.ToString(reader["location"]);

                movie = new Movie { name = name, director = director, location = location };
                movie.setID(id);
                importedMovies.Add(movie);
            }

            return importedMovies;
        }

        public void add(Movie movie)
        {
            string sql = "insert into movies values ('" + movie.name + "', '" + movie.director + "', '" + movie.location + "');";
            SQLiteCommand command = new SQLiteCommand(sql, DatabaseConnection.instance);
            command.ExecuteNonQuery();
        }
    }
}
