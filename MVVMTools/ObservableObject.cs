using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTools
{
    /// <summary>
    ///     Représente un objet observable compatible avec le moteur de Binding
    /// </summary>
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region MyRegion

        /// <summary>
        ///     Se produit avant qu'une propriété de l'objet change.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Se produit lorsqu'une propriété de l'objet change.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        #region Constructors

        public ObservableObject()
        {
            OnMaterialized();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Modifie un champ privé et notify que la propriété a changé.
        /// </summary>
        /// <typeparam name="T">Type de la propriété.</typeparam>
        /// <param name="property">Nom de la propriété.</param>
        /// <param name="value">Référence sur le champ privé exposé par la propriété.</param>
        /// <param name="newValue">Nouvelle valeur à donner au champ privé.</param>
        protected void SetAndNotify<T>(string property, ref T value, T newValue)
        {
            OnPropertyChanging(property);
            value = newValue;
            OnPropertyChanged(property);
        }

        //protected void SetAndNotify(string property, ref object value, object newValue)
        //{
        //    OnPropertyChanging(property);
        //    value = newValue;

        //    OnPropertyChanged(property);
        //}

        /// <summary>
        ///     Déclenche l'évènement PropertyChanging de manière sécurisé.
        /// </summary>
        /// <param name="property">Nom de la propriété qui va changer.</param>
        protected virtual void OnPropertyChanging(string property)
        {
            PropertyChangingEventHandler handler = PropertyChanging;

            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(property));
            }
        }

        /// <summary>
        ///     Déclenche l'évènement PropertyChanged de manière sécurisé.
        /// </summary>
        /// <param name="property">Nom de la propriété qui a changé.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public virtual void OnMaterialized()
        {

        }

        #endregion

    }
}
