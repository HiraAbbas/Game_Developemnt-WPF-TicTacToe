using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{

    public partial class MainWindow : Window
    {
        private char[,] board = new char[3, 3];
        private char a = 'X';
        private char b = 'O';
        private char currentPlayer;
        private int moves = 0;
        private int xWins = 0, oWins = 0, draws = 0;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            board = new char[3, 3];
            moves = 0;
            currentPlayer = a;
            TurnText.Text = $"Player {currentPlayer}'s Turn";
            WinText.Text = "";

            foreach (var child in GameGrid.Children)
            {
                if (child is Button button)
                {
                    button.Content = "";
                    button.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content.ToString() == "")
            {
                int index = GameGrid.Children.IndexOf(btn);
                int row = index / 3;
                int col = index % 3;

                board[row, col] = currentPlayer;
                btn.Content = currentPlayer.ToString();
                btn.IsEnabled = false;

                moves++;

                if (CheckWin(currentPlayer))
                {
                    WinText.Text = $"Player {currentPlayer} wins!";
                    if (currentPlayer == 'X') xWins++; else oWins++;
                    UpdateScore();
                    DisableBoard();
                    return;
                }
                else if (moves == 9)
                {
                    WinText.Text = "It's a draw!";
                    draws++;
                    UpdateScore();
                    return;
                }

                currentPlayer = currentPlayer == a ? b : a;
                TurnText.Text = $"Player {currentPlayer}'s Turn";
            }
        }

        private bool CheckWin(char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                {
                    return true;
                }
            }

            return (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                   (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
        }

        private void UpdateScore()
        {
            ScoreX.Text = $"Player X Wins: {xWins}";
            ScoreO.Text = $"Player O Wins: {oWins}";
            Draws.Text = $"Draws: {draws}";
        }

        private void DisableBoard()
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}
