using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateBase
    , IInputResponder
{
    public PlayerStateMove(SpriteRenderer spriteRenderer) : base(spriteRenderer)
    {
    }

    public override void Update()
    {
    }

    public override void OnEnableState()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
    }

    public override void OnDisableState()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }

    public void OnMultipleInput(eInputType inputType, sbyte index, eHIDType hIDType)
    {
        if (inputType == eInputType.MoveDownKey)
        {
            EventManager.BroadcastPlayerMove(Vector2.down);
        }
        else if (inputType == eInputType.MoveUpKey)
        {
            EventManager.BroadcastPlayerMove(Vector2.up);
        }
        if (inputType == eInputType.MoveRightKey)
        {
            EventManager.BroadcastPlayerMove(Vector2.right);
        }
        else if (inputType == eInputType.MoveLeftKey)
        {
            EventManager.BroadcastPlayerMove(Vector2.left);
        }

    }
}
