using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour {

    //public float speed; 

    //private Vector3 lastPosition;

    public GameManager gameManager;

    private int hitEnv = 0;
    private int hitNPC = 0;

    //private Collider lastCollider = null;
    //private int lastCollisionFrameCount = 0;

    private Dictionary<Collider, int> activeColliderFrameCounter = new Dictionary<Collider, int>();

	// Use this for initialization
	void Start () {
        //lastPosition = transform.position;
        //lastCollider = null;
        //lastCollisionFrameCount = 0;

        activeColliderFrameCounter = new Dictionary<Collider, int>();

        hitNPC = 0;
        hitEnv = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //speed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;
        //lastPosition = transform.position;
        UpdateGameManager();
	}

    void UpdateGameManager()
    {
        if (gameManager.hitEnvironment != hitEnv) gameManager.hitEnvironment = hitEnv;
        if (gameManager.hitPeople != hitNPC) gameManager.hitPeople = hitNPC;
    }

    void FixedUpdate()
    {
        //if(lastCollider != null)
        //{
        //    if (lastCollisionFrameCount >= 20)
        //        lastCollider = null;
        //    else
        //        lastCollisionFrameCount++;
        //}
        List<Collider> lapsed = new List<Collider>();
        List<Collider> colliders = new List<Collider>(activeColliderFrameCounter.Keys);
        foreach (Collider collider in colliders)
        {
            if (activeColliderFrameCounter[collider] >= 10)
                lapsed.Add(collider);
            else
                activeColliderFrameCounter[collider]++;
        }
        foreach (Collider collider in lapsed)
            activeColliderFrameCounter.Remove(collider);    
     }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.name == "FloorCollider") return;
        //if (hit.collider == lastCollider)
        //    lastCollisionFrameCount = 0;
        //else //register a new hit
        //{
        //    lastCollider = hit.collider;
        //    lastCollisionFrameCount = 0;
        //    if (hit.collider.name == "NPCCollider")
        //    {
        //        gameManager.InterruptShopper(hit.collider.GetComponentInParent<Shopper>());
        //        hitNPC++;
        //    }
        //    else
        //        hitEnv++;
        //    //Debug.Log(hit.collider.name);
        //}
        if (activeColliderFrameCounter.ContainsKey(hit.collider))
            activeColliderFrameCounter[hit.collider] = 0;
        else //register new hit
        {
            activeColliderFrameCounter.Add(hit.collider, 0);
            if (hit.collider.name == "NPCCollider")
            {
                gameManager.InterruptShopper(hit.collider.GetComponentInParent<Shopper>());
                hitNPC++;
            }
            else
                hitEnv++;
        }

    }
}
