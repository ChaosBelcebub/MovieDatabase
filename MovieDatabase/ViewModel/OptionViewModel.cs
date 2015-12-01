using MovieDatabase.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFLocalizeExtension.Engine;

namespace MovieDatabase.ViewModel
{
    class OptionViewModel : BaseViewModel
    {
        private int selectedLanguageP;
        private UserPreferences userPrefs;
        private ICommand saveCommandP;

        public event EventHandler OnRequestClose;

        public OptionViewModel()
        {
            userPrefs = new UserPreferences();

            this.selectedLanguage = userPrefs.Language;
        }

        public int selectedLanguage
        {
            get
            {
                return selectedLanguageP;
            }

            set
            {
                if (value != selectedLanguageP)
                {
                    selectedLanguageP = value;
                    RaisePropertyChanged("selectedLanguage");
                }
            }
        }

        /// <summary>
        /// ICommand saveCommand. Calls the save() method
        /// </summary>
        public ICommand saveCommand
        {
            get
            {
                if (this.saveCommandP == null)
                {
                    this.saveCommandP = new RelayCommand(param => this.save());
                }
                return this.saveCommandP;
            }
        }

        private void save()
        {
            userPrefs.Language = selectedLanguageP;

            userPrefs.Save();
            OnRequestClose(this, new EventArgs());
        }
    }
}
