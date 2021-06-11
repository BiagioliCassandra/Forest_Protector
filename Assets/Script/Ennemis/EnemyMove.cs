using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //Script permettant a notre enemy de se déplacer sur la carte selon des points de passages grace au nav mesh agent

    #region Variable

    [Header("Nav Mesh Agent")]
    NavMeshAgent agent;
    private Transform target;

    [Tooltip("A referencier : WayPoint que va suivre l'ennemy")]
    public WayPoint wayPoint;
    private int waypointsIndex = 0;


    private Humanoide humain;

    #endregion

    #region Built In methods

    /// <summary>
    /// On récupèr les composants qu'on as besoin ainsi que la première cible
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        humain = GetComponent<Humanoide>();

        if(wayPoint == null)
        Debug.LogError("Pas de Chemin assigné");

        target = wayPoint.position[0];
        
        humain.speed = 1;

        agent.SetDestination(target.position);
        
    }

    /// <summary>
    /// Udpate on vas vérif si il arrive a destination
    /// /// </summary>
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= humain.distanceForNextPoint)
        {
            Debug.Log("getnext");
            GetNextWaypoint();
        }
        
    }

    #endregion

    #region CustomBuilt

    /// <summary>
    /// Ici on vas récupère le prochain point de passage de l'enmy
    /// </summary>
    private void GetNextWaypoint()
    {   
        //Si on arrive au dernier point
        if (waypointsIndex >= wayPoint.position.Length - 1)
        {
            waypointsIndex = 0;
            target = wayPoint.position[0];
            agent.SetDestination(target.position);
            return;
        }
        
        //Maj du point
        waypointsIndex++;
        target = wayPoint.position[waypointsIndex];
        agent.SetDestination(target.position);
    }

    #endregion
}

