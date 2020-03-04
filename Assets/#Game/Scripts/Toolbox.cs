using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobManager))]
[RequireComponent(typeof(WayPointManager))]
public class Toolbox : MonoBehaviour
{

    void Update()
    {
        InputManager.ManualUpdate();
    }
}
