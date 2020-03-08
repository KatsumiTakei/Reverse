using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;

public class PlayScene : SceneBase
{

    [SerializeField]
    MobGenerater[] mobGeneraters = null;

    int generaterActiveCnt = 0;
    const int GeneraterActiveTime = 60 * 8;

    private void OnEnable()
    {
        ActiveGenerater();
    }

    void OnDisable()
    {
        for (int i = 0; i < mobGeneraters.Length; i++)
        {
            mobGeneraters[i].enabled = false;
        }
    }

    void Update()
    {
        if (generaterActiveCnt++ > GeneraterActiveTime)
        {
            ActiveGenerater();
            generaterActiveCnt = 0;
        }
    }

    void ActiveGenerater()
    {
        var sleepGeneraters = System.Array.FindAll(mobGeneraters, generater => !generater.enabled);
        if (sleepGeneraters.Length > 0)
            sleepGeneraters[Random.Range(0, sleepGeneraters.Length)].enabled = true;
    }

    public override void Open()
    {
        enabled = true;
    }

    public override void Close()
    {
        enabled = false;
    }
}
