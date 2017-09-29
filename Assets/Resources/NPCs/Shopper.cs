using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shopper : MonoBehaviour {
    public enum AnimationState { isIdling, isWalking, isTurning, isPickingHigh, isPickingMedium, isPickingLow }
    public Transform[] targets;

    private AnimationState state = AnimationState.isIdling;
    public string[] states;
    private Animator anim;

    private NavMeshAgent agent;

    private Transform target;

    private AnimationState savedState;
    private bool isInterrupted = false;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(0, 100);
        anim = GetComponentInChildren<Animator>();

        int i = Random.Range(0, targets.Length);

        target = targets[i];

        agent.destination = target.position;

        List<string> list = new List<string>();

        foreach (int value in System.Enum.GetValues(typeof(AnimationState)))
        {
            list.Add(((AnimationState)value).ToString());
        }

        states = list.ToArray();

        SetAnimationState(AnimationState.isWalking);
        isInterrupted = false;
    }

    void SetAnimationState(AnimationState value)
    {
        if (state == value) return;
        for (int i = 0; i < states.Length; i++)
        {
            if ((int)value == i)
                anim.SetTrigger(states[i]);
            else
                anim.ResetTrigger(states[i]);
        }
        state = value;
    }

    public void InterruptMovement(float interruptTime)
    {
        if (isInterrupted) return;
        savedState = state;
        SetAnimationState(AnimationState.isIdling);
        agent.isStopped = true;
        isInterrupted = true;
        Invoke("ResumeMovement", interruptTime);
    }

    void ResumeMovement()
    {
        SetAnimationState(savedState);
        agent.isStopped = false;
        isInterrupted = false;
    }

    // Update is called once per frame
    void Update () {
        if (isInterrupted) return;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    Vector3 targetDir = -target.forward;
                    float dot = Vector3.Dot(targetDir, transform.forward);
                    if (dot < 0.99f)
                    {
                        float step = agent.angularSpeed * Mathf.Deg2Rad * Time.deltaTime;
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                        transform.rotation = Quaternion.LookRotation(newDir);
                        SetAnimationState(AnimationState.isTurning);
                    }
                    else
                    {
                        if (state == AnimationState.isTurning)
                        {
                            int i = Random.Range(0, 3);
                            if (i == 0)
                                SetAnimationState(AnimationState.isPickingHigh);
                            else if (i == 1)
                                SetAnimationState(AnimationState.isPickingMedium);
                            else
                                SetAnimationState(AnimationState.isPickingLow);
                        }

                        if(state == AnimationState.isPickingHigh || state == AnimationState.isPickingMedium || state == AnimationState.isPickingLow)
                        {
                            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
                            if(info.IsName("end_of_pick"))
                            {

                                int i = Random.Range(0, targets.Length);
                                while(targets[i] == target)
                                {
                                    i = Random.Range(0, targets.Length);
                                }
                                target = targets[i];

                                agent.destination = target.position;
                                SetAnimationState(AnimationState.isWalking);

                            }
                        }
                    }
                }
            }
        }
    }
}
