using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AvancerTerritoire : MonoBehaviour
{

    public Transform plane;
    
    public bool coteDemon;

    public float scale;


    // Start is called before the first frame update
    void Start()
    {
        if(!plane)
        Debug.LogWarning("PAS DE TRANSFORM");
    }

    // Update is called once per frame
    void Update()
    {   

        float dist = transform.position.x - plane.position.x;

        if(coteDemon)
        dist = dist * -1;

        

        Debug.Log(dist);
        plane.localScale = new Vector3(dist * scale, plane.localScale.y, plane.localScale.z);
    }
}
