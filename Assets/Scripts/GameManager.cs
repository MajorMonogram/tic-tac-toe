using UnityEngine;

namespace TicTacToe
{
    public class GameManager : MonoBehaviour
    {
        private int _scoreX;
        private int _scoreO;

        private string _lastWinner;

        private void OnEnable()
        {
            GameEvents.GameWon += OnGameWon;
            GameEvents.GameDrawn += OnGameDrawn;
            GameEvents.UndoWin += OnUndoWin;
        }

        private void OnDisable()
        {
            GameEvents.GameWon -= OnGameWon;
            GameEvents.GameDrawn -= OnGameDrawn;
            GameEvents.UndoWin -= OnUndoWin;
        }

        private void OnUndoWin()
        {
            if (_lastWinner == "X")
            {
                _scoreX--;
            }
            else if(_lastWinner == "O")
            {
                _scoreO--;
            }
            _lastWinner = "";
            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
        }

        private void Start()
        {
            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
        }

        private void OnGameWon(string winner)
        {
            _lastWinner = winner;
            if (winner == "X")
            {
                _scoreX++;
            }
            else
            {
                _scoreO++;
            }

            GameEvents.ScoreChanged?.Invoke(_scoreX, _scoreO);
            GameEvents.ResultReady?.Invoke($"{winner} wins!");
        }
        

        private void OnGameDrawn()
        {
            _lastWinner = "";
            GameEvents.ResultReady?.Invoke("Draw!");
        }
    }
}
