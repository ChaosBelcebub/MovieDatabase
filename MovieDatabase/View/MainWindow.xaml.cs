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
using MovieDatabase.Model;
using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace MovieDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] language = new string[2] { "en", "de-DE"};
        public MainWindow()
        {
            InitializeComponent();

            var userPrefs = new UserPreferences();

            this.Height = userPrefs.WindowHeight;
            this.Width = userPrefs.WindowWidth;
            this.Top = userPrefs.WindowTop;
            this.Left = userPrefs.WindowLeft;
            this.WindowState = userPrefs.WindowState;
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(language[userPrefs.Language]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var userPrefs = new UserPreferences();

            userPrefs.WindowHeight = this.Height;
            userPrefs.WindowWidth = this.Width;
            userPrefs.WindowTop = this.Top;
            userPrefs.WindowLeft = this.Left;
            userPrefs.WindowState = this.WindowState;

            userPrefs.Save();
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

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            var vm = new OptionViewModel();
            var win = new OptionView { DataContext = vm };
            vm.OnRequestClose += (s, ev) => win.Close();

            win.ShowDialog();

            var userPrefs = new UserPreferences();
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(language[userPrefs.Language]);
            var viewModel = DataContext as MainWindowViewModel;
            viewModel.loadDatabase();
        }
    }
}
