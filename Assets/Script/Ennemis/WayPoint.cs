using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    //Script qui permet de récupérez les points de passages

    #region Variable

    [Tooltip("Transform des points de passages")]
    public Transform[] position;

    #endregion

    #region Built In Methods

    /// <summary>
    /// récupération des points de passages
    /// </summary>
    void Awake()
    {
        position = new Transform[transform.childCount];

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = transform.GetChild(i);
        }
    }

    #endregion
}
