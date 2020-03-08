using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;

public class WayPointManager : SingletonMonoBehaviour<WayPointManager>
{
    [SerializeField]
    WayPoint[] wayPoints = null;

    [SerializeField]
    Sprite[] targetSprites = null;

    private void Start()
    {
        Random.InitState((int)Time.time);
    }

    public (WayPoint, Sprite) GetRandomDestination()
    {
        int index = Random.Range(0, wayPoints.Length);
        return (wayPoints[index], targetSprites[index]);
    }


}
