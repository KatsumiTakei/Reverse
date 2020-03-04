using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;

public class WayPointManager : SingletonMonoBehaviour<WayPointManager>
{
    [SerializeField]
    WayPoint[] wayPoints = null;

    private void Start()
    {
        Random.InitState((int)Time.time);
    }

    public WayPoint GetRandomDestination()
    {
        return wayPoints[Random.Range(0, wayPoints.Length)];
    }

}
