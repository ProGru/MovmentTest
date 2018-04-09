using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorControlerKnight : MonoBehaviour {

    public Rigidbody rb;
    Vector3 staraPozycja;
    Animator animator;
    AnimatorStateInfo stateInfo;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        staraPozycja = rb.transform.position;
        animator = (Animator)GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {

        if (staraPozycja != rb.transform.position )
        {

            staraPozycja = rb.transform.position;
            animator.SetFloat("predkosc", 4);

        }
        else
        {
            animator.SetFloat("predkosc", 6);
        }
    }
}
