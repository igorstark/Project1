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

namespace MovizManager.ViewModels
{
    class ViewModelMain : ViewModelBase
    {

        #region Fields
        private ViewModelBase _CurrentViewModel;
        private ViewModelMovies _ViewModelMovies;
        private ViewModelOneMovie _ViewModelOneMovie;
        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetAndNotify("CurrentViewModel", ref _CurrentViewModel, value); }
        }

        public ViewModelMovies ViewModelMovies
        {
            get { return _ViewModelMovies; }
            set { SetAndNotify("ViewModelMovies", ref _ViewModelMovies, value); }
        }

        public ViewModelOneMovie ViewModelOneMovie
        {
            get { return _ViewModelOneMovie; }
            set { SetAndNotify("ViewModelOneMovie", ref _ViewModelOneMovie, value); }
        }

        #endregion


        #region Constructors
        public ViewModelMain()
        {
            ViewModelMovies = new ViewModelMovies();
            ViewModelMovies.PropertyChanged += ViewModelMovies_PropertyChanged;
            // Choix du ViewModel initial
            CurrentViewModel = ViewModelMovies;

        }


        #endregion

        #region PropertyChanged
        void ViewModelMovies_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "GoNextSelected":
                    ViewModelOneMovie = new ViewModelOneMovie(this.ViewModelMovies.SelectedMovie); 
                    ViewModelOneMovie.PropertyChanged += ViewModelOneMovie_PropertyChanged;
                    CurrentViewModel = ViewModelOneMovie;

                    break;

                case "AddedFilm": // On teste la modif d'une propriété...
                    ViewModelOneMovie = new ViewModelOneMovie(this.ViewModelMovies.AddedFilm);
                    ViewModelOneMovie.PropertyChanged += ViewModelOneMovie_PropertyChanged;
                    CurrentViewModel = ViewModelOneMovie;
                    break;

                default:
                    break;
            }
        }

        void ViewModelOneMovie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //CurrentViewModel = ViewModelMovies;
            switch (e.PropertyName)
            {
                case "OKButtonWasClicked":
                    CurrentViewModel = ViewModelMovies;
                    break;

                case "CancelButtonWasClicked":
                    CurrentViewModel = ViewModelMovies;
                    break;

                default:
                    break;
            }
        }

        #endregion

    }
}
