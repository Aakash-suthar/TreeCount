using System.Windows.Controls;

namespace tree
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
            this.DataContext = new AboutPage();
        }
    }
}
