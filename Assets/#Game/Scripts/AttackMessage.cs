using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;


public class AttackMessage : MonoBehaviour
{
    SpriteRenderer spriteRenderer = null;
    Transform refTransform = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        refTransform = transform;
    }

    //private void OnEnable()
    //{
    //    EventManager.OnJudgeAttack += OnJudgeAttack;
    //}

    //private void OnDisable()
    //{
    //    EventManager.OnJudgeAttack -= OnJudgeAttack;
    //}

    void OnJudgeAttack(Vector3 playerPos, Vector3 mobPos, bool isVisible)
    {
        if(!isVisible)
        {
            spriteRenderer.color = Color.clear;
            return;
        }

        spriteRenderer.color = Color.white;
        refTransform.position = Vector3.Lerp(playerPos, mobPos, 0.5f) + new Vector3(0, 75, 0);
    }
}
