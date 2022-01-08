using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPersonnage : MonoBehaviour
{
    [SerializeField] public Camera cameraPlayer;

    [SerializeField] public Canvas canvas;

    [SerializeField] private float speedMove = 6f;
    [SerializeField] private float speedCamera = 500f;
    [SerializeField] private float maxViewVertical = 90f;
    [SerializeField] private float minViewVertical = -90f;
    [SerializeField] private float maxArm = 2.5f;

    private Transform transformPlayer;
    private Light light; 
    private float viewCameraVertical = 0f;
    private CharacterController controller;

    [SerializeField] public GameObject computer;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        transformPlayer = GetComponent<Transform>();
        light = cameraPlayer.GetComponentInChildren<Light>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!controller.isGrounded) {
            Vector3 dir = transform.TransformDirection(Vector3.down);
            controller.Move(dir * speedMove * Time.deltaTime);
        }

        if(Input.GetKey("z")) {
            Vector3 dir = transform.TransformDirection(Vector3.forward);
            controller.Move(dir * speedMove * Time.deltaTime);
        }

        if(Input.GetKey("s")) {
            Vector3 dir = transform.TransformDirection(Vector3.back);
            controller.Move(dir * speedMove * Time.deltaTime);
        }

        if(Input.GetKey("q")) {
            Vector3 dir = transform.TransformDirection(Vector3.left);
            controller.Move(dir * speedMove * Time.deltaTime);
        }

        if(Input.GetKey("d")) {
            Vector3 dir = transform.TransformDirection(Vector3.right);
            controller.Move(dir * speedMove * Time.deltaTime);
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

        if(Input.GetKeyDown("f")) {
            light.enabled = !light.enabled;
        }

        if(Input.GetKeyDown("e")) {
            //computer.GetComponent<ComputerInteract>().ToInteract();

            Vector3 start = transform.position;
            Vector3 dir = transform.TransformDirection(Vector3.forward);

            int layerMask = 1 << 8;
            RaycastHit hit;

            if (Physics.Raycast(start, dir, out hit, maxArm, layerMask))
            {
                Debug.Log("Did Hit");
                GameObject objectInteract = hit.collider.gameObject;
                objectInteract.GetComponent<Interaction>().ToInteract();
            }
        }
    }
}
