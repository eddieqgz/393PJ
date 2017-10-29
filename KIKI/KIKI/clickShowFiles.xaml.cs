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
using System.Windows.Shapes;
using KIKIXmlProcessor;
using System.Diagnostics;

namespace KIKI
{
    /// <summary>
    /// Interaction logic for clickShowFiles.xaml
    /// </summary>

    public partial class clickShowFiles : Window
    {
        private string[] id;

        // Constructor
        public clickShowFiles(string IDList)
        {
            InitializeComponent();
            XMLProcessor processor = new XMLProcessor(App.id);
            XMLSearcher searcher = new XMLSearcher(processor.GetWorkingPath(), App.id);
            char[] delimiterChars = { ';' };
            id = IDList.Split(delimiterChars);
            foreach (string s in id)
            {
                if (searcher.FindFilesByFileIDs(s).Count != 0)
                {
                    Files.Items.Add(searcher.FindFilesByFileIDs(s).Last().GetFileName());
                }
            }
        }

        private void Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (System.Windows.Forms.SystemInformation.MouseButtonsSwapped)
            { }
            else
            {
                clickFileShowMeeting newWindow = new clickFileShowMeeting(id[Files.SelectedIndex]);
                newWindow.Show();
            }
        }
    }

}
