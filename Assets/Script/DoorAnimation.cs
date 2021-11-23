using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator animator;

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void interactOn() {
        if(isOpen) {
            isOpen = false;
            animator.Play("Base Layer.door_1_close");
        }
        else {
            isOpen = true;
            animator.Play("Base Layer.door_1_open");
        }
    }
}
