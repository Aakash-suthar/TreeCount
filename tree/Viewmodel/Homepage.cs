using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace tree
{
    public class Homepage : BaseViewModel
    {
        #region Public properties

        #region Static properties

        public static int i = 0;

        public static BackgroundWorker bg;

        public static List<string> l = new List<string>();

        public static Dictionary<string, Item> b;

        public static readonly List<string> ImageExtensions = new List<string> { ".jpeg", ".jpg", ".bmp", ".gif", ".png" };

        public static readonly List<string> Archice = new List<string> { ".zip", ".rar", ".iso" };

        public static readonly List<string> Doc = new List<string> { ".txt", ".doc", ".docx" };

        public static int Counts = 0;

        public int Count { get; set; } = 0;

        public string Status { get; set; } = "";

        public bool ScanEnable { get; set; } = true;
        #endregion


        //children know as items
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        public ICommand Refresh { get { return new RelayCommand(Refreshs); } }

        public ICommand Scan { get { return new RelayCommand(Scans); } }

        #endregion

        #region Constructor
        public Homepage()
        {
            b = new Dictionary<string, Item>();
            bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.WorkerReportsProgress = true;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.ProgressChanged += Bg_ProgressChanged;
        }
        #endregion

        #region Scans
        public void Scans()
        {

            ScanEnable = false;
            bg.RunWorkerAsync();
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                //Get the result from the background thread
                Status = e.Result.ToString();//Display the result to the user
                ScanEnable = true;
            }
            //else if (e.Cancelled)
            //{
            //    lblStatus.Text = "User Canceled";
            //}
            else
            {
                i = 0;
            }
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Count = e.ProgressPercentage;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            Status = "Scanning...";
            foreach(var drive in l)
            {
                Getobject(drive);
            }
            e.Result = "Done";
        }
        #endregion

        #region SetItems
        public void Setitems()
        {
            var Children = DirectoryStructure.GetLogicalDrives();
            Items = new ObservableCollection<DirectoryItemViewModel>(
                Children.Select(dirvv => new DirectoryItemViewModel(dirvv.Fullpath, DirectoryItemType.Drive)));
        }
        #endregion

        #region RefreshClick
        public void Refreshs()
        {
            Items?.Clear();
            Setitems();
            
        }
        #endregion

        #region Getobject object
        public Item Getobject(string directory)
        {
            //check if key is already present or not
            if (b.ContainsKey(directory)) { return b[directory]; };

            //create a new instance of item class
            Item c = new Item();
            #region Countfiles
            try
            {
                Directory.GetFiles(directory).ToList().ForEach(r => {
                    var ext = Path.GetExtension(r).ToLower();
                    if (ImageExtensions.Contains(ext)) c.Img++;
                    else if (Archice.Contains(ext)) c.Zip++;
                    else if (Doc.Contains(ext)) c.Txt++;
                    else if (ext.Equals(".pdf")) c.Pdf++;
                    else c.Other++;
                    Counts++;

                });
            }
            catch { }

            #endregion

            #region GetFolders
            try
            {
                //get directories
                var dirs = Directory.GetDirectories(directory).ToList();
                if (dirs.Count > 0)
                {
                    dirs.ForEach(d =>
                    {
                        c += Getobject(d);
                    });
                }
            }
            catch { }

            //report to background worker to ui
            bg.ReportProgress(Counts);
            #endregion
            //add values to key
            b.Add(directory, c);
            return c;
        }
        #endregion
    }
}
