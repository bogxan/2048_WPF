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

namespace examen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int Size { get; set; } = 4;
        private static Cell[,] board = new Cell[Size, Size];
        private static Cell[,] tmpCells = new Cell[Size, Size];


        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cell cell = new Cell(i, j, 0)
                    {
                        Background = Brushes.Yellow,
                        // Name = $"tb{i * j}",
                        Text = "",
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        IsReadOnly = true,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(5),
                        FontSize = 25,

                    };
                    var setter = new Setter
                    {
                        Property = Label.CursorProperty,
                        Value = Cursors.Hand
                    };
                    var trigger = new Trigger
                    {
                        Property = Label.IsMouseOverProperty,
                        Value = true,
                        Setters = { setter }
                    };
                    var style = new Style
                    {
                        Triggers = { trigger }
                    };
                    cell.Style = style;
                    board[i, j] = cell;
                    tmpCells[i, j] = cell;
                    grid.Children.Add(cell);
                    Grid.SetRow(cell, cell.Row);
                    Grid.SetColumn(cell, cell.Collumn);
                }
            }
            int pick1, pick2;
            Random rnd = new Random();
            do
            {
                pick1 = rnd.Next(1, 17) - 1;
                pick2 = rnd.Next(1, 17) - 1;
            } while (pick1 == pick2);
            board[pick1 / 4, (pick1) % 4].Value = 2;
            board[pick2 / 4, (pick2) % 4].Value = 2;
            copyState();
        }

        static void GenerateNewItem()
        {
            Random rnd = new Random();
            int pick, row, col;
            do
            {
                pick = rnd.Next(1, 17) - 1;
                row = pick / 4;
                col = pick % 4;
            } while (board[row, col].Value != 0);
            board[row, col].Value = 2;
        }
        
        static int changeInState()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (board[i, j].Value != tmpCells[i, j].Value)
                        return 1;
            return 0;
        }
        
        static void copyState()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    tmpCells[i, j].Value = board[i, j].Value;
        }
        
        static int filledAll()
        {
            int filled = 1;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (board[i, j].Value == 0)
                        filled = 0;
            return filled;
        }
        
        private void MoveRight()
        {
            for (int i = 0; i < 4; i++)
            {
                int[] temp = new int[4];
                int j, k = 0;
                for (j = 0; j < 4; j++)
                {
                    if (board[i, j].Value != 0)
                    {
                        temp[k] = board[i, j].Value;
                        k++;
                    }
                }
                int total = k;
                k--;
                for (j = 3; j >= 0 && k >= 0; j--)
                {
                    board[i, j].Value = temp[k];
                    k--;
                }
                while (j >= 0)
                {
                    board[i, j].Value = 0;
                    //board[i, j].Text = board[i, j].Value.ToString();
                    j--;
                }
                if ((board[i, 3].Value == board[i, 2].Value) && (board[i, 3].Value != 0))
                {
                    board[i, 3].Value = board[i, 2].Value + board[i, 3].Value;
                    board[i, 3].Text = board[i, 3].Value.ToString();
                    board[i, 2].Value = board[i, 1].Value;
                    board[i, 2].Text = board[i, 2].Value.ToString();
                    board[i, 1].Value = board[i, 0].Value;
                    board[i, 1].Text = board[i, 1].Value.ToString();
                    board[i, 0].Value = 0;
                    //board[i, 0].Text = board[i, 0].Value.ToString();
                }
                if ((board[i, 2].Value == board[i, 1].Value) && (board[i, 2].Value != 0))
                {
                    board[i, 2].Value = board[i, 1].Value + board[i, 2].Value;
                    board[i, 2].Text = board[i, 2].Value.ToString();
                    board[i, 1].Value = board[i, 0].Value;
                    board[i, 1].Text = board[i, 1].Value.ToString();
                    board[i, 0].Value = 0;
                    //board[i, 0].Text = board[i, 0].Value.ToString();
                }
                if ((board[i, 1].Value == board[i, 0].Value) && (board[i, 1].Value != 0))
                {
                    board[i, 1].Value = board[i, 0].Value + board[i, 1].Value;
                    board[i, 1].Text = board[i, 1].Value.ToString();
                    board[i, 0].Value = 0;
                    //board[i, 0].Text = board[i, 0].Value.ToString();
                }
            }
            copyState();
            GenerateNewItem();
            GameResult();
        }

        private void MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                int[] temp = new int[4];
                int j, k = 0;
                for (j = 0; j < 4; j++)
                {
                    if (board[i, j].Value != 0)
                    {
                        temp[k] = board[i, j].Value;
                        k++;
                    }
                }
                int total = k;
                k = 0;
                for (j = 0; j <= 3 && k < total; j++)
                {
                    board[i, j].Value = temp[k];
                    k++;
                }
                while (j <= 3)
                {
                    board[i, j].Value = 0;
                    j++;
                }
                if ((board[i, 0].Value == board[i, 1].Value) && (board[i, 0].Value != 0))
                {
                    board[i, 0].Value = board[i, 0].Value + board[i, 1].Value;
                    board[i, 0].Text = board[i, 0].Value.ToString();
                    board[i, 1].Value = board[i, 2].Value;
                    //board[i, 1].Text = board[i, 1].Value.ToString();
                    board[i, 2].Value = board[i, 3].Value;
                    //board[i, 2].Text = board[i, 2].Value.ToString();
                    board[i, 3].Value = 0;
                }
                if ((board[i, 1].Value == board[i, 2].Value) && (board[i, 2].Value != 0))
                {
                    board[i, 1].Value = board[i, 1].Value + board[i, 2].Value;
                    board[i, 1].Text = board[i, 1].Value.ToString();
                    board[i, 2].Value = board[i, 3].Value;
                    //board[i, 2].Text = board[i, 2].Value.ToString();
                    board[i, 3].Value = 0;
                }
                if ((board[i, 2].Value == board[i, 3].Value) && (board[i, 2].Value != 0))
                {
                    board[i, 2].Value = board[i, 2].Value + board[i, 3].Value;
                    board[i, 2].Text = board[i, 2].Value.ToString();
                    board[i, 3].Value = 0;
                }
            }
            copyState();
            GenerateNewItem();
            GameResult();
        }

        private void MoveDown()
        {
            for (int i = 0; i < 4; i++)
            {
                int[] temp = new int[4];
                int j, k = 0;
                for (j = 0; j < 4; j++)
                {
                    if (board[j, i].Value != 0)
                    {
                        temp[k] = board[j, i].Value;
                        k++;
                    }
                }
                int total = k;
                k--;
                for (j = 3; j >= 0 && k >= 0; j--)
                {
                    board[j, i].Value = temp[k];
                    k--;
                }
                while (j >= 0)
                {
                    board[j, i].Value = 0;
                    j--;
                }
                if ((board[3, i].Value == board[2, i].Value) && (board[3, i].Value != 0))
                {
                    board[3, i].Value = board[2, i].Value + board[3, i].Value;
                    board[3, i].Text = board[3, i].Value.ToString();
                    board[2, i].Value = board[1, i].Value;
                    //board[2, i].Text = board[2, i].Value.ToString();
                    board[1, i].Value = board[0, i].Value;
                    //board[1, i].Text = board[1, i].Value.ToString();
                    board[0, i].Value = 0;
                }
                if ((board[2, i].Value == board[1, i].Value) && (board[2, i].Value != 0))
                {
                    board[2, i].Value = board[1, i].Value + board[2, i].Value;
                    board[2, i].Text = board[2, i].Value.ToString();
                    board[1, i].Value = board[0, i].Value;
                    //board[1, i].Text = board[1, i].Value.ToString();
                    board[0, i].Value = 0;
                }
                if ((board[1, i].Value == board[0, i].Value) && (board[1, i].Value != 0))
                {
                    board[1, i].Value = board[0, i].Value + board[1, i].Value;
                    board[1, i].Text = board[1, i].Value.ToString();
                    board[0, i].Value = 0;
                }
            }
            copyState();
            GenerateNewItem();
            GameResult();
        }

        private void MoveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                int[] temp = new int[4];
                int j, k = 0;
                for (j = 0; j < 4; j++)
                {
                    if (board[j, i].Value != 0)
                    {
                        temp[k] = board[j, i].Value;
                        k++;
                    }
                }
                int total = k;
                k = 0;
                for (j = 0; j <= 3 && k < total; j++)
                {
                    board[j, i].Value= temp[k];
                    board[i, j].Text = board[i, j].Value.ToString();
                    k++;
                }
                while (j <= 3)
                {
                    board[j, i].Value = 0;
                    //board[i, j].Text = board[j, i].Value.ToString();
                    j++;
                }
                if ((board[0, i].Value == board[1, i].Value) && (board[0, i].Value != 0))
                {
                    board[0, i].Value = board[0, i].Value + board[1, i].Value;
                    board[0, i].Text = board[0, i].Value.ToString();
                    board[1, i].Value = board[2, i].Value;
                    //board[1, i].Text = board[1, i].Value.ToString();
                    board[2, i].Value = board[3, i].Value;
                    //board[2, i].Text = board[2, i].Value.ToString();
                    board[3, i].Value = 0;
                    //board[3, i].Text = board[3, i].Value.ToString();
                }
                if ((board[1, i] == board[2, i]) && (board[1, i].Value != 0))
                {
                    board[1, i].Value = board[1, i].Value + board[2, i].Value;
                    board[1, i].Text = board[1, i].Value.ToString();
                    board[2, i].Value = board[3, i].Value;
                    //board[2, i].Text = board[2, i].Value.ToString();
                    board[3, i].Value = 0;
                    //board[3, i].Text = board[3, i].Value.ToString();
                }
                if ((board[2, i].Value == board[3, i].Value) && (board[2, i].Value != 0))
                {
                    board[2, i].Value = board[2, i].Value + board[3, i].Value;
                    board[2, i].Text = board[2, i].Value.ToString();
                    board[3, i].Value = 0;
                    //board[3, i].Text = board[3, i].Value.ToString();
                }
            }
            copyState();
            GenerateNewItem();
            GameResult();
        }
        
        static int checkGameOver()
        {
            return filledAll();
        }
        
        static void GameResult()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if( board[x,y].Value==2048)
                    {
                        var result = MessageBox.Show("CONGRATULATION!!!!!! YOU WON!!!!");
                        if (result == MessageBoxResult.OK)
                        {
                            Environment.Exit(0);
                        }
                    }
                }
            }    
            if (checkGameOver() == 1) 
            {
                var result = MessageBox.Show("All cells are filled!!! You loose!" );
                if (result == MessageBoxResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            copyState();
            if (e.Key == Key.Up)
            {
                MoveUp();
            }
            if (e.Key == Key.Down)
            {
                MoveDown();
            }
            if (e.Key == Key.Left)
            {
                MoveLeft();
            }
            if (e.Key == Key.Right)
            {
                MoveRight();
            }
        }
    }
}
