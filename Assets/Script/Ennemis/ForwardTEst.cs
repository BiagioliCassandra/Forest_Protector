using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTEst : MonoBehaviour
{
    float t;
    public float timeToReach;

    Vector3 startPosition;

    Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime/timeToReach;


        transform.position = Vector3.Lerp(startPosition, target, t);
    }

    /// <summary>
    /// Set la destination du point
    /// </summary>
    /// <param name="destination">Destination a entre</param>
    /// <param name="time">Temps pour arriver a cette destination</param>
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReach = time;
        target = destination;

    }


}
