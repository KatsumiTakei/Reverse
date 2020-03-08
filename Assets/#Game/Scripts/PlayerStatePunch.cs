using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePunch : PlayerStateBase
{
    int freazeCnt = 0;
    const int FreazeLimit = 10;

    public PlayerStatePunch(SpriteRenderer spriteRenderer, Sprite[] sprites) : base(spriteRenderer, sprites)
    {
    }

    public override void Update()
    {
        if (freazeCnt++ >= FreazeLimit)
            EventManager.BroadcastChangePlayerState(ePlayerStateType.Move);
    }

    public override void OnEnableState()
    {
        spriteRenderer.sprite = sprites[0];
        freazeCnt = 0;
        AudioManager.Instance.PlaySE(ResourcesPath.Audio.SE._BombSe);
    }

    public override void OnDisableState()
    {
    }

}
