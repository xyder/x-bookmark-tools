using System.Windows.Forms;
using XDBookmarkTools.Controllers;

namespace XDBookmarkTools.Views
{
    public partial class InfoPanel : UserControl
    {
        public DbController Dbc;

        public string InfoText
        {
            set { infoLabel.Text = value; }
        }

        public InfoPanel(DbController dbc)
        {
            InitializeComponent();
            Dbc = dbc;
        }
    }
}