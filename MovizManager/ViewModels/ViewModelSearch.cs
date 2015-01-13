using MovizManager.Model;
using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Windows.Data;

namespace MovizManager.ViewModels
{
    class ViewModelSearch : ViewModelBase
    {

        #region tests

        private bool _OKButtonWasClicked;
        public bool OKButtonWasClicked
        {
            get { return _OKButtonWasClicked; }
            set { SetAndNotify("OKButtonWasClicked", ref _OKButtonWasClicked, value); }
        }


        private bool _CancelButtonWasClicked;
        public bool CancelButtonWasClicked
        {
            get { return _CancelButtonWasClicked; }
            set { SetAndNotify("CancelButtonWasClicked", ref _CancelButtonWasClicked, value); }
        }


        #endregion

        #region Fields


        // DataStore
        private DataStore _Storage;

        // Film
        private Movie _Movie;
        
        // Liste de genres
        private Genres _Kinds;

        // Liste de notes
        private ObservableCollection<int> _Ratings;

        private string _SearchText;

        #region Commands
        // Commande de retour à la liste des films
        private DelegateCommand _ReturnCommand;

        // Commande d'annulation d'ajout de film
        private DelegateCommand _CancelCommand;

        // Commande de sélection de film
        private DelegateCommand _SelectCommand;


        public DelegateCommand ReturnCommand
        {
            get { return _ReturnCommand; }
            private set { SetAndNotify("ReturnCommand", ref _ReturnCommand, value); }
        }


        public DelegateCommand CancelCommand
        {
            get { return _CancelCommand; }
            private set { SetAndNotify("CancelCommand", ref _CancelCommand, value); }
        }

        public DelegateCommand SelectCommand
        {
            get { return _SelectCommand; }
            set { SetAndNotify("SelectCommand", ref _SelectCommand, value); }
        }
        #endregion


        #endregion

        #region Properties

        public DataStore Storage
        {
            get { return _Storage; }
            set { SetAndNotify("Storage", ref _Storage, value); }
        }

        public Movie Movie
        {
            get { return _Movie; }
            set { SetAndNotify("Movie", ref _Movie, value); }
        }

        public Genres Kinds
        {
            get { return _Kinds; }
            set { SetAndNotify("Kinds", ref _Kinds, value); }
        }

        public ObservableCollection<int> Ratings
        {
            get { return _Ratings; }
            set { SetAndNotify("Ratings", ref _Ratings, value); }
        }
        
        public string SearchText
        {
            get { return _SearchText; }
            set { SetAndNotify("SearchText", ref _SearchText, value); }
        }


        #endregion

        #region Constructor
        public ViewModelSearch(DataStore DataStorage, Movie M)
        {
            this._ReturnCommand = new DelegateCommand(ExecuteReturnCommand, CanExecuteReturnCommand);
            this._CancelCommand = new DelegateCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            this._SelectCommand = new DelegateCommand(ExecuteSelectCommand, CanExecuteSelectCommand);
            
            // Liste des genres
            Kinds = new Genres();

            // Liste de notes
            Ratings = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5 };

            this._Storage = new DataStore();
            this._Storage.ListeDeMecs = DataStorage.ListeDeMecs;

            this._Movie = M;

            
        }

        #endregion

        #region Methods

        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);

            switch (property)
            {
                case "SearchText":
                    // Filtrer la collection
                    CollectionViewSource.GetDefaultView(Storage.ListeDeMecs).Filter =
                        new Predicate<object>(o => FiltrerListe(o));
                    break;
                default:
                    break;
            }
        }

        private bool FiltrerListe(object o)
        {
            Movie movie = o as Movie;

            if (movie != null)
            {
                return movie.MovieTitle.ToLowerInvariant().Contains(SearchText.ToLowerInvariant());
            }

            return false;
        }


        #region Commands
        private void ExecuteReturnCommand(object parameter)
        {
            
            this.OKButtonWasClicked = true;
        }

        public bool CanExecuteReturnCommand(object parameter)
        {
            return true;
        }


        private void ExecuteCancelCommand(object parameter)
        {
            this._Movie = null;
            this.CancelButtonWasClicked = true;
        }

        public bool CanExecuteCancelCommand(object parameter)
        {
            return true;
        }



        private void ExecuteSelectCommand(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Séquences vidéo (.avi)|*.avi|Séquences vidéo (.mov)|.mov|Séquences vidéo (.mkv)|(.mkv)|Séquences vidéo (.mp4)|.mp4";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                bool? userClickedOK = openFileDialog.ShowDialog();

                if (userClickedOK == true)
                {
                    Movie.Nom = openFileDialog.FileName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ooops, une erreur est survenue.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public bool CanExecuteSelectCommand(object parameter)
        {
            return true;
        }

        #endregion
        #endregion

    }
}
