using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateMove : MobStateBase
{
    WayPoint target = null;
    BoxCollider2D boxCollider2D = null;


    float spd = 1.75f;
    int animCnt = 0;
    int instanceId = 0;

    Transform transform = null;

    Vector3 moveValue = Vector3.zero;
    readonly Vector3 rightMove = new Vector3(1, 1, 1);
    readonly Vector3 leftMove = new Vector3(-1, 1, 1);

    public MobStateMove(SpriteRenderer spriteRenderer, Sprite[] sprites, Transform transform, WayPoint target, int instanceId, BoxCollider2D boxCollider2D) : base(spriteRenderer, sprites)
    {
        this.instanceId = instanceId;
        this.target = target;
        this.transform = transform;
        this.boxCollider2D = boxCollider2D;
    }

    public override void OnEnableState()
    {
        spriteRenderer.color = Color.white;
    }

    public override void OnDisableState()
    {
    }

    public override void Update()
    {
        MoveMob(new Vector2(
            Mathf.Atan(target.transform.position.x - transform.position.x),
            Mathf.Atan(target.transform.position.y - transform.position.y)));

        transform.position += moveValue;
        moveValue = Vector3.zero;

        if (animCnt++ % 3 == 0)
            spriteRenderer.sprite = (spriteRenderer.sprite.Equals(sprites[0])) ? sprites[1] : sprites[0];

        if (IsHit())
        {
            EventManager.BroadcastChangeMobState(eMobStateType.Eat, instanceId);
        }

    }

    bool IsHit()
    {
        Collider2D[] results = new Collider2D[5];
        int hitCount = boxCollider2D.OverlapCollider(new ContactFilter2D(), results);

        return System.Array.Find(results, element =>
        {
            if (element != null)
                return element.gameObject.GetInstanceID().Equals(target.gameObject.GetInstanceID());
            return false;
        }) != null;
    }

    void MoveMob(Vector2 moveValue)
    {
        if (moveValue.x > 0)
        {
            this.moveValue += Vector3.right * spd;
            transform.localScale = rightMove;
        }
        else if (moveValue.x < 0)
        {
            this.moveValue += Vector3.left * spd;
            transform.localScale = leftMove;
        }

        if (moveValue.y > 0)
        {
            this.moveValue += Vector3.up * spd;
        }
        else if (moveValue.y < 0)
        {
            this.moveValue += Vector3.down * spd;
        }
    }
}