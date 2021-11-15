using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersonnage : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private float speedCamera;
    private Transform transformObject;

    // Start is called before the first frame update
    void Start()
    {
        transformObject = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) {
            transformObject.Translate(0, 0, speedMove*Time.deltaTime);
        }
        if(Input.GetKey("down")) {
            transformObject.Translate(0, 0, -speedMove * Time.deltaTime);
        }
        if(Input.GetKey("left")) {
            transformObject.Translate(-speedMove * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("right")) {
            transformObject.Translate(speedMove * Time.deltaTime, 0, 0);
        }
        if(Input.GetAxis("Mouse X")<0){
            transformObject.Rotate(0, -speedCamera * Time.deltaTime, 0);
        }
        if(Input.GetAxis("Mouse X")>0){
            transformObject.Rotate(0, speedCamera * Time.deltaTime, 0);
        }
    }
}
