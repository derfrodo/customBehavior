using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace customBehavior
{
    public class ViewModel
    {
        public int Number { get; set; }

        public List<ItemViewModel> ItemModels { get; set; } = new List<ItemViewModel>
        {
            new ItemViewModel {Model = new ItemModel { Message="Some Item" } , Children=new List<ItemViewModel> { new ItemViewModel { Model= new ItemModel { Message = "SubItem" } } } },
            new ItemViewModel {Model = new ItemModel { Message="Another Item" } }
        };

    }

    public class ItemViewModel     {
        public ItemModel Model { get; set; }
        public List<ItemViewModel> Children { get; set; }


        public ICommand ItemExpanding => new RelayCommand<RoutedEventArgs>(e =>
        {
            //Hier könnten jetzt auch bspw. children geladen werden...
             Model.Expanded = "Expanded";
        });
    }

    public class ItemModel : INotifyPropertyChanged
    {
        private string _message;
        private string _expanded;
        public string Message { get { return _message; }
            set
            {
                _message = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }
        public String Expanded {
            get { return _expanded; }
            set
            {
                _expanded = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Expanded)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    //Implementierung aus: https://stackoverflow.com/questions/22285866/why-relaycommand
    //Blind für Demonstrationszwecke kopiert.
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }

}
