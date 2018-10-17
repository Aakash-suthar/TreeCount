using System.Windows.Controls;

namespace tree
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
            this.DataContext = new SearchViewmodel();
        }
    }
}
