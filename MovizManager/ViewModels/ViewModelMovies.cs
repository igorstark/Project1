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
using System.Windows.Data;
using System.Diagnostics;
using Microsoft.Win32;

namespace MovizManager.ViewModels
{
    class ViewModelMovies : ViewModelBase
    {
        #region tests

        #endregion

        #region Fields

        #region Commands
        // Commande pour quitter l'application
        private DelegateCommand _QuitCommand;
        // Commande pour sérialiser les données
        private DelegateCommand _SaveCommand;
        // Commande pour afficher la boite de dialogue A propos
        private DelegateCommand _AboutCommand;
        // Commande pour afficher l'ajout d'un Mec
        //private DelegateCommand _ShowAddCommand;
        // Commande pour ajouter un Mec
        private DelegateCommand _AddCommand;
        // Commande pour supprimer un Mec
        private DelegateCommand _DeleteCommand;
        // Commande pour trier la liste
        private DelegateCommand _SortCommand;
        // Commande pour passer à la vue suivante
        private DelegateCommand _GoNextCommand;
        // Commande pour lancer la lecture
        private DelegateCommand _PlayCommand;
        // Commande pour permettre la modification des propriétés d'un film
        private DelegateCommand _ModifyCommand;
        // Commande de sélection de film
        private DelegateCommand _SelectCommand;


        #endregion
        // Le DataStore
        private Model.DataStore _DataStore;
        
        // Un Mec, nécessaire pour le binding avec les champs du XAML
        
        private Model.Movie _UnMec;

        // Liste de films
        private ObservableCollection<Model.Movie> _ListeFiltreeDeMecs;

        // Liste de genres
        private Model.Genres _LesGenres;

        // Mec sélectionné
        private Model.Movie _MecSelected;

        // Film ajouté
        private Model.Movie _AddedMovie;

        // Genre sélectionné
        private string _GenreSelected;

        // Modification active
        private bool _ModifActivated;

        private ViewModelOneMovie _ViewModelOneMovie;
        private ViewModelBase _CurrentViewModel;

        #endregion

        #region Properties

        #region Commands
        // Obtient la commande pour quitter l'application
        public DelegateCommand QuitCommand
        {
            get { return _QuitCommand; }
            private set { SetAndNotify("QuitCommand", ref _QuitCommand, value); }
        }

        // Obtient la commande pour sérialiser les données
        public DelegateCommand SaveCommand
        {
            get { return _SaveCommand; }
            set { SetAndNotify("SaveCommand", ref _SaveCommand, value); }
        }

        // Obtient la commande pour afficher la boite de dialogue A propos
        public DelegateCommand AboutCommand
        {
            get { return _AboutCommand; }
            private set { SetAndNotify("AboutCommand", ref _AboutCommand, value); }
        }

        // Obtient la commande pour ajouter un film à la liste de films
        public DelegateCommand AddCommand
        {
            get { return _AddCommand; }
            private set { SetAndNotify("AddCommand", ref _AddCommand, value); }
        }

        // Obtient la commande pour supprimer un Mec de la liste de Mec
        public DelegateCommand DeleteCommand
        {
            get { return _DeleteCommand; }
            private set
            {
                SetAndNotify("DeleteCommand", ref _DeleteCommand, value);
                // this.OnPropertyChanged("UnMecSelectionne");
            }
        }

        // Obtient la commande pour trier la liste
        public DelegateCommand SortCommand
        {
            get { return _SortCommand; }
            private set
            { SetAndNotify("SortCommand", ref _SortCommand, value); }
        }

        // Obtient la commande pour passer à la vue suivante
        public DelegateCommand GoNextCommand
        {
            get { return _GoNextCommand; }
            private set { SetAndNotify("GoNextCommand", ref _GoNextCommand, value);}
        }

        // Obtient la commande pour lancer la lecture
        public DelegateCommand PlayCommand
        {
            get { return _PlayCommand; }
            private set { SetAndNotify("PlayCommand", ref _PlayCommand, value); }
        }

        //
        // Obtient la commande pour lancer la lecture
        public DelegateCommand ModifyCommand
        {
            get { return _ModifyCommand; }
            private set { SetAndNotify("ModifyCommand", ref _ModifyCommand, value); }
        }

        // Obtient la commande pour sélectioner un visuel
        public DelegateCommand SelectCommand
        {
            get { return _SelectCommand; }
            set { SetAndNotify("SelectCommand", ref _SelectCommand, value); }
        }
        #endregion



        public Model.Movie UnMec
        {
            get { return _UnMec; }
            set { SetAndNotify("UnMec", ref _UnMec, value); }
        }

        public Model.Movie MecSelected
        {
            get { return _MecSelected; }
            set { SetAndNotify("MecSelected", ref _MecSelected, value); }
        }

        public Model.Movie AddedFilm
        {
            get { return _AddedMovie;  }
            set { SetAndNotify("AddedFilm", ref _AddedMovie, value); }
        }

        public Model.DataStore DataStore
        {
            get { return _DataStore; }
            set { SetAndNotify("DataStore", ref _DataStore, value); }
        }

        public ObservableCollection<Model.Movie> ListeFiltreeDeMecs
        {
            get { return _ListeFiltreeDeMecs; }
            set { SetAndNotify("ListeFiltreeDeMecs", ref _ListeFiltreeDeMecs, value); }
        }

        public Model.Genres LesGenres
        {
            get { return _LesGenres; }
            set { SetAndNotify("LesGenres", ref _LesGenres, value); }
        }

        public string GenreSelected
        {
            get { return _GenreSelected; }
            set { SetAndNotify("GenreSelected", ref _GenreSelected, value); }
        }

        public ViewModelOneMovie ViewModelOneMovie
        {
            get { return _ViewModelOneMovie; }
            set { SetAndNotify("ViewModelOneMovie", ref _ViewModelOneMovie, value); }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetAndNotify("CurrentViewModel", ref _CurrentViewModel, value); }
        }

        public bool ModifActivated
        {
            get { return _ModifActivated; }
            set { SetAndNotify("ModifActivated", ref _ModifActivated, value); }
        }


        #endregion

        #region Constructor
        // Initialise une nouvelle instance du Vue-Modèle
        public ViewModelMovies()
        {
            this.QuitCommand = new DelegateCommand(ExecuteQuitCommand, CanExecuteQuitCommand);
            this.SaveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            this.AboutCommand = new DelegateCommand(ExecuteAboutCommand, CanExecuteAboutCommand);
            this.AddCommand = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand);
            this.DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            this.SortCommand = new DelegateCommand(ExecuteSortCommand, CanExecuteSortCommand);
            this.GoNextCommand = new DelegateCommand(ExecuteGoNextCommand, CanExecuteGoNextCommand);
            this.PlayCommand = new DelegateCommand(ExecutePlayCommand, CanExecutePlayCommand);
            this.ModifyCommand = new DelegateCommand(ExecuteModifyCommand, CanExecuteModifyCommand);
            this._SelectCommand = new DelegateCommand(ExecuteSelectCommand, CanExecuteSelectCommand);

            LesGenres = new Model.Genres();

            // Instanciation du DataStore et chargement des données
            _DataStore = new Model.DataStore();
            _DataStore.LoadData(".\\data.mvz");

            ModifActivated = false;

