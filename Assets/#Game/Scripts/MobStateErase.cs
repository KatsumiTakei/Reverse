using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobStateErase : MobStateBase
{
    GradientSprite gradientSprite = null;
    Sprite idleSprite = null;

    public MobStateErase(SpriteRenderer spriteRenderer, Sprite[] sprites, GradientSprite gradientSprite) : base(spriteRenderer, sprites)
    {
        idleSprite = spriteRenderer.sprite;
        this.gradientSprite = gradientSprite;
    }

    public override void OnEnableState()
    {
        gradientSprite.enabled = true;
        spriteRenderer.sprite = idleSprite;
        spriteRenderer.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderer.gameObject.transform.DOLocalMoveY(50f, 1f).SetRelative();

        gradientSprite.SetAlphaGradient(1f);
        MobManager.Instance.Remove(spriteRenderer.gameObject.GetInstanceID());

        EventManager.BroadcastEraseMob();

    }

    public override void OnDisableState()
    {
        gradientSprite.enabled = false;
    }

    public override void Update()
    {
    }

}
