using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerater : MonoBehaviour
{
    const int GenerateTime = 60;
    int generateCnt = 0;

    void Start()
    {
        generateCnt = 0;
    }

    void Update()
    {
        if(generateCnt++ > GenerateTime)
            GenerateMob();
    }

    void GenerateMob()
    {
        if (EventManager.BroadcastIsCreateLimit())
            return;

        var mob = MobManager.Instance.Create();
        mob.transform.position = transform.position;

        generateCnt = -Random.Range(120, 240);

        EventManager.BroadcastCreateMob();
    }

}
