using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobStateChoke : MobStateBase
{
    GradientSprite gradientSprite = null;
    Transform transform = null;

    int instanceId = 0;

    public MobStateChoke(SpriteRenderer spriteRenderer, Sprite[] sprites, GradientSprite gradientSprite, Transform transform, int instanceId) : base(spriteRenderer, sprites)
    {
        this.transform = transform;
        this.gradientSprite = gradientSprite;
        this.instanceId = instanceId;
    }

    public override void OnEnableState()
    {
        gradientSprite.enabled = true;
        spriteRenderer.sprite = sprites[0];
        transform.DOShakePosition(2f, 10f);
    }

    public override void OnDisableState()
    {
        gradientSprite.enabled = false;
    }

    public override void Update()
    {
        if (!gradientSprite.IsFinishGradient())
            return;

        EventManager.BroadcastChangeMobState(eMobStateType.Erase, instanceId);
    }
}
