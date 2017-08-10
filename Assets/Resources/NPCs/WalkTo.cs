using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class WalkTo : MonoBehaviour {

    public enum AnimationState { isIdling, isWalking }
    public Transform target;
    public float speed;

    private AnimationState state = AnimationState.isIdling;
    public string[] states;
    private Animator anim;

    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        agent.destination = target.position;

        List<string> list = new List<string>();

        foreach (int value in Enum.GetValues(typeof(AnimationState)))
        {
            list.Add(((AnimationState)value).ToString());
        }

        states = list.ToArray();

        SetAnimationState(AnimationState.isIdling);
	}

    void SetAnimationState(AnimationState value)
    {
        for(int i = 0; i < states.Length; i++)
        {
            if ((int)value == i)
                anim.SetTrigger(states[i]);
            else
                anim.ResetTrigger(states[i]);
        }
        state = value;
    }
	
	// Update is called once per frame
	void Update () {
        speed = agent.velocity.magnitude;

        if(speed < 0.1f)
        {
            if(state != AnimationState.isIdling)
            {
                SetAnimationState(AnimationState.isIdling);
            }
        }
        else
        {
            if(state != AnimationState.isWalking)
            {
                SetAnimationState(AnimationState.isWalking);
            }
        }        
	}
}
