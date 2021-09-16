using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetPosition : MonoBehaviour
{
    //SCRIPT CRASH TEST POUR RECUP LA POSITION DE LAVANCE DU TERRITOIRE

    public Transform avancerDemon;
    public float offsetmMn = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(avancerDemon.position);
        Debug.Log(transform.position);

        //float pos = 0;

        Vector3 offset = transform.right * (avancerDemon.localScale.x / 2f) * -1f * offsetmMn;
        Vector3 pos = avancerDemon.position + offset; //This is the position

        //pos = (transform.position.x - avancerDemon.position.x);

        transform.position = pos;

    }
}
