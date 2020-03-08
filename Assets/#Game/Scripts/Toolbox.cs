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
        AudioManager.PlayBGM(ResourcesPath.Audio.BGM._battle);
    }

    IEnumerator CoLateStart()
    {
        yield return null;
        yield return null;


    }

    void Update()
    {
        InputManager.ManualUpdate();
    }
}
