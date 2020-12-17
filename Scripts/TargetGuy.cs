using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGuy : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.transform.Rotate(-5, 0, 0, Space.Self);
    }
}