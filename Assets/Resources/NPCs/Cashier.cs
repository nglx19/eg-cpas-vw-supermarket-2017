using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("end_of_scan"))
        {
            bool finished = (Random.Range(0f, 1f) < 0.25f? true: false);
            anim.SetBool("finished", finished);
        }
    }
}
