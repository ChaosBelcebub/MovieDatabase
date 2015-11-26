using Microsoft.Win32;
using MovieDatabase.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieDatabase.View
{
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class EditMovieView : Window
    {
        public EditMovieView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.LocationTextBox.Text = openFileDialog.FileName;
                var viewModel = DataContext as EditMovieViewModel;
                viewModel.location = this.LocationTextBox.Text;
            }
        }
    }
}
