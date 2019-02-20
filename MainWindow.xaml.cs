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
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Field[,] Fields;
        public Brush[] colors = new Brush[] { Brushes.DarkMagenta, Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Cyan, Brushes.Magenta, Brushes.Black, Brushes.DarkGreen, Brushes.Orange };
        public MainWindow()
        {
            InitializeComponent();
            this.Fields = new Field[9, 9];
            

            do
            {
                this.refresh();
                this.generateTable();
            } while (!this.isValid());
                this.writeOut();

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
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label label = new Label();
                    label.Content = this.Fields[i, j].number;
                    label.FontSize = 16;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.VerticalAlignment = VerticalAlignment.Center;
                    if (this.Fields[i, j].number != 0) label.Foreground = this.colors[this.Fields[i, j].number - 1];
                    else label.Background = Brushes.Red;
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
                    } while (check(i,j,x) && count < 100);
                    if (count < 100) this.Fields[i, j].number = x;
                    else f = false;
                }
            }
        }

        public bool check(int i, int j, int x)
        {
            if (checkRow(i, x) && checkCol(j, x) && checkOneBlock(i,j,x)) return false;
            else return true;
        }

        public bool checkRow(int i, int x){
            for (int j = 0; j < this.Fields.GetLength(0); j++){
                if (this.Fields[i,j].number == x) return false;
            }
            return true;
        }

        public bool checkCol(int j, int x){
            for (int i = 0; i < this.Fields.GetLength(1); i++){
                if (this.Fields[i,j].number == x) return false;
            }
            return true;
        }

        public bool checkOneBlock(int i, int j, int x){
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

        public bool isValid()
        {
            foreach (var i in this.Fields)
            {
                if (i.number == 0) return false;
            }
            return true;
        }
    }
}
