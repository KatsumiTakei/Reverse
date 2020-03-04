using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMobStateType
{
    Move,
    Eat,
    Choke,
    Erase,
    Dead
}

public abstract class MobStateBase
{
    protected SpriteRenderer spriteRenderer = null;
    protected Sprite[] sprites = null;

    public MobStateBase(SpriteRenderer spriteRenderer, Sprite []sprites)
    {
        this.spriteRenderer = spriteRenderer;
        this.sprites = sprites;
    }

    public abstract void OnEnableState();

    public abstract void OnDisableState();

    public abstract void Update();

    
}
