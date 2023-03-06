using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM_Tools
{
    public class IdleState : IState
    {
        private StateMachine movementStateMachine;
        private PlayerExample player;

        public IdleState(StateMachine movementStateMachine, PlayerExample player)
        {
            this.movementStateMachine = movementStateMachine;
            this.player = player;
        }

        public void OnEnter()
        {
            Debug.Log("IdleState Enter");
        }

        public void OnExit()
        {
            Debug.Log("IdleState Exit");
        }

        public void Execute()
        {
            Debug.Log("IdleState Execute");
        }
    }
}