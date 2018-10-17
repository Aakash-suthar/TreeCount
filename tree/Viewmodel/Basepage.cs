using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace tree
{
    public class Basepage : BaseViewModel
    {
        #region Private members
        private Window w;
        #endregion

        #region Public Properties

        public Color Background { get; set; } = Colors.Bisque;

        public ApplicationPage Application { get; set; } = ApplicationPage.Homepage1;

        public int CustomMargin { get; set; } = 0;

        public string Title { get; set; } = "InDetail";

        public ICommand Close { get { return new RelayCommand(() => w.Close()); } }

        public ICommand Mini { get { return new RelayCommand(() => w.WindowState = WindowState.Minimized); } }

        public ICommand SetAbout { get { return new RelayCommand(() => Application = ApplicationPage.About); } }

        public ICommand SetHome { get { return new RelayCommand(() => Application = ApplicationPage.Homepage1); } }

        public ICommand Search { get { return new RelayCommand(() => Application = ApplicationPage.Search); } }

        public ICommand Exit { get { return new RelayCommand(() => Environment.Exit(0)); } }
        #endregion

        #region Constructor

        public Basepage(Window m)
        {
            w = m;
        }
        #endregion
      
    }
}
