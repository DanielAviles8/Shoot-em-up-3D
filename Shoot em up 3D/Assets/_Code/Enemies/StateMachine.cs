using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StateMachine
{
    private protected StateNode CurrentStateNode;
    protected Dictionary<Type, StateNode> StateNodes = new ();
    public List<ITransition> AnyTransitions = new ();
    public void Update()
    {
        var trans = GetTransition();
        if (trans != null)
        {
            ChangeState(trans.TargetState);
        }
        CurrentStateNode.TransitionState.Update();
    }
    public ITransition GetTransition()
    {
        foreach (var trans in AnyTransitions)
        {
            if (trans.Condition.Evaluate())
            {
                return trans;
            }
        }
        foreach(var trans in CurrentStateNode.transitions)
        {
            if (trans.Condition.Evaluate())
            {
                return trans;
            }
        }
        return null;
    }
    public void ChangeState(IState newState)
    {
        if(newState == CurrentStateNode.TransitionState)
        {
            return;
        }
        var previousState = CurrentStateNode.TransitionState;
        var nextState = StateNodes[newState.GetType()];
        previousState.OnExit();
        nextState.TransitionState.OnEnter();
        CurrentStateNode = StateNodes[newState.GetType()];
    }
    public void SetState(IState state)
    {
        CurrentStateNode = StateNodes[state.GetType()];
        CurrentStateNode.TransitionState?.OnEnter();
    }
    private StateNode GetOrAddNode(IState state)
    {
        var node = StateNodes.GetValueOrDefault(state.GetType());

        if (node == null)
        {
            node = new StateNode(state);
            StateNodes.Add(state.GetType(), node);
        }

        return node;
    }
    public void AddTransition(IState from, IState to, IPredicate condition)
    {
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).TransitionState, condition);
    }
    public void AddAnyTransition(ITransition transition)
    {
        AnyTransitions.Add(transition);
        //recordar hacer los predicate de las any transitions falsos en su enter
    }
    public class StateNode
    {
        public IState TransitionState { get; }

        public List<ITransition> transitions;

        public StateNode(IState targetState)
        {
            TransitionState = targetState;
            transitions = new List<ITransition>();
        }
        public void AddTransition(IState targetState, IPredicate condition)
        {
            transitions.Add(new Transition(targetState, condition));
        }
    }
}