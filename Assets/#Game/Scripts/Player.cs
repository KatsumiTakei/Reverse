using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerStateBase currentState = null;
    PlayerStateBase[] stateArray = null;


    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        stateArray = new PlayerStateBase[] {
            new PlayerStateMove(spriteRenderer),
            new PlayerStateGrab(spriteRenderer),
            new PlayerStatePunch(spriteRenderer)
        };
    }

    void Update()
    {
        currentState.Update();
    }

    void ChangeState(ePlayerStateType stateType)
    {
        int type = (int)stateType;

        currentState.OnDisableState();
        currentState = stateArray[type];
        currentState.OnEnableState();

    }

    void OnPlayerMove(Vector2 moveValue)
    {
        if(moveValue.x > 0)
        {
        }
        else
        {
        }
    }
}
