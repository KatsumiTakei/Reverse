using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerStateType
{
    Move,
    Grab,
    Punch
}

public abstract class PlayerStateBase
{
    protected SpriteRenderer spriteRenderer = null;
    protected Sprite[] sprites = null;

    public PlayerStateBase(SpriteRenderer spriteRenderer, Sprite []sprites)
    {
        this.spriteRenderer = spriteRenderer;
        this.sprites = sprites;
    }

    public abstract void OnEnableState();

    public abstract void OnDisableState();

    public abstract void Update();

    
}
