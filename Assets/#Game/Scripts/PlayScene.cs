using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;

public class PlayScene : SingletonMonoBehaviour<PlayScene>
{
    [SerializeField]
    MobGenerater[] mobGeneraters = null;

    int generaterActiveCnt = 0;
    const int GeneraterActiveTime = 60 * 10;

    public bool CanAttack { private set; get; } = false;


    private void OnEnable()
    {
        //EventManager.OnJudgeAttack += OnJudgeAttack;
    }

    private void OnDisable()
    {
        //EventManager.OnJudgeAttack -= OnJudgeAttack;
    }

    private void Start()
    {
        ActiveGenerater();
    }

    void Update()
    {
        if(generaterActiveCnt++ > GeneraterActiveTime)
        {
            ActiveGenerater();
            generaterActiveCnt = 0;
        }
    }

    void OnJudgeAttack(Vector3 playerPos, Vector3 mobPos, bool isVisible)
    {
        CanAttack = isVisible;
    }

    void ActiveGenerater()
    {
        var sleepGeneraters = System.Array.FindAll(mobGeneraters, generater => !generater.enabled);
        if(sleepGeneraters.Length > 0)
            sleepGeneraters[Random.Range(0, sleepGeneraters.Length + 1)].enabled = true;
    }
}
