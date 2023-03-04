using System;
using System.Collections.Generic;
using JM_Tools;

namespace StateMachineTest // Note: actual namespace depends on the project name.
{
    public enum StateTest
    {
        Idle,
        Walk,
        Run,
        Jump,
        Fall,
        Attack,
        Die
    }
    
    internal class Program
    {
        private static StateMachine<StateTest> stateMachine;
        
        static void Main(string[] args)
        {
            stateMachine = new StateMachine<StateTest>(StateTest.Idle);
            
            stateMachine.ChangeState(StateTest.Walk, () => true);
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            stateMachine.ChangeState(StateTest.Run, CheckIfCanRun);
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            stateMachine.ChangeState(StateTest.Jump, (CheckIfCanJump));
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            stateMachine.ChangeState(StateTest.Run, CheckIfCanRun);
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            stateMachine.ChangeState(StateTest.Jump, (CheckIfCanJump));
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            stateMachine.ChangeState(StateTest.Fall, () => true);
            Console.WriteLine(stateMachine.GetCurrentState().ToString());
            Console.WriteLine(stateMachine.GetPreviousState().ToString());
        }
        
        private static bool CheckIfCanJump()
        {
            if (stateMachine.GetPreviousState() == StateTest.Jump)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private static bool CheckIfCanRun()
        {
            if (stateMachine.GetPreviousState() == StateTest.Walk)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}