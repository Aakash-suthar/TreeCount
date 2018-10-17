using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace tree
{
    public class DirectoryStructureViewModel : BaseViewModel
    {
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        public DirectoryStructureViewModel() {
            var Children = DirectoryStructure.GetLogicalDrives();
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                Children.Select(dirvv => new DirectoryItemViewModel(dirvv.Fullpath, DirectoryItemType.Drive)));
                
                
         }


    }
}
