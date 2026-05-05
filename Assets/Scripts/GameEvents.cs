using System;
using UnityEngine;

namespace TicTacToe
{
    public static class GameEvents
    {
        public static Action<Cell> CellClicked;
        public static Action Undo;
        public static Action MoveMade;
        public static Action InvalidMove;
        public static Action<string> GameWon;
        public static Action GameDrawn;
        public static Action GameRestarted;
        public static Action UndoWin;
        public static Action<int, int> ScoreChanged;
        public static Action<string> ResultReady;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlay()
        {
            CellClicked = null;
            Undo = null;
            MoveMade = null;
            InvalidMove = null;
            GameWon = null;
            GameDrawn = null;
            ScoreChanged = null;
            ResultReady = null;
            GameRestarted = null;
            UndoWin = null;
        }
    }
}
