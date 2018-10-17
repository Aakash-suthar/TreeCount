using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace tree
{
    public class SearchViewmodel : BaseViewModel
    {
        public static BackgroundWorker Bw;

        public string SearchText { get; set; }

        public int Images { get; set; } = 0;

        public int Zips { get; set; } = 0;

        public int Pdfs { get; set; } = 0;

        public int Texts { get; set; } = 0;

        public int Others { get; set; } = 0;

        public string Status { get; set; } ="Status";

        public ICommand SearchButton { get { return new RelayCommand(Searchs); } }

        public SearchViewmodel()
        {
            Bw = new BackgroundWorker();
            Bw.DoWork += Bw_DoWork;
            Bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            Bw.WorkerReportsProgress = false;
        }

        private void Searchs()
        {
            try
            {
                if (Directory.GetFiles(SearchText).Count() > 0) { 
                    Bw.RunWorkerAsync();
                }
            }
            catch { Status = "Folder OR Directory Not Found."; }    
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                //Get the result from the background thread
                Status = e.Result.ToString();//Display the result to the user
            }
            else Status = "Error";
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Status = "Scanning";
            Getobject(SearchText);
            e.Result = "Done";
        }

        #region Getobject object
        public Item Getobject(string directory)
        {
            //check if key is already present or not
            if (Homepage.b.ContainsKey(directory)) { return Homepage.b[directory]; };

            //create a new instance of item class
            Item c = new Item();
            #region Countfiles
            try
            {
                Directory.GetFiles(directory).ToList().ForEach(r => {
                    var ext = Path.GetExtension(r).ToLower();
                    if (Homepage.ImageExtensions.Contains(ext)) { c.Img++; Images++; }
                    else if (Homepage.Archice.Contains(ext)) { c.Zip++; Zips++; }
                    else if (Homepage.Doc.Contains(ext)) { c.Txt++; Texts++; }
                    else if (ext.Equals(".pdf")) { c.Pdf++; Pdfs++; }
                    else { c.Other++; Others++; }
                    //Counts++;

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
            #endregion
            //add values to key
            Homepage.b.Add(directory, c);
            return c;
        }
        #endregion
    }
}
