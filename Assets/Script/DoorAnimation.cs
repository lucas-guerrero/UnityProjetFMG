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

    private void open() {
        if(!isOpen) {
            isOpen = true;
            animator.Play("Base Layer.door_1_open");
        }
    }

    private void close() {
        if(isOpen) {
            isOpen = false;
            animator.Play("Base Layer.door_1_close");
        }
    }

    void OnTriggerEnter() {
        open();
    }

    void OnTriggerExit() {
        close();
    }
}
