using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateBase
    , IInputResponder
{
    float spd = 2f;
    int animCnt = 0;

    Transform transform = null;
    Sprite idleSprite = null;
    BoxCollider2D boxCollider2D = null;
    ContactFilter2D contactFilter = (default);


    Vector3 moveValue = Vector3.zero;
    readonly Vector3 rightMove = new Vector3(1, 1, 1);
    readonly Vector3 leftMove = new Vector3(-1, 1, 1);



    public PlayerStateMove(Transform transform, ContactFilter2D contactFilter, SpriteRenderer spriteRenderer, Sprite[] sprites) : base(spriteRenderer, sprites)
    {
        this.contactFilter = contactFilter;
        this.transform = transform;
        idleSprite = spriteRenderer.sprite;

        boxCollider2D = transform.GetComponent<BoxCollider2D>();

    }

    public override void Update()
    {
        animCnt++;

        transform.position += moveValue;
        if (moveValue.Equals(Vector2.zero))
        {
            spriteRenderer.sprite = idleSprite;
        }
        else
        {
            if (animCnt % 3 == 0)
                spriteRenderer.sprite = (spriteRenderer.sprite.Equals(sprites[0])) ? sprites[1] : sprites[0];
        }

        moveValue = Vector2.zero;
    }

    public override void OnEnableState()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
        EventManager.OnDeadMob += OnDeadMob;
        EventManager.OnResetSpd += OnResetSpd;
    }

    public override void OnDisableState()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
        EventManager.OnDeadMob -= OnDeadMob;
        EventManager.OnResetSpd -= OnResetSpd;
    }

    public void OnMultipleInput(eInputType inputType, sbyte index, eHIDType hIDType)
    {
        if (inputType == eInputType.MoveDownKey)
        {
            MovePlayer(Vector2.down);
        }
        else if (inputType == eInputType.MoveUpKey)
        {
            MovePlayer(Vector2.up);
        }
        if (inputType == eInputType.MoveRightKey)
        {
            MovePlayer(Vector2.right);
        }
        else if (inputType == eInputType.MoveLeftKey)
        {
            MovePlayer(Vector2.left);
        }

        var mobCollider = GetNearHitMob();
        if (mobCollider)
        {
            var mob = mobCollider.gameObject.GetComponent<Mob>();
            if (mob)
            {
                EventManager.BroadcastJudgeAttack(transform.position, mob.transform.position, true);
                if (inputType == eInputType.AttackAndDecideKeyDown)
                {
                    EventManager.BroadcastChangeMobState(eMobStateType.Dead, mob.gameObject.GetInstanceID());
                    EventManager.BroadcastChangePlayerState(ePlayerStateType.Punch);
                }
            }
        }
        else
        {
            EventManager.BroadcastJudgeAttack(Vector3.zero, Vector3.zero, false);
        }
    }

    Collider2D GetNearHitMob()
    {
        Collider2D[] results = new Collider2D[5];
        int hitCount = boxCollider2D.OverlapCollider(contactFilter, results);
        if (hitCount == 0)
        {
            //Debug.Log("not found collider");
            return null;
        }

        Collider2D mob = System.Array.Find(results, element =>
        {
            if (element == null)
                return false;

            //Debug.Log(element.name + " : " + element?.tag);
            return element.CompareTag(TagName.Mob);
        });

        //Debug.Log(mob);
        return mob;
    }

    void MovePlayer(Vector2 moveValue)
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

    void OnDeadMob()
    {
        spd += 0.1f;
    }
    void OnResetSpd()
    {
        spd = 2f;
    }
}
