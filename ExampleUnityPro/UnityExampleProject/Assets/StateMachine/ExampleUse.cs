using System;
using System.Collections.Generic;
using JM_Tools;

namespace StateMachineTest // Note: actual namespace depends on the project name.
{
    
    public class WalkingState : IState
    {
        public void OnEnter()
        {
            Console.WriteLine("Entered Walking State");
        }

        public void Execute()
        {
            Console.WriteLine("Walking");
        }

        public void OnExit()
        {
            Console.WriteLine("Exited Walking State");
        }
    }

    public class RunningState : IState
    {
        public void OnEnter()
        {
            Console.WriteLine("Entered Running State");
        }

        public void Execute()
        {
            Console.WriteLine("Running");
        }

        public void OnExit()
        {
            Console.WriteLine("Exited Running State");
        }
    }

    internal class Program
    {
        private static StateMachine movementStateMachine;
        
        static void Main(string[] args)
        {
            movementStateMachine = new StateMachine(new WalkingState());

            movementStateMachine.Update();
            movementStateMachine.Update();
            movementStateMachine.Update();

            movementStateMachine.ChangeState(new RunningState());

            movementStateMachine.Update();
            movementStateMachine.Update();
            movementStateMachine.Update();

            movementStateMachine.ChangeState(new WalkingState());
        }
        

    }
}