using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateDead : MobStateBase
{
    Collider2D collider2D = null;
    VomitAnim vomit = null;

    public MobStateDead(SpriteRenderer spriteRenderer, Sprite[] sprites, Collider2D collider2D, VomitAnim vomit) : base(spriteRenderer, sprites)
    {
        this.collider2D = collider2D;
    }

    public override void OnEnableState()
    {
        //EventManager.BroadcastJudgeAttack(Vector3.zero, Vector3.zero, false);

        vomit.gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[0];
        collider2D.enabled = false;
        MobManager.Instance.Remove(spriteRenderer.gameObject.GetInstanceID());
    }

    public override void OnDisableState()
    {
        collider2D.enabled = true;
    }

    public override void Update()
    {
    }
}
