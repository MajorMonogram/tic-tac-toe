using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;

        private string _currentPlayer = "X";
        private bool _isGameOver;
        
        private Stack<ICommand> _history;

        private static readonly int[][] WinningLines = new int[][]
        {
            
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 },
        };

        private void OnEnable()
        {
            GameEvents.CellClicked += OnCellClicked;
            GameEvents.Undo += OnUndo;
            GameEvents.GameRestarted += OnRestart;
        }

        private void OnDisable()
        {
            GameEvents.CellClicked -= OnCellClicked;
            GameEvents.Undo -= OnUndo;
            GameEvents.GameRestarted -= OnRestart;
        }

        private void OnRestart()
        {
            ResetBoard();
            _history.Clear();
            _isGameOver = false;
        }

        private void OnUndo()
        {
            if (_history == null || _history.Count == 0)
            {
                GameEvents.InvalidMove?.Invoke();
                return;
            }

            if (_isGameOver)
            {
                GameEvents.UndoWin?.Invoke();
                _isGameOver = false;
            }
            _history.Pop().Undo();
            _currentPlayer = _currentPlayer == "X" ? "O" : "X";
        }

        private void OnCellClicked(Cell cell)
        {
            if (_isGameOver)
            {
                GameEvents.InvalidMove?.Invoke();
                return;
            }

            if (!cell.IsEmpty())
            {
                GameEvents.InvalidMove?.Invoke();
                return;
            }

            cell.SetMark(_currentPlayer);
            cell.Execute();
            if (_history == null)
            {
                _history = new Stack<ICommand>();
            }
            _history.Push(cell);
            GameEvents.MoveMade?.Invoke();

            string winner = CheckWinner();
            if (winner != "")
            {
                _isGameOver = true;
                GameEvents.GameWon?.Invoke(winner);
                _currentPlayer = _currentPlayer == "X" ? "O" : "X";
                //ResetBoard();
                return;
            }

            if (IsBoardFull())
            {
                _isGameOver = true;
                GameEvents.GameDrawn?.Invoke();
                _currentPlayer = _currentPlayer == "X" ? "O" : "X";
                //ResetBoard();
                return;
            }

            _currentPlayer = _currentPlayer == "X" ? "O" : "X";
        }

        private void ResetBoard()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].Clear();
            }
            _currentPlayer = "X";
            _isGameOver = false;
        }

        private string CheckWinner()
        {
            for (int i = 0; i < WinningLines.Length; i++)
            {
                int[] line = WinningLines[i];
                string a = _cells[line[0]].Mark;
                string b = _cells[line[1]].Mark;
                string c = _cells[line[2]].Mark;

                if (a != "" && a == b && b == c)
                {
                    return a;
                }
            }
            return "";
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
