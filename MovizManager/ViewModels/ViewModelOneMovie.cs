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

namespace MovizManager.ViewModels
{
    class ViewModelOneMovie : ViewModelBase
    {

        #region tests

        private ObservableCollection<string> _GoPrevious;
        public ObservableCollection<string> GoPrevious
        {
            get { return _GoPrevious; }
            private set { SetAndNotify("GoPrevious", ref _GoPrevious, value); }
        }

        private bool _ButtonWasClicked;
        public bool ButtonWasClicked
        {
            get { return _ButtonWasClicked; }
            set { SetAndNotify("ButtonWasClicked", ref _ButtonWasClicked, value); }
        }

        #endregion

        #region Fields

        // Film à ajouter
        private Movie _Movie;
        
        // Liste de genres
        private Genres _LesGenres;

        // Liste de notes
        private ObservableCollection<int> _Ratings;

        #region Commands
        // Commande de retour à la liste des films
        private DelegateCommand _ReturnCommand;

        // Commande de sélection de film
        private DelegateCommand _SelectCommand;


        public DelegateCommand ReturnCommand
        {
            get { return _ReturnCommand; }
            private set { SetAndNotify("ReturnCommand", ref _ReturnCommand, value); }
        }

        public DelegateCommand SelectCommand
        {
            get { return _SelectCommand; }
            set { SetAndNotify("SelectCommand", ref _SelectCommand, value); }
        }
        #endregion


        #endregion

        #region Properties

        public Movie Movie
        {
            get { return _Movie; }
            set { SetAndNotify("Movie", ref _Movie, value); }
        }

        public Model.Genres LesGenres
        {
            get { return _LesGenres; }
            set { SetAndNotify("LesGenres", ref _LesGenres, value); }
        }

        public ObservableCollection<int> Ratings
        {
            get { return _Ratings; }
            set { SetAndNotify("Ratings", ref _Ratings, value); }
        }
        
        #endregion

        #region Constructor
        public ViewModelOneMovie(Movie M)
        {
            this._ReturnCommand = new DelegateCommand(ExecuteReturnCommand, CanExecuteReturnCommand);
            this._SelectCommand = new DelegateCommand(ExecuteSelectCommand, CanExecuteSelectCommand);
            
            // Liste des genres
            LesGenres = new Genres();

            // Liste de notes
            Ratings = new ObservableCollection<int> { 0, 1, 2, 3, 4, 5 };

            this._Movie = M;
            if (this._Movie == null)
            {
                this._Movie = new Movie();
                // this._Movie.Age = 0;
            }
            
        }

        #endregion

        #region Methods
        #region Commands
        private void ExecuteReturnCommand(object parameter)
        {
            this.ButtonWasClicked = true;
        }

        public bool CanExecuteReturnCommand(object parameter)
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
