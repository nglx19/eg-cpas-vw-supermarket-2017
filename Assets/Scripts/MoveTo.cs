using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {
    public Transform goal;
    private NavMeshAgent agent;
    public Transform arrow;

    private Transform player;
    private float angle;

    // Use this for initialization
    void Start () {
        angle = 0.0f;
        player = transform.root;
        //agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
    }
	
	// Update is called once per frame
	void Update () {
        //agent.destination = goal.position;
        Vector3 targetDir = goal.position - player.position;
        float step = 3.14f;
        Vector3 newDir = Vector3.RotateTowards(player.forward, targetDir, step, 0.0F);
        angle = player.eulerAngles.y - Quaternion.LookRotation(newDir).eulerAngles.y ;
        //Debug.Log(angle);
        //agent.isStopped = true;
        arrow.eulerAngles = new Vector3(0,0,angle);
        
    }
}
