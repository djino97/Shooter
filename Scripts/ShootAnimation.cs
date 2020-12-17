using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAnimation : MonoBehaviour
{
    Animator animator; // перменная для аниматора
    public GameObject smokeObj;
    public GameObject sparksObj;
    public GameObject impactEffect;
    public Camera fpsCam;
    public GameObject targetObj;
    public AudioSource audioSource;

    public static bool isShoot;
    public float range = 1000f;
    public static bool playAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        smokeObj.SetActive(false);
        sparksObj.SetActive(false);
    }

    void Update()
    {
       animator.SetBool("IsShoot", isShoot);

       if (isShoot)
       {
           smokeObj.SetActive(true);
           sparksObj.SetActive(true);
           targetObj.SetActive(true);

           ControlPivot.isShoot = isShoot;
           
           if(playAudio)
           {
               audioSource.Play();
               playAudio = false;
           }

           Shoot();
       }
       else
       {cscsv
           playAudio = false;
           audioSource.Stop();
           ControlPivot.isShoot = isShoot;
           smokeObj.SetActive(false);
           sparksObj.SetActive(false);
           targetObj.SetActive(false);
       }
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if(hit.transform.name != "Soldier")
            {
                GameObject impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactObj, 0.2f);
            }
        }
    }
}