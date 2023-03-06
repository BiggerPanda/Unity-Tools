using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JM_Tools;

public class PlayerExample : MonoBehaviour
{
    private StateMachine movementStateMachine;
    private WalkingState walkingState;
    private IdleState idleState;
    private RunningState runningState;

    // Start is called before the first frame update
    void Start()
    {
        idleState = new IdleState(movementStateMachine, this);
        walkingState = new WalkingState(movementStateMachine, this);
        runningState = new RunningState(movementStateMachine, this);

        movementStateMachine = new StateMachine(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        movementStateMachine.Update();
    }

    private void HandleInput() // Next Tool Idea Input Handler on new Unity Input System
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            movementStateMachine.ChangeState(runningState);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            movementStateMachine.ChangeState(walkingState);
        }
        else
        {
            movementStateMachine.ChangeState(idleState);
        }
    }
}
