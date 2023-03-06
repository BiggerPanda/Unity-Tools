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

    public class StateMachine<TState> where TState : IState
    {
        private TState currentState;
        private List<TState> previousStates;
        private int maxPreviousStates = 10;

        public StateMachine(TState initialState)
        {
            currentState = initialState;
            previousStates = new List<TState>
            {
                Capacity = maxPreviousStates
            };
        }

        public StateMachine(TState initialState, int _maxPreviousStates)
        {
            currentState = initialState;
            maxPreviousStates = _maxPreviousStates;
            previousStates = new List<TState>
            {
                Capacity = maxPreviousStates
            };
        }

        public StateMachine()
        {
            currentState = default(TState);
            previousStates = new List<TState>
            {
                Capacity = maxPreviousStates
            };
        }

        public void ChangeState(TState _newState, Func<bool> _checkIfCanChange)
        {
            if (!Enum.IsDefined(typeof(TState), _newState))
            {
                throw new ArgumentException($"Invalid state: {_newState}");
            }

            if (CanTransition(_checkIfCanChange))
            {
                previousStates.Insert(0, currentState);
                currentState = _newState;

                if (previousStates.Count > maxPreviousStates)
                {
                    previousStates.TrimExcess();
                }
            }
            else
            {
                // too strict, should be a warning
                // throw new InvalidOperationException($"Invalid transition: {currentState} -> {_newState}");

                Console.WriteLine($"Invalid transition: {currentState} -> {_newState}");
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }

        public TState GetCurrentState()
        {
            return currentState;
        }

        public TState GetPreviousState()
        {
            return previousStates[0];
        }

        public List<TState> GetPreviousStates()
        {
            return previousStates;
        }

        public TState GetPreviousState(int index)
        {
            return previousStates[index];
        }

        private bool CanTransition(Func<bool> funcToCheck)
        {
            return funcToCheck();
        }
    }
}