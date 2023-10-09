using System;
using System.Windows;

namespace Sudoku.WPF
{
    /// <summary>
    /// Interaction logic for RulesWindow.xaml
    /// </summary>
    public partial class RulesWindow : Window
    {
        public RulesWindow()
        {
            InitializeComponent();

            textBlock.Text = "- Sudoku grid consists of 9x9 spaces."
                + Environment.NewLine
                + "- You can use only numbers from 1 to 9. "
                + Environment.NewLine
                + "- Each 3×3 block can only contain numbers from 1 to 9. "
                + Environment.NewLine
                + "- Each vertical column can only contain numbers from 1 to 9. "
                + Environment.NewLine
                + "- Each horizontal row can only contain numbers from 1 to 9. "
                + Environment.NewLine
                + "- Each number in the 3×3 block, vertical column or horizontal row can be used only once. "
                + Environment.NewLine
                + "- The game is over when the whole Sudoku grid is correctly filled with numbers.";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Uri iconUri = new Uri("favicon.ico", UriKind.RelativeOrAbsolute);
            //Icon = BitmapFrame.Create(iconUri);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}