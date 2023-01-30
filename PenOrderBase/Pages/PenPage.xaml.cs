using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PenOrderBase.Pages
{
    /// <summary>
    /// Interaction logic for PenPage.xaml
    /// </summary>
    public partial class PenPage : Page
    {
        public Core.Pen Pen { get; set; }
        public List<PenType> PenTypes { get; set; }
        public List<Company> Companies { get; set; }

        public PenPage(Core.Pen pen, bool isNew = false)
        {
            InitializeComponent();

            Pen = pen;
            PenTypes = DataAccess.GetPenTypes();
            Companies = DataAccess.GetCompanies();

            if (isNew)
            {
                Title = $"Новый {Title}";
            }
            else
            {
                Title += $" {Pen.Id}";
                IsEnabled = false;
            }

            this.DataContext = this;
        }
    }
}
