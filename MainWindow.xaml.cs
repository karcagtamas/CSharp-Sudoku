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

namespace Sudoku
{

    public class Field
    {
        public int number { get; set; }
        public bool isTask { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Field[,] Fields;
        public Field[,] Completed;
        public Brush[] colors = new Brush[] { Brushes.DarkMagenta, Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.DarkViolet, Brushes.Magenta, Brushes.DarkOliveGreen, Brushes.DarkGreen, Brushes.Orange };
        public MainWindow()
        {
            InitializeComponent();
            this.Fields = new Field[9, 9];
        }

        public void refresh()
        {
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.Fields[i, j] = new Field();
                }
            }
        }

        public void writeOut()
        {
            for (int i = 0; i < GridSudoku.Children.Count - 1; i++)
            {
                if (GridSudoku.Children[i].GetType() == typeof(Label))
                {
                    Label L = (Label)GridSudoku.Children[i];
                    GridSudoku.Children.Remove(L);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label label = new Label();
                    label.Content = this.Fields[i, j].number != 0 ? this.Fields[i,j].number.ToString() : " ";
                    label.FontSize = 16;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;
                    if (this.Fields[i, j].number != 0) label.Foreground = this.colors[this.Fields[i, j].number - 1];
                    label.Background = Brushes.LightGray;
                    label.Width = 50;
                    label.Height = 50;
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    label.SetValue(Grid.RowProperty, i);
                    label.SetValue(Grid.ColumnProperty, j);
                    GridSudoku.Children.Add(label);
                }
            }
        }


        public void generateTable()
        {
            Random r = new Random();
            bool f = true;
            for (int i = 0; i < this.Fields.GetLength(0) && f; i++)
            {
                for (int j = 0; j < this.Fields.GetLength(1) && f; j++)
                {
                    int x;
                    int count = 0;
                    do
                    {
                        x = r.Next(1,10);
                        count++;
                    } while (check(i,j,x, this.Fields) && count < 100);
                    if (count < 100)
                    {
                        this.Fields[i, j].number = x;
                    }
                    else f = false;
                }
            }
        }

        public bool check(int i, int j, int x, Field[,] F)
        {
            if (checkRow(i, x, F) && checkCol(j, x, F) && checkOneBlock(i,j,x, F)) return false;
            else return true;
        }

        public bool checkRow(int i, int x, Field[,] F)
        {
            for (int j = 0; j < this.Fields.GetLength(0); j++){
                if (this.Fields[i,j].number == x) return false;
            }
            return true;
        }

        public bool checkCol(int j, int x, Field[,] F)
        {
            for (int i = 0; i < this.Fields.GetLength(1); i++){
                if (this.Fields[i,j].number == x) return false;
            }
            return true;
        }

        public bool checkOneBlock(int i, int j, int x, Field[,] F)
        {
            int row = i / 3;
            int col = j / 3;
            int posi = row * 3 + 1;
            int posj = col * 3 + 1;
            if (this.Fields[posi,posj].number == x) return false;
            if (this.Fields[posi,posj + 1].number == x) return false;
            if (this.Fields[posi,posj - 1].number == x) return false;
            if (this.Fields[posi + 1,posj].number == x) return false;
            if (this.Fields[posi + 1,posj + 1].number == x) return false;
            if (this.Fields[posi + 1,posj - 1].number == x) return false;
            if (this.Fields[posi - 1,posj].number == x) return false;
            if (this.Fields[posi - 1,posj + 1].number == x) return false;
            if (this.Fields[posi - 1,posj - 1].number == x) return false;
            return true;
        }

        public bool isValid(Field[,] F)
        {
            foreach (var i in F)
            {
                if (i.number == 0) return false;
            }
            return true;
        }

        public void deleteNumbers(int count)
        {
            Random r = new Random();

            do
            {

                int i = r.Next(0, 9);
                int j = r.Next(0, 9);
                if (this.Fields[i, j].number != 0)
                {
                    this.Fields[i, j].number = 0;
                    count--;
                }
            } while (count != 0);
        }

        public void completeTask()
        {
            Random r = new Random();
            this.Completed = this.Fields;
            bool f = true;
            for (int i = 0; i < this.Completed.GetLength(0) && f; i++)
            {
                for (int j = 0; j < this.Completed.GetLength(1) && f; j++)
                {
                    if (this.Completed[i,j].number == 0)
                    {
                        int x;
                        int count = 0;
                        do
                        {
                            x = r.Next(1, 10);
                            count++;
                        } while (check(i, j, x, this.Completed) && count < 100);
                        if (count < 100)
                        {
                            this.Completed[i, j].number = x;
                        }
                        else f = false;
                    }
                }
            }
        }

        private void BtnGenerateFull_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                this.refresh();
                this.generateTable();
            } while (!this.isValid(this.Fields));
            this.writeOut();
        }

        private void BtnGenerateTask_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                this.refresh();
                this.generateTable();
            } while (!this.isValid(this.Fields));
            this.deleteNumbers(20);
            this.writeOut();

        }

        private void BtnTaskComplete_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                this.completeTask();
            } while (!this.isValid(this.Completed));
            this.Fields = this.Completed;
            this.writeOut();
        }
    }
}
