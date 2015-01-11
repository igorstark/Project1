using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovizManager.Model
{
    [Serializable]
    class Movie : ObservableObject
    {
        private string _MovieTitle;
        private string _Nom;
        private int _Age;
        private string _SourceImage;
        private string _Genre;
        private bool _Watched;


        public string MovieTitle
        {
            get { return _MovieTitle; }
            set { SetAndNotify("MovieTitle", ref _MovieTitle, value); }
        }

        public string Nom
        {
            get { return _Nom; }
            set { SetAndNotify("Nom", ref _Nom, value); }
        }

        public int Age
        {
            get { return _Age; }
            set { SetAndNotify("Age", ref _Age, value); }
        }

        public string SourceImage
        {
            get { return _SourceImage; }
            set { SetAndNotify("SourceImage", ref _SourceImage, value); }
        }

        public string Genre
        {
            get { return _Genre; }
            set
            { SetAndNotify("Genre", ref _Genre, value); }
        }

        public bool Watched
        {
            get { return _Watched; }
            set { SetAndNotify("Watched", ref _Watched, value); }
        }


        #region Constructors

        public Movie(string unNom, string unTitre, int unAge, string uneSourceImage)
        {
            Nom = unNom;
            MovieTitle = unTitre;
            Age = unAge;
            SourceImage = uneSourceImage;
        }


        public Movie(string unNom, string unTitre, int unAge)
        {
            Nom = unNom;
            MovieTitle = unTitre;
            Age = unAge;
        }

        public Movie(String unNom, string unTitre)
        {
            Nom = unNom;
            MovieTitle = unTitre;
        }

        public Movie()
        {
        }
        #endregion

        #region Methods


        #endregion
    }
}
