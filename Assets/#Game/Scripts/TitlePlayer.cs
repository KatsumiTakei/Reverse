using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites = null;

    [SerializeField]
    ContactFilter2D contactFilter = (default);
    BoxCollider2D boxCollider2D = null;
    SpriteRenderer spriteRenderer = null;

    bool isMove = false;
    int animCnt = 0;
    const int AnimCntMax = 10;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animCnt++;

        if (isMove)
        {
            if (animCnt % 3 == 0)
                spriteRenderer.sprite = (spriteRenderer.sprite.Equals(sprites[0])) ? sprites[1] : sprites[0];

            transform.position += Vector3.right * 2;
            if(transform.localPosition.x >= 7f)
                transform.localPosition = new Vector3(-7f, transform.localPosition.y);

            var mobCollider = GetNearHitMob();
            if (!mobCollider)
                return;

            var mob = mobCollider.gameObject.GetComponent<TitleMob>();
            if (!mob)
                return;

            if (mob.GetVomiting())
                return;

            mob.Vomit();
            spriteRenderer.sprite = sprites[2];
            isMove = false;
            animCnt = 0;
        }
        else
        {
            if (animCnt > AnimCntMax)
            {
                isMove = true;
                spriteRenderer.sprite = sprites[0];
            }
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
}
