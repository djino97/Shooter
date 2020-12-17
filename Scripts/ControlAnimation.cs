using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : MonoBehaviour
{
    Animator animator; // перменная для аниматора
    public GameObject pivot;

    public Avatar avatarSamba;   // авватар танца
    public Avatar avatarRunnig;  // стандартный аватар
    public Avatar avatarJumping; // аватар прыжка
    public Avatar avatarOfShooting;

    bool playSambaDancing = false;
    bool playFight = false;
    bool playJumping = false;
    bool playRunning = false;
    bool rightRunning;
    bool leftRunning;
    float LastAngleRight;

    bool isShootRun;
    bool shoot;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //Срабатывает анимация стрельбы на месте при нажатии левой кнопки мыши
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RunningForward") && Input.GetButtonDown("Fire1"))
        {
            animator.avatar = avatarOfShooting;
            animator.SetBool("startRunningForward", false);
            animator.SetBool("startFight", true);
            ShootAnimation.isShoot = true;
            playFight = true;
            ShootAnimation.playAudio = true;
            shoot = true;

            gameObject.GetComponent<RigidBodyController>().enabled = false;
        }

        //Срабатывает анимация танца персонажа, происходит смена стандартного аватара
        // на аватар с импортированной анимацией танца Sambo,
        //Выключается скрипт управления персонажем RigidBodyController
        if (Input.GetKeyDown("f"))
        {
            animator.avatar = avatarSamba;
            animator.SetBool("startSambaDancing", true);
            animator.applyRootMotion = true;
            shoot = false;
            gameObject.GetComponent<RigidBodyController>().enabled = false;
        }

        //Условие срабатывает при остановке анимации танца, происходит смена аватара на стандартый,
        //Включается скрипт управления персонажем RigidBodyController
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") && playSambaDancing)
        {
            animator.SetBool("startSambaDancing", false);
            animator.applyRootMotion = false;
            animator.avatar = avatarRunnig;
            playSambaDancing = false;
            shoot = false;
            gameObject.GetComponent<RigidBodyController>().enabled = true;

        }

        //Анимация стрельбы  персонажа отключается
        if(Input.GetButtonUp("Fire1") && playFight)
        {
            animator.avatar = avatarRunnig;
            animator.SetBool("startFight", false);
            ShootAnimation.isShoot = false;
            playFight = false;
            shoot = false;
            gameObject.GetComponent<RigidBodyController>().enabled = true;
        }

        // if (Input.GetButtonDown("Fire1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        // {
        //     playFight = true;
        // }

         if (Input.GetKeyUp("f") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            playSambaDancing = true;
            shoot = false;
        }

        // Срабатывает анимация бега вперед
        if (Input.GetKeyDown("w"))
        {
            animator.avatar = avatarRunnig;
            shoot = false;
            animator.SetBool("startRunningForward", true);
            animator.SetBool("startFight", false);
            ShootAnimation.isShoot = false;
            animator.SetBool("startSambaDancing", false);
            gameObject.GetComponent<RigidBodyController>().enabled = true;
        }

        //Анимация бега вперед отключается
        if (Input.GetKeyUp("w"))
            animator.SetBool("startRunningForward", false);
            
        //Срабатывает анимация стрельбы в беге
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunningForward") && Input.GetButtonDown("Fire1"))
        {
            isShootRun = true;
            
            shoot = true;
            ShootAnimation.playAudio = true;
            ShootAnimation.isShoot = true;
            animator.SetBool("isRunShoot", true);
            animator.SetBool("startFight", false);
            animator.SetBool("startSambaDancing", false);
            gameObject.GetComponent<RigidBodyController>().enabled = true;
        }

        if (isShootRun && Input.GetButtonUp("Fire1"))
        {
            isShootRun = false;
            shoot = false;
            ShootAnimation.isShoot = false;
            animator.SetBool("isRunShoot", false);
        }


        // Срабатывает анимация бега назад
        if (Input.GetKeyDown("s"))
        {
            animator.avatar = avatarRunnig;
            animator.SetBool("startRunningBackward", true);
            animator.SetBool("startRunningForward", false);
            animator.SetBool("startFight", false);
            ShootAnimation.isShoot = false;
            animator.SetBool("startSambaDancing", false);
            gameObject.GetComponent<RigidBodyController>().enabled = true;
        }

        if (Input.GetKeyDown("d"))
        {
            rightRunning = true;
        }

        if(rightRunning)
        {
            this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y + 90, 0);
            rightRunning = false;
        }

        if (Input.GetKeyDown("a"))
        {
            leftRunning = true;
        }

        if(leftRunning)
        {
            this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y - 90, 0);
            leftRunning = false;
        }

        if(shoot)
        {
            this.transform.rotation = Quaternion.Euler(pivot.transform.rotation.eulerAngles.x - 10, this.transform.rotation.eulerAngles.y,this.transform.rotation.eulerAngles.z);
        }

        // Отключается анимация бега назад
        if (Input.GetKeyUp("s"))
            animator.SetBool("startRunningBackward", false);

        //Срабатывает анимация прыжка персонажа, меняется стандартный аватар
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.avatar = avatarJumping;
            animator.SetBool("startRunningForward", true);
            animator.SetBool("startJumping", true);
            playJumping = true;
        }

        // Анимация прыжка персонажа отключается и включается анимация бега вперед
        if(playJumping && animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
        {
            animator.SetBool("startJumping", false);
            playJumping = false;
            playRunning = true;
        }
        else if(playRunning && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
        {
            animator.avatar = avatarRunnig;
            animator.SetBool("startRunningForward", true);
            playRunning = false;
        }
    }
}
