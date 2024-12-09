using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public float distance;
    public Transform Player;
    public NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, Player.position); 
        if(distance<5)
        {
            navMeshAgent.destination = Player.position;
        }  
    }
}
