using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MovizManager.Model
{
    class DataStore : ObservableObject
    {
        #region Fields
        private ObservableCollection<Movie> _ListeDeMecs;
        #endregion

        #region Properties
        // Obtient la liste de mecs
        public ObservableCollection<Movie> ListeDeMecs
        {
            get { return _ListeDeMecs; }
            set { SetAndNotify("ListeDeMecs", ref _ListeDeMecs, value); }
        }
        #endregion

        #region Constructor
        // Initialise une nouvelle instance de la source de données
        public DataStore()
        {
            ListeDeMecs = new ObservableCollection<Movie>();
        }
        #endregion

        #region Methods
        public void SaveData(string filePath)
        {
            using (FileStream fileStream = File.Open(filePath, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, this.ListeDeMecs);
            }
        }

        public void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    if (fileStream.Length > 0)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        this.ListeDeMecs = bf.Deserialize(fileStream) as ObservableCollection<Movie>;
                    }
                }
            }
        }

        #endregion
    }

}
