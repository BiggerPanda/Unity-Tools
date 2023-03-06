using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM_Tools
{
    public class WalkingState : IState
    {
        private StateMachine movementStateMachine;
        private PlayerExample player;

        public WalkingState(StateMachine movementStateMachine, PlayerExample player)
        {
            this.movementStateMachine = movementStateMachine;
            this.player = player;
        }

        public void OnEnter()
        {
            Debug.Log("WalkingState Enter");
        }

        public void OnExit()
        {
            Debug.Log("WalkingState Exit");
        }

        public void Execute()
        {
            Debug.Log("WalkingState Execute");
        }
    }
}
