using System;
using TicTacToe;
using UnityEngine;
using UnityEngine.UI;

public class UndoController : MonoBehaviour
{
    public Button undoButton;

    private void OnEnable()
    {
        undoButton.onClick.AddListener(Undo);
    }
    
    private void OnDisable()
    {
        undoButton.onClick.RemoveListener(Undo);
    }

    private void Undo()
    {
        GameEvents.Undo?.Invoke();
    }
}
