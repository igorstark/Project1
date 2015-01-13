using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace MovizManager.TemplateSel
{
    class ViewModelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template = base.SelectTemplate(item, container);

            if (item is ViewModels.ViewModelMovies)
            {
                template = App.Current.FindResource("ViewMoviesTemplate") as DataTemplate;
            }
            else if (item is ViewModels.ViewModelOneMovie)
            {
                template = App.Current.FindResource("ViewOneMovieTemplate") as DataTemplate;
            }

            else if (item is ViewModels.ViewModelSearch)
            {
            template = App.Current.FindResource("ViewSearchTemplate") as DataTemplate;
            }
            return template;
        }
    }
}
