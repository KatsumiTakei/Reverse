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

    #region     OnChangePlayerState
    public static event Action<ePlayerStateType> OnChangePlayerState = null;
    public static void BroadcastChangePlayerState(ePlayerStateType playerStateType)
    {
        OnChangePlayerState?.Invoke(playerStateType);
    }
    #endregion  OnChangePlayerState
    
    #region     OnChangeMobState
    public static event Action<eMobStateType, int> OnChangeMobState = null;
    public static void BroadcastChangeMobState(eMobStateType mobStateType, int instanceId)
    {
        OnChangeMobState?.Invoke(mobStateType, instanceId);
    }
    #endregion  OnChangeMobState


    #region     OnAddAttackedList
    public static event Action<int> OnAddAttackedList = null;
    public static void BroadcastAddAttackList(int instanceId)
    {
        OnAddAttackedList?.Invoke(instanceId);
    }
    #endregion  OnAddAttackedList
    

    #region     OnRemoveAttackedList
    public static event Action<int> OnRemoveAttackedList = null;
    public static void BroadcastRemoveAttackedList(int instanceId)
    {
        OnRemoveAttackedList?.Invoke(instanceId);
    }
    #endregion  OnRemoveAttackedList



}

public interface IInputResponder
{
    void OnMultipleInput(eInputType inputType, sbyte index, eHIDType hIDType);
}