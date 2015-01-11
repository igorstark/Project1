using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovizManager.Model
{
    class Genres : ObservableObject
    {
        private ObservableCollection<string> _Genre;

        public ObservableCollection<string> Genre
        {
            get { return _Genre; }
            set { SetAndNotify("Genres", ref _Genre, value); }
        }

        public Genres()
        {
            this._Genre = new ObservableCollection<string> {"Comédie", "Erotique", "Muet", "Drame", "Dessin animé" };
        }
    }
}
