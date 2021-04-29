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
using PitchOnline.Core;

namespace PitchOnline
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new WindowViewModel(this);
            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        } 
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //IoC.Get<SettingsViewModel>().Save();
        }
    }

   
}
