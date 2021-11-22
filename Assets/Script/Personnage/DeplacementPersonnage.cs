using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersonnage : MonoBehaviour
{
    [SerializeField] public Camera cameraPlayer;

    [SerializeField] private float speedMove;
    [SerializeField] private float speedCamera = 200f;
    [SerializeField] private float maxViewVertical = 45f;
    [SerializeField] private float minViewVertical = -30f;

    private Transform transformPlayer;
    private float viewCameraVertical = 0f;
    private bool isCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        transformPlayer = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) {
            transformPlayer.Translate(0, 0, speedMove*Time.deltaTime);
        }

        if(Input.GetKey("down")) {
            transformPlayer.Translate(0, 0, -speedMove * Time.deltaTime);
        }

        if(Input.GetKey("left")) {
            transformPlayer.Translate(-speedMove * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey("right")) {
            transformPlayer.Translate(speedMove * Time.deltaTime, 0, 0);
        }

        if(Input.GetAxis("Mouse X") != 0f ){


            transformPlayer.Rotate(0, Input.GetAxisRaw("Mouse X") * 0.01f * speedCamera, 0);
        }

        if(Input.GetAxis("Mouse Y") != 0f){

            if(viewCameraVertical <= maxViewVertical && viewCameraVertical >= minViewVertical) {
                float tmp = Input.GetAxisRaw("Mouse Y");
                tmp *= -0.01f;

                viewCameraVertical += tmp * speedCamera;
                viewCameraVertical = Mathf.Clamp(viewCameraVertical, -89f, 89f);

                cameraPlayer.transform.localEulerAngles = new Vector3(viewCameraVertical, 0, 0);
            }
            else {
                if(viewCameraVertical > maxViewVertical) viewCameraVertical = maxViewVertical;
                else viewCameraVertical = minViewVertical;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        isCollision = true;
    }

    void OnCollisionExit(Collision collision) {
        isCollision = false;
    }
}
