using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Humanoide : MonoBehaviour
{   
    [Header("States")]
    private NavMeshAgent navMeshAgent;


    [Header("Value")]
    public float speed;
    public float distanceForNextPoint;

    /// <summary>
    /// Start, recup componnent
    /// </summary>
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {

    }
}
