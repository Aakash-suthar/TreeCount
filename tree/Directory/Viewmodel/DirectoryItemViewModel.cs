using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Windows;

namespace tree
{
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region public properties

        public DirectoryItemType Type { get; set; }

        public string Fullpath { get; set; }

        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.Fullpath : DirectoryStructure.getfilefolder(this.Fullpath); } }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        public Visibility Visible { get { return Type == DirectoryItemType.File ? Visibility.Collapsed : Visibility.Visible; } }

        public bool CanExpand { get { return Type != DirectoryItemType.File; } }

        public ICommand ExpandCommand { get; set; }

        public ICommand Get { get { return new RelayCommand(SetName); } }

        //private bool Check2 { get; set; }

        public bool Check {
                get
                {
                    return Homepage.b.ContainsKey(Fullpath)||Homepage.l.Contains(Fullpath);
                }
                set {
                if (!Homepage.bg.IsBusy)
                {
                    if (value == true) Homepage.l.Add(Fullpath); 
                    else  Homepage.l.Remove(Fullpath); 
                }
                }
            }

        public bool IsExpanded
        {
            //get is ask by ui to expand or not
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            //set is ask when we we click expand icon
            set
            {
                //if ui tells us to expand
                if (value == true)
                    //find all children
                    Expand();
                //if ui tells us to close
                else
                    this.ClearChildren();
            }
        }
     
        #endregion

        #region Constructor
        public DirectoryItemViewModel(string fullpath, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);
            this.Type = type;
            this.Fullpath = fullpath;
            this.ClearChildren();
        }
        #endregion

        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;
            var children = DirectoryStructure.GetDirectoryContents(this.Fullpath);
            this.Children=new ObservableCollection<DirectoryItemViewModel>(
                children.Select(content=>new DirectoryItemViewModel(content.Fullpath,content.Type)));
        }

        public void SetName() {
            StaticBinder.Instance.FolderName = Name;

            if (Homepage.b.ContainsKey(Fullpath))
            {
                Item c = Homepage.b[Fullpath];
                StaticBinder.Instance.Images = c.Img.ToString();
                StaticBinder.Instance.Texts = c.Txt.ToString();
                StaticBinder.Instance.Pdfs = c.Pdf.ToString();
                StaticBinder.Instance.Others = c.Other.ToString();
                StaticBinder.Instance.Zips = c.Zip.ToString();
            }
            else
            {
                StaticBinder.Instance.Images = 0.ToString();
                StaticBinder.Instance.Texts = 0.ToString();
                StaticBinder.Instance.Pdfs = 0.ToString();
                StaticBinder.Instance.Others = 0.ToString();
                StaticBinder.Instance.Zips = 0.ToString();
            }
        }

        //removes all children from the lust add a ddummy item to show icon if required
        public void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();
            //show the expand arrow if we r not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }
    }
}
