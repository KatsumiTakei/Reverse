using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(WayPointManager))]
public class Toolbox : MonoBehaviour
{

    private void Start()
    {
        //AudioManager.PlayBGM();
    }

    void Update()
    {
        InputManager.ManualUpdate();
    }
}
