using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace customBehavior
{
    /// <summary>
    /// Credits gehen an https://stackoverflow.com/questions/23316932/invoke-command-when-treeviewitem-is-expanded
    /// </summary>
    public static class Behaviours
    {
        public static readonly DependencyProperty ExpandingBehaviourProperty =
            DependencyProperty.RegisterAttached("ExpandingBehaviour",
                typeof(ICommand),
                typeof(Behaviours),
                new PropertyMetadata(OnExpandingBehaviourChanged));

        public static void SetExpandingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(ExpandingBehaviourProperty, value);
        }

        public static ICommand GetExpandingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(ExpandingBehaviourProperty);
        }

        //Wird bei jeder neuen Zuweisung der Expanding Behaviour Dependency Property zum Objekt "d" aufgerufen.
        private static void OnExpandingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem tvi = d as TreeViewItem;

            if(tvi != null)
            {
                ICommand ic = e.NewValue as ICommand;
                if(ic != null)
                {
                    // Was hier nicht gemacht wird: Wir lösen keinen Handler (falls bereits ein anderes Command auf tvi.Expanded gebunden wurde)!
                    // Dies ist grundsätzlich nicht möglich in der Lambda-Schreibweise
                    // Bspw. möglicher Ansatz: konkreter Delegat, welcher in einem Directory vorgehalten wird.
                    // Allerdings: Unwahrscheinlicher fall, dass 2 Commands gebunden werden.
                    // Die Zuweisung zu dieser Property fand bislang in meinen Versuchsaufbauten stets innerhalb der XAML-Definition 
                    // und somit eigentlich nur einmalig (während der Initialisierung) statt
                    tvi.Expanded += (sender, routedEventArgs) =>
                    {
                        if (ic.CanExecute(routedEventArgs))
                        {
                            ic.Execute(routedEventArgs);
                        }
                        routedEventArgs.Handled = true;
                    };
                }
            }
        }
    }
}
