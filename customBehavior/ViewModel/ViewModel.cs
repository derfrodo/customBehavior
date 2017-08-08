using customBehavior.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customBehavior.ViewModel
{
    // Das HauptviewModel...
    public class ViewModel
    {
        
        public List<ItemViewModel> ItemModels { get; set; } = new List<ItemViewModel>
        {
            new ItemViewModel {Model = new ItemModel { Message="Some Item" } , Children=new List<ItemViewModel> { new ItemViewModel { Model= new ItemModel { Message = "SubItem" } } } },
            new ItemViewModel {Model = new ItemModel { Message="Another Item" } }
        };

    }
}