            //ListeDeMecs.Where(m => m.Nom == "Skywalker");
        }



        #endregion

        #region Methods
        #region Commands

        // Méthode d'exécution de la commande pour quitter l'application
        private void ExecuteQuitCommand(object parameter)
        {
            App.Current.Shutdown();
        }

        public bool CanExecuteQuitCommand(object parameter)
        {
            return true;
        }

        // Méthode d'exécution de la commande pour sérialiser les données
        private void ExecuteSaveCommand(object parameter)
        {
            _DataStore.SaveData(".\\data.mvz");
        }

        private bool CanExecuteSaveCommand(object parameter)
        {
            return true;
        }

        // Méthode d'exécution de la commande d'affichage de la boite de dialogue A propos
        private void ExecuteAboutCommand(object obj)
        {
            MessageBox.Show("C'est la version 0.99. Mais oui.\r\nMerci pour votre visite... ", "A propos", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public bool CanExecuteAboutCommand(object parameter)
        {
            return true;
        }


        // Méthode d'exécution de la commande d'ajout de film à la liste de films
        private void ExecuteAddCommand(object obj)
        {

            string path = Directory.GetCurrentDirectory();
            path = path.Replace(@"\", "/");
            string defaultImage = path + @"/Images/film.png";

            _DataStore.ListeDeMecs.Add(new Model.Movie("", "", 0, defaultImage));
            
            //_MecSelected = _DataStore.ListeDeMecs.Last();
        }


        public bool CanExecuteAddCommand(object parameter)
        {
            return true;
        }

        // Méthode d'exécution de la commande de suppression d'un Mec à la liste de Mec
        private void ExecuteDeleteCommand(object obj)
        {
            _DataStore.ListeDeMecs.Remove(this.MecSelected);
            _DataStore.SaveData(".\\data.mvz");

        }

        public bool CanExecuteDeleteCommand(object parameter)
        {
            return this.MecSelected != null;
        }

        // Méthode d'exécution de la commande de suppression d'un Mec à la liste de Mec
        private void ExecuteSortCommand(object obj)
        {
            //ListeDeMecs = new ObservableCollection<Model.Movie>(from i in ListeDeMecs orderby i.Age select i);
            _DataStore.ListeDeMecs = new ObservableCollection<Model.Movie>(_DataStore.ListeDeMecs.OrderByDescending(m => m.Age));

        }

        public bool CanExecuteSortCommand(object parameter)
        {
            return this._DataStore.ListeDeMecs != null;
        }

        public void ExecuteGoNextCommand(object parameter)
        {

            string path = Directory.GetCurrentDirectory();
            path = path.Replace(@"\", "/");
            string defaultImage = path + @"/Images/film.png";
            
            AddedFilm = new Model.Movie();
            AddedFilm.SourceImage = defaultImage;
            _DataStore.ListeDeMecs.Add(AddedFilm);
            MecSelected = AddedFilm;
        }

        public bool CanExecuteGoNextCommand(object parameter)
        {
            return true;
        }

        public void ExecutePlayCommand(object parameter)
        {
            try
            {
                Process.Start(MecSelected.Nom);
                this.MecSelected.Watched = true;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Erreur de lecture...", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public bool CanExecutePlayCommand(object parameter)
        {
            return this.MecSelected != null;
        }

        public void ExecuteModifyCommand(object parameter)
        {
            this.ModifActivated = true;
        }

        public bool CanExecuteModifyCommand(object parameter)
        {
            return this.MecSelected != null;
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
                    this.MecSelected.Nom = openFileDialog.FileName;
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

        // Observe toutes les propriété du ViewModel
        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);

            switch (property)
            {
                case "GenreSelected":
                    // On retourne uniquement les 3 premiers de la liste
                    this.ListeFiltreeDeMecs = new ObservableCollection<Model.Movie>(_DataStore.ListeDeMecs.Where(m => m.Genre == GenreSelected).Take(3));
                    if (this.ModifActivated)
                    {
                        // Enregistrement si l'un des films a été modifié
                        _DataStore.SaveData(".\\data.mvz");
                        this.ModifActivated = false;
                    }
                    
                    //ListeFiltreeDeMecs = new ObservableCollection<Model.Mec>(ListeDeMecs.Where(m => m.Age > 500));
                    break;
                case "MecSelected":
                    if (this.ModifActivated)
                    {   
                        // Enregistrement si l'un des films a été modifié
                        _DataStore.SaveData(".\\data.mvz");
                        this.ModifActivated = false;
                    }
                    break;

                default:
                    break;
            }
        }

        #endregion

    }

}
