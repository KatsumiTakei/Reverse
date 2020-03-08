using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Sprite[] moveSprites = null;

    [SerializeField]
    Sprite[] grabSprites = null;

    [SerializeField]
    Sprite[] punchSprites = null;
    
    [SerializeField]
    ContactFilter2D contactFilter = (default);

    PlayerStateBase currentState = null;
    PlayerStateBase[] stateArray = null;

    List<int> attackedMobInsanceId = new List<int>();

    private void OnEnable()
    {
        EventManager.OnChangePlayerState += OnChangePlayerState;
    }
    private void OnDisable()
    {
        EventManager.OnChangePlayerState -= OnChangePlayerState;
    }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        stateArray = new PlayerStateBase[] {
            new PlayerStateMove(transform, contactFilter, spriteRenderer, moveSprites),
            new PlayerStateGrab(spriteRenderer, grabSprites),
            new PlayerStatePunch(spriteRenderer, punchSprites)
        };

        OnChangePlayerState(ePlayerStateType.Move);

    }

    void Update()
    {
        currentState.Update();

    }

    void OnChangePlayerState(ePlayerStateType stateType)
    {
        int type = (int)stateType;

        currentState?.OnDisableState();
        currentState = stateArray[type];
        currentState?.OnEnableState();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var mob = collision.gameObject.GetComponent<Mob>();
    //    if (mob.CurrentStateType.Equals(eMobStateType.Choke))
    //    {
    //        attackedMobInsanceId.Add(collision.gameObject.GetInstanceID());
    //        //EventManager.BroadcastJudgeAttack(transform.position, collision.transform.position, true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    attackedMobInsanceId.Remove(collision.gameObject.GetInstanceID());

    //    //EventManager.BroadcastJudgeAttack(Vector3.zero, Vector3.zero, false);
    //}


}
