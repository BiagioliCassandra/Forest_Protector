using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TestTheo : MonoBehaviour
{
    //Script CRASH TEST pour la transition de materiaux

    [Tooltip("Colors des environnements \n 1 = Green \n 2 = Violet \n 3 = Rouge")]
    public Color[] colors;
    [Tooltip("Position de l'avancer des demons")]
    public Transform positionDemon;
    [Tooltip("Position de l'avancer des humain")]
    public Transform positionHumain;

    private Renderer mesh;

    public bool actualize;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<Renderer>();

        if(!mesh)
        Debug.LogWarning("Pas de mesh");

        if(colors.Length != 2)
        Debug.LogWarning("pas assez de colors");
    }

    // Update is called once per frame
    void Update()
    {
        if (actualize)
        {
            //Calcule des distances
            float distDemon = transform.position.x - positionDemon.position.x;

            float distHumain = transform.position.x - positionHumain.position.x;

            //Index permettant de choisir
            float index = 0; //1green //2 humain //3demon

            //Verification
            if(distDemon >= 0 && distHumain >= 0)       //COTE DEMON
            {
                index = 3;
                Debug.Log(index + "COTE DEMON") ;
            }
            else if(distDemon <= 0 && distHumain >= 0)       //COTE FORET
            {
                index = 1;
                Debug.Log(index + "COTE FOREST") ;
            }
            else if(distDemon <= 0 && distHumain <= 0)       //COTE HUMAIN
            {
                index = 2;
                Debug.Log(index + "COTE HUMAIN") ;
            }   

            //instance de couleur
            Color colorInstance = new Color(0, 0, 0, 0); //Green Color

            //Recuperation de la bonne color + application
            if(index == 3)
            {
                Debug.Log("Materiaux Demon");

                colorInstance = new Color(colors[1].r, colors[1].g, colors[1].b, 1); //Green Color

                mesh.material.color = colorInstance;
            }
            else if (index == 1)
            {

                Debug.Log("Materiaux Foret");

                colorInstance = new Color(colors[0].r, colors[0].g, colors[0].b, 1); //Green Color

                mesh.material.color = colorInstance;

            }
            else if (index == 2)
            {

                Debug.Log("Materiaux Humain");

                colorInstance = new Color(colors[2].r, colors[2].g, colors[2].b, 1); //Green Color

                mesh.material.color = colorInstance;

            }
        }
    }
}
