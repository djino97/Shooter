using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPivot : MonoBehaviour
{
    public GameObject pivot;

    public static bool isShoot;

    void Update()
    {
        if (isShoot)
        {
            pivot.transform.localPosition = new Vector3(7.63f, 2.9f, -1.76f);
        }
        else
        {
            pivot.transform.localPosition = new Vector3(0, 2f, 0);
        }
    }
}