using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MovieDatabase.View;
using MovieDatabase.ViewModel;
using WPFLocalizeExtension.Extensions;
using System.Reflection;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            // Modify the header of the Name column.
            if (e.Column.Header.ToString() == "name")
            {
                e.Column.Header = GetLocalizedValue<string>("name");
            }
            else if (e.Column.Header.ToString() == "director")
            {
                e.Column.Header = GetLocalizedValue<string>("director");
            }
            else if (e.Column.Header.ToString() == "location")
            {
                e.Column.Header = GetLocalizedValue<string>("location");
            }
            else if (e.Column.Header.ToString() == "watched")
            {
                e.Column.Header = GetLocalizedValue<string>("watched");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = new AddMovieViewModel();
            var win = new AddMovieView { DataContext = vm };
            vm.OnRequestClose += (s, ev) => win.Close();
            
            win.ShowDialog();

            var viewModel = DataContext as MainWindowViewModel;
            viewModel.loadDatabase();
        }

        public static T GetLocalizedValue<T>(string key)
        {
            return LocExtension.GetLocalizedValue<T>(Assembly.GetCallingAssembly().GetName().Name + ":Resources:" + key);
        }
    }
}
