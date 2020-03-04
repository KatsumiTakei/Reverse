using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{

    [SerializeField]
    Sprite[] moveSprites = null;

    [SerializeField]
    Sprite[] eraseSprites = null;

    [SerializeField]
    Sprite[] chokeSprites = null;

    [SerializeField]
    Sprite[] deadSprites = null;

    [SerializeField]
    Sprite[] eatSprites = null;

    MobStateBase currentState = null;
    MobStateBase[] stateArray = null;

    WayPoint target = null;

    int waitCnt = 0;
    const int WaitTime = 60;

    public eMobStateType CurrentStateType { private set; get; } = eMobStateType.Move;

    private void OnEnable()
    {
        EventManager.OnChangeMobState += OnChangeMobState;
    }
    private void OnDisable()
    {
        EventManager.OnChangeMobState -= OnChangeMobState;
    }


    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        var vomit = GetComponent<VomitAnim>();
        vomit.gameObject.SetActive(false);

        target = WayPointManager.Instance.GetRandomDestination();

        var gradient = GetComponent<GradientSprite>();
        gradient.enabled = false;

        int instanceId = gameObject.GetInstanceID();

        stateArray = new MobStateBase[] {

            new MobStateMove(sr, moveSprites, transform, target, instanceId, GetComponent<BoxCollider2D>()),
            new MobStateEat(sr, eatSprites, instanceId),
            new MobStateChoke(sr, chokeSprites, gradient, transform, instanceId),
            new MobStateErase(sr, eraseSprites, gradient),
            new MobStateDead(sr, deadSprites, GetComponent<Collider2D>(), vomit),

        };

        EventManager.BroadcastChangeMobState(eMobStateType.Move, instanceId);
    }

    void Update()
    {
        if (waitCnt++ < WaitTime)
            return;

        currentState.Update();
    }

    void OnChangeMobState(eMobStateType stateType, int instanceId)
    {
        if (gameObject.GetInstanceID() != instanceId)
            return;

        CurrentStateType = stateType;
        int type = (int)stateType;

        currentState?.OnDisableState();
        currentState = stateArray[type];
        currentState?.OnEnableState();
    }

    public bool IsSameTarget(WayPoint wayPoint)
    {
        return (wayPoint.Equals(target));
    }

}
