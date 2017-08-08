using customBehavior.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace customBehavior.ViewModel
{
    //Einfaches View Model für Items im Treeview.
    //Sinnvoll wäre hier ggf. für die Vorliegende Konstruktion (Children im View Model) INotifyPropertyChanged
    public class ItemViewModel
    {
        public ItemModel Model { get; set; }
        public List<ItemViewModel> Children { get; set; }

        // Annahme: Sowohl View als auch das ViewModel sind Teil des UI-Layers.
        // Durchführung von Applikationslogik kann hier vom (jeweils zuständigen) 
        // ViewModel an Logic-Layer / entsprechende Logic-Funktionsklasse weitergegeben werden
        public ICommand ItemExpanding => new RelayCommand<RoutedEventArgs>(e =>
        {
            //Hier könnten jetzt auch bspw. children geladen werden...
            Model.Expanded = "Expanded";
        });
    }
}
