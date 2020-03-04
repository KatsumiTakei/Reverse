using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateGrab : PlayerStateBase
    ,IInputResponder
{

    public PlayerStateGrab(SpriteRenderer spriteRenderer, Sprite[] sprites) : base(spriteRenderer, sprites)
    {
    }

    public override void Update()
    {
    }

    public override void OnEnableState()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
        spriteRenderer.sprite = sprites[0];
    }

    public override void OnDisableState()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }

    public void OnMultipleInput(eInputType inputType, sbyte index, eHIDType hIDType)
    {
        if(inputType == eInputType.AttackAndDecideKeyDown)
        {
            EventManager.BroadcastChangePlayerState(ePlayerStateType.Punch);
        }
    }
}
