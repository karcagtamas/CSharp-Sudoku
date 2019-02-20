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
        public MainWindow()
        {
            InitializeComponent();
            this.Fields = new Field[9, 9];
        }


        public void generateTable()
        {
            Random r = new Random();
            for (int i = 0; i < this.Fields.GetLength(0); i++)
            {
                for (int j = 0; j < this.Fields.GetLength(1); j++)
                {
                    int x;
                    do
                    {
                        x = r.Next(1,10);
                    } while (check(i,j,));
                    Field[i][j].number = x;
                }
            }
        }

        public bool check(int i, int j, int x)
        {

        
        
        }

        public bool checkRow(int j, int x){
            for (int i = 0; i < this.Fields.GetLength(0); i++){
                if (Fields[i][j].number === x) return false;
            }
            return true;
        }

        public bool checkCol(int i, int x){
            for (int j = 0; j < this.Fields.GetLength(1); j++){
                if (Fields[i][j].number === x) return false;
            }
            return true;
        }

        public bool checkOneBlock(int i, int j, int x){
            int row = i / 3;
            int col = j / 3;
            int posi = row * 3 + 1;
            int posj = col * 3 + 1;
            if (this.Fields[posi][posj].number === x) return false;
            if (this.Fields[posi][posj + 1].number === x) return false;
            if (this.Fields[posi][posj - 1].number === x) return false;
            if (this.Fields[posi + 1][posj].number === x) return false;
            if (this.Fields[posi + 1][posj + 1].number === x) return false;
            if (this.Fields[posi + 1][posj - 1].number === x) return false;
            if (this.Fields[posi - 1][posj].number === x) return false;
            if (this.Fields[posi - 1][posj + 1].number === x) return false;
            if (this.Fields[posi - 1][posj - 1].number === x) return false;
            return true;
        }
    }
}
