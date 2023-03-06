using System;
using System.Collections;
using System.Collections.Generic;

namespace JM_Tools
{

    public interface IState
    {
        public void OnEnter();
        public void Execute();
        public void OnExit();
    }

    public class StateMachine
    {
        private IState currenIState;
        private List<IState> previousStates;
        private int maxPreviousStates = 10;

        public StateMachine(IState initialState)
        {
            currenIState = initialState;
            previousStates = new List<IState>
            {
                Capacity = maxPreviousStates
            };
        }

        public StateMachine(IState initialState, int _maxPreviousStates)
        {
            currenIState = initialState;
            maxPreviousStates = _maxPreviousStates;
            previousStates = new List<IState>
            {
                Capacity = maxPreviousStates
            };
        }

        public StateMachine()
        {
            currenIState = default(IState);
            previousStates = new List<IState>
            {
                Capacity = maxPreviousStates
            };
        }

        public void ChangeState(IState _newState)
        {
            if (currenIState != null)
            {
                currenIState.OnExit();
            }

            previousStates.Insert(0, currenIState);
            
            if (previousStates.Count > maxPreviousStates)
            {
                previousStates.TrimExcess();
            }

            currenIState = _newState;
            currenIState.OnEnter();
        }

        public void Update()
        {
            if (currenIState != null)
            {
                currenIState.Execute();
            }
        }

        public IState GetCurrenIState()
        {
            return currenIState;
        }

        public IState GetPreviousState()
        {
            return previousStates[0];
        }

        public List<IState> GetPreviousStates()
        {
            return previousStates;
        }

        public IState GetPreviousState(int index)
        {
            return previousStates[index];
        }

        private bool CanTransition(Func<bool> funcToCheck)
        {
            return funcToCheck();
        }
    }
}