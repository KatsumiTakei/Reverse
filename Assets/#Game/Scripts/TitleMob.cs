using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMob : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites = null;

    SpriteRenderer spriteRenderer = null;
    VomitAnim vomit = null;

    int animCnt = 0;
    const int AnimCntMax = 60;

    bool isVomiting = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vomit = GetComponentInChildren<VomitAnim>();
        vomit.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isVomiting)
            return;

        if (animCnt++ < AnimCntMax)
            return;

        animCnt = 0;
        spriteRenderer.sprite = sprites[0];
        vomit.gameObject.SetActive(false);
        isVomiting = false;

    }

    public void Vomit()
    {
        isVomiting = true;
        spriteRenderer.sprite = sprites[1];
        vomit.gameObject.SetActive(true);
    }

    public bool GetVomiting()
    {
        return isVomiting;
    }
}
