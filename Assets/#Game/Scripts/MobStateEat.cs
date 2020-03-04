using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateEat : MobStateBase
{
    const int EatingLimit = 60;
    int eatingTime = 0;
    int instanceId = 0;

    int animCnt = 0;

    public MobStateEat(SpriteRenderer spriteRenderer, Sprite[] sprites, int instanceId) : base(spriteRenderer, sprites)
    {
        this.instanceId = instanceId;
    }

    public override void OnEnableState()
    {
    }

    public override void OnDisableState()
    {

    }

    public override void Update()
    {

        if (animCnt++ % 6 == 0)
            spriteRenderer.sprite = (spriteRenderer.sprite.Equals(sprites[0])) ? sprites[1] : sprites[0];

        if (eatingTime++ > EatingLimit)
            EventManager.BroadcastChangeMobState(eMobStateType.Choke, instanceId);
    }
}

