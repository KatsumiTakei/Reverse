using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerStateType
{
    Move,
    Grab,
    Punch,
    Choke,
    Dead
}

public abstract class PlayerStateBase
{
    protected SpriteRenderer spriteRenderer = null;

    public PlayerStateBase(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public abstract void OnEnableState();

    public abstract void OnDisableState();

    public abstract void Update();

    
}
