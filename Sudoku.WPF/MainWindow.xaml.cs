using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.WPF;

public partial class MainWindow : Window
{
    private int exampNum = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        //Uri iconUri = new Uri("favicon.ico", UriKind.RelativeOrAbsolute);
        //Icon = BitmapFrame.Create(iconUri);
    }

    private void buttonClear_Click(object sender, RoutedEventArgs e)
    {
        ClearSudoku();
        buttonExample.IsEnabled = true;
    }

    private void buttonExample_Click(object sender, RoutedEventArgs e)
    {
        ClearSudoku();

        // wiki example
        if (exampNum == 0)
        {
            box_01.Text = "3";
            box_13.Text = "1";
            box_14.Text = "9";
            box_15.Text = "5";
            box_22.Text = "8";
            box_27.Text = "6";

            box_30.Text = "8";
            box_34.Text = "6";
            box_40.Text = "4";
            box_43.Text = "8";
            box_48.Text = "1";
            box_54.Text = "2";

            box_61.Text = "6";
            box_66.Text = "2";
            box_67.Text = "8";
            box_73.Text = "4";
            box_74.Text = "1";
            box_75.Text = "9";
            box_78.Text = "5";
            box_87.Text = "7";
        }
        else if(exampNum == 1)
        {        
            // 2nd example
            box_00.Text = "2";
            box_01.Text = "9";
            box_02.Text = "6";
            box_11.Text = "8";
            box_20.Text = "7";
            box_21.Text = "4";
            box_23.Text = "8";
            box_24.Text = "9";
            box_25.Text = "1";

            box_32.Text = "8";
            box_33.Text = "9";
            box_34.Text = "1";
            box_42.Text = "7";
            box_43.Text = "2";
            box_45.Text = "4";
            box_47.Text = "6";
            box_51.Text = "3";
            box_53.Text = "6";
            box_56.Text = "1";
            box_58.Text = "5";

            box_64.Text = "7";
            box_71.Text = "7";
            box_73.Text = "1";
            box_75.Text = "2";
            box_76.Text = "9";
            box_77.Text = "5";
            box_78.Text = "8";

            box_80.Text = "1";
            box_84.Text = "3";
            box_88.Text = "4";
        }
        else
        {
            box_01.Text = "7";
            box_02.Text = "2";
            box_05.Text = "4";
            box_06.Text = "9";
            box_10.Text = "3";
            box_12.Text = "4";
            box_14.Text = "8";
            box_15.Text = "9";
            box_16.Text = "1";
            box_20.Text = "8";
            box_21.Text = "1";
            box_22.Text = "9";
            box_25.Text = "6";
            box_26.Text = "2";
            box_27.Text = "5";
            box_28.Text = "4";

            box_30.Text = "7";
            box_32.Text = "1";
            box_37.Text = "9";
            box_38.Text = "5";
            box_40.Text = "9";
            box_45.Text = "2";
            box_47.Text = "7";
            box_53.Text = "8";
            box_55.Text = "7";
            box_57.Text = "1";
            box_58.Text = "2";

            box_60.Text = "4";
            box_62.Text = "5";
            box_65.Text = "1";
            box_66.Text = "6";
            box_67.Text = "2";
            box_70.Text = "2";
            box_71.Text = "3";
            box_72.Text = "7";
            box_76.Text = "5";
            box_78.Text = "1";
            box_84.Text = "2";
            box_85.Text = "5";
            box_86.Text = "7";
        }

        exampNum++;
        if (exampNum > 2)
        {
            exampNum = 0;
        }
    }

    private async void buttonSolve_Click(object sender, RoutedEventArgs e)
    {
        this.IsEnabled = false;
        buttonExample.IsEnabled = false;

        SudokuLogic sudoku = new();

        //SetSudoku(sudoku);
        ActionForEveryTextBox((box, x, y) =>
        {
            if (int.TryParse(box.Text, out int value))
            {
                sudoku.SetValue(y, x, value);
            }
        });

        var result = await Task.Factory.StartNew(() => sudoku.GetStarted());

        this.IsEnabled = true;

        if (!result)
        {
            MessageBox.Show("Table couldn't be solved.", "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        //GetSudoku(sudoku);
        ActionForEveryTextBox((box, x, y) =>
        {
            box.IsReadOnly = true;

            if (box.Text != string.Empty)
            {
                return;
            }

            var value = sudoku.GetValue(y, x);
            box.Foreground = Brushes.Red;
            box.Text = value == 0 ? "" : value.ToString();

        });

        #region old version
        //this.IsEnabled = false;

        //buttonExample.IsEnabled = false;
        //bool result = false;
        //Task.Factory.StartNew(() =>
        //{
        //    result = _sudoku.GetStarted();


        //}).ContinueWith(t =>
        //{

        //    Dispatcher.Invoke(() => {
        //        this.IsEnabled = true;

        //        if (!result)
        //        {
        //            MessageBox.Show("Table couldn't be solved.", "Failed", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;
        //        }

        //        PrintContent(false);
        //    });
        //});
        #endregion
    }
    
    private void buttonExplane_Click(object sender, RoutedEventArgs e)
    {
        RulesWindow window = new RulesWindow();
        window.Owner = this;
        window.ShowDialog();
    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {
        TextBox box = (TextBox)sender;
        box.Text = ValidTextBox(box.Text);
    }

    //private void box_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    for (int y = 0; y < 9; y++)
    //    {
    //        for (int x = 0; x < 9; x++)
    //        {
    //            var foundTextBox = GetTextBoxByName("box_" + y + x);
    //            foundTextBox.Text = ValidString(foundTextBox.Text);
    //        }
    //    }
    //}

    private string ValidTextBox(string text)
    {
        Regex validateNumber = new Regex("^[1-9]+$");

        if (!validateNumber.IsMatch(text))
        {
            return string.Empty;
        }

        return text.FirstOrDefault().ToString();
    }

    //private void SetSudoku(SudokuLogic sudoku)
    //{
        //for (int y = 0; y < 9; y++)
        //{
        //    for (int x = 0; x < 9; x++)
        //    {
        //        var box = GetTextBoxByName("box_" + y + x);

        //        if (int.TryParse(box.Text, out int value))
        //        {
        //            sudoku.SetValue(y, x, value);
        //        }
        //    }
        //}
    //}
   
    //private void GetSudoku(SudokuLogic sudoku)
    //{
        //for (int y = 0; y < 9; y++)
        //{
        //    for (int x = 0; x < 9; x++)
        //    {
        //        var box = GetTextBoxByName("box_" + y + x);
        //        box.IsReadOnly = true;

        //        if (box.Text != string.Empty)
        //        {
        //            continue;
        //        }

        //        var value = sudoku.GetValue(y, x);
        //        box.Foreground = Brushes.Red;
        //        box.Text = value == 0 ? "" : value.ToString();
        //    }
        //}
    //}

    private void ClearSudoku()
    {
        ActionForEveryTextBox((box, _, _) =>
        {
            box.Foreground = Brushes.Black;
            box.Text = string.Empty;
            box.IsReadOnly = false;
        });
           
        //for (int y = 0; y < 9; y++)
        //{
        //    for (int x = 0; x < 9; x++)
        //    {
        //        var box = GetTextBoxByName("box_" + y + x);
        //        box.Foreground = Brushes.Black;
        //        box.Text = string.Empty;
        //        box.IsReadOnly = false;
        //    }
        //}
    }

    private void ActionForEveryTextBox(Action<TextBox, int, int> action)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                var box = GetTextBoxByName("box_" + y + x);
                action(box, x , y);
            }
        }
    }

    private TextBox GetTextBoxByName(string name)
    {
        TextBox foundTextBox = UIHelper.FindChild<TextBox>(Application.Current.MainWindow, name);
        return foundTextBox;
    }
}