using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JM_Tools;

public class RunningState : IState
{
    private StateMachine movementStateMachine;
    private PlayerExample player;

    public RunningState(StateMachine movementStateMachine, PlayerExample player)
    {
        this.movementStateMachine = movementStateMachine;
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("RunningState Enter");
    }

    public void OnExit()
    {
        Debug.Log("RunningState Exit");
    }

    public void Execute()
    {
        Debug.Log("RunningState Execute");
    }
}
