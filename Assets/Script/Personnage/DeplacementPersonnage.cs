using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeplacementPersonnage : MonoBehaviour
{
    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private Canvas uI;
    [SerializeField] private GameObject torch;
    [Space]
    [SerializeField] private float speedMove = 6f;
    [SerializeField] private float speedCamera = 500f;
    [SerializeField] private float maxViewVertical = 90f;
    [SerializeField] private float minViewVertical = -90f;
    [SerializeField] private float maxArm = 2.5f;
    [Space(10)]
    [SerializeField] private bool takeFlash = false;

    private Transform transformPlayer;
    private Light torchLight; 
    private float viewCameraVertical = 0f;
    private Rigidbody rb;

    private Text textInteract;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        transformPlayer = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        if(uI != null) textInteract = uI.GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 start = transform.position;
        Vector3 direc = transform.TransformDirection(Vector3.forward);

        int layerMask = 1 << 8;
        RaycastHit hit;

        bool isInteract = Physics.Raycast(start, direc, out hit, maxArm, layerMask);

        if(isInteract) {
            Debug.Log("Press E To Interact");
            textInteract.enabled = true;
        }
        else textInteract.enabled = false;

        //Debug.DrawLine(start, transform.TransformDirection(Vector3.forward), Color.green, 0.5f);

        Vector3 InputMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Vector3 dir = transform.TransformDirection(InputMovement) * speedMove;
        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);

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
            if(takeFlash) torchLight.enabled = !torchLight.enabled;
        }

        if(Input.GetKeyDown("e")) {
            if (isInteract)
            {
                GameObject objectInteract = hit.collider.gameObject;
                EInteract interaction = objectInteract.GetComponent<Interaction>().ToInteract();
                switch (interaction) {
                    case EInteract.OBJECT_DYNAMIC:
                        break;
                    case EInteract.COMPUTER: {
                        if(!takeFlash) takeTorch(); 
                        break;
                    }
                    default :
                        break;
                }
            }
        }
    }

    public void takeTorch()
    {
        takeFlash = true;

        torch.transform.parent = cameraPlayer.transform;
        torch.transform.localPosition = new Vector3(0.47f, -0.20f, 0.39f);
        torch.transform.rotation = Quaternion.Euler(93, 0, 0);
        //torch.transform.localPosition = new Vector3(0f, 0f, 0f);

        torchLight = gameObject.GetComponentInChildren<Light>();
    }
}
