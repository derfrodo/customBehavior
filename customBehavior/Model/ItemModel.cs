using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customBehavior.Model
{
    public class ItemModel : INotifyPropertyChanged
    {
        private string _message;
        private string _expanded;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }
        public String Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Expanded)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
