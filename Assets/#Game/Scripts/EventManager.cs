using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    #region     OnMultipleInput
    public static event Action<eInputType, sbyte, eHIDType> OnMultipleInput = null;
    public static void BroadcastMultipleInput(eInputType argInput, sbyte playerIndex, eHIDType hidType)
    {
        OnMultipleInput?.Invoke(argInput, playerIndex, hidType);
    }
    #endregion  OnMultipleInput

    #region     OnPlayerMove
    public static event Action<Vector2> OnPlayerMove = null;
    public static void BroadcastPlayerMove(Vector2 moveValue)
    {
        OnPlayerMove?.Invoke(moveValue);
    }
    #endregion  OnPlayerMove

}

public interface IInputResponder
{
    void OnMultipleInput(eInputType inputType, sbyte index, eHIDType hIDType);
}