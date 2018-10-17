using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tree
{
    public class StaticBinder : BaseObject
    {
        #region public properties

        public string Pdfs
        {
            get { return base.GetValue<string>("Pdfs"); }
            set { base.SetValue("Pdfs", value); }
        }

        public string FolderName
        {
            get { return base.GetValue<string>("FolderName"); }
            set { base.SetValue("FolderName", value); }
        }

        public string Others
        {
            get { return base.GetValue<string>("Others"); }
            set { base.SetValue("Others", value); }
        }

        public string Images {
            get { return base.GetValue<string>("Images"); }
            set { base.SetValue("Images", value); }
        }

        public string Zips {
            get { return base.GetValue<string>("Zips"); }
            set { base.SetValue("Zips", value); } }

        public string Texts {
            get { return base.GetValue<string>("Texts"); }
            set { base.SetValue("Texts", value); }
        }
        #endregion

        private static StaticBinder __instance = null;

        static StaticBinder()
        {
            __instance = new StaticBinder();
        }

        public StaticBinder()
        {
            //    MSG_Cancel = "Welcome";
            //    MSG_OK = "OK";
             //   MSG_Welcome = string.Format("Welcome {0}!", "WPF Binding");
            FolderName = "No Folder Selected";
            Images = "0"; Zips = "0"; Texts = "0"; Others = "0"; Pdfs = "0" ;
        }

        public static StaticBinder Instance
        {
            get { return __instance; }
        }
    }
}
