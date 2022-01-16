using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningDoor : MonoBehaviour
{
    private Animator animator;

    [SerializeField] public bool isEnable = false;

    [SerializeField] private Text textBloque;

    [SerializeField] private Mesh enable;
    [SerializeField] private Mesh disable;

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update() {
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        if(isEnable && meshCollider.sharedMesh != enable) {
            meshCollider.sharedMesh = enable;
            meshCollider.convex = false;
        }
        else if(!isEnable && meshCollider.sharedMesh != disable) {
            meshCollider.sharedMesh = disable;
            meshCollider.convex = true;
        }
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
        if(isEnable) open();
        else textBloque.enabled = true;
    }

    void OnTriggerExit() {
        if(isEnable) close();
        else textBloque.enabled = false;
    }
}
