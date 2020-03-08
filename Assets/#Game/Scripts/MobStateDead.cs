using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateDead : MobStateBase
{
    SpriteRenderer tarfetSpriteRenderer = null;
    Collider2D collider2D = null;
    VomitAnim vomit = null;

    public MobStateDead(SpriteRenderer spriteRenderer, Sprite[] sprites, Collider2D collider2D, VomitAnim vomit, SpriteRenderer tarfetSpriteRenderer) : base(spriteRenderer, sprites)
    {
        this.collider2D = collider2D;
        this.vomit = vomit;
        this.tarfetSpriteRenderer = tarfetSpriteRenderer;
    }

    public override void OnEnableState()
    {
        //EventManager.BroadcastJudgeAttack(Vector3.zero, Vector3.zero, false);

        tarfetSpriteRenderer.gameObject.SetActive(false);
        vomit.gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[0];
        collider2D.enabled = false;
        MobManager.Instance.Remove(spriteRenderer.gameObject.GetInstanceID());

        EventManager.BroadcastDeadMob();
    }

    public override void OnDisableState()
    {
        collider2D.enabled = true;
    }

    public override void Update()
    {
    }
}
