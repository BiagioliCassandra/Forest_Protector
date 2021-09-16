using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    //Script qui vas gerez les Zones

    public int  index;
    public bool capture;
    public bool inCapture;
    public float tempsPourCapture;
    float time;

    [Tooltip("Zone voisine \n 0.Zone actuelle \n les autres zones commencent en haut a gauche et tounre en horloge")]
    public Zone[] voisinZone; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //On entre et on lance les captures
            inCapture = true;
        }
    }

    private void Update()
    {
        if (inCapture)
        {
            time += Time.deltaTime;

            if (time >= tempsPourCapture)
            {
                capture = true;
            }
        }

        if(capture)
        {

        }
    }

    //Capture 
    private void Capture()
    {
        
    }
}
