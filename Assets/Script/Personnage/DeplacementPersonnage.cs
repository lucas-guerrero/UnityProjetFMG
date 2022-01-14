using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeplacementPersonnage : MonoBehaviour
{
    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private Text textInteract;
    [SerializeField] private GameObject maxInteract;
    [SerializeField] private Image imageComputer;
    [SerializeField] private Canvas pause;
    [Space]
    [SerializeField] private float speedMove = 6f;
    [SerializeField] private float speedCamera = 500f;
    [SerializeField] private float maxViewVertical = 90f;
    [SerializeField] private float minViewVertical = -90f;

    private Transform transformPlayer;
    private Light torchLight;
    private GameObject torch;
    private float viewCameraVertical = 0f;
    private Rigidbody rb;

    private bool takeFlash = false;

    private bool isTakeObject = false;
    private GameObject ObjectTaking;

    private int mode = 0; // GameMode: -1 -> Pause, 0 -> Game, 1 -> Text


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        transformPlayer = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        imageComputer.enabled = false;
        pause.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       switch (mode) {
            case -1:
               break;
            case 1:
                textMode();
                break;
            default:
                gameMode();
                break;
       }
    }

    void gameMode()
    {
        Cursor.visible = false;

        Vector3 start = transform.position;
        Vector3 direc = transform.TransformDirection(Vector3.forward);

        int layerMask = 1 << 8;
        RaycastHit hit;

        bool isInteract = Physics.Linecast(cameraPlayer.transform.position, maxInteract.transform.position, out hit, layerMask);

        if(isInteract) {
            textInteract.enabled = true;
        }
        else textInteract.enabled = false;

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

        if(Input.GetAxis("Cancel") != 0)
        {
            mode = -1;
            Cursor.visible = true;
            textInteract.enabled = false;
            showPause();
        }

        if(Input.GetKeyDown("f")) {
            if(takeFlash) torchLight.enabled = !torchLight.enabled;
        }

        if(Input.GetKeyDown("e")) {
            if(isTakeObject)
            {
                dropObject();
            }
            else if (isInteract)
            {
                GameObject objectInteract = hit.collider.gameObject;
                if(objectInteract.GetComponent<Interaction>() != null)
                {
                    EInteract interaction = objectInteract.GetComponent<Interaction>().ToInteract();
                    if(interaction == EInteract.TORCH) takeTorch(objectInteract);
                    else if(interaction == EInteract.COMPUTER) computer();
                }
                else takeObject(objectInteract);
            }
        }
    }

    void showPause()
    {
        pause.enabled = true;
    }

    public void resumeGame()
    {
        pause.enabled = false;
        mode = 0;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void textMode()
    {
        if(Input.GetAxis("Cancel") != 0 || Input.GetKeyDown("e"))
        {
            mode = 0;
            imageComputer.enabled = false;
            Cursor.visible = false;
        }
    }

    void computer()
    {
        Cursor.visible = true;
        textInteract.enabled = false;
        mode = 1;
    }

    void dropObject() 
    {
        isTakeObject = false;

        textInteract.text = "Press E To Interact";

        ObjectTaking.transform.parent = null;
        ObjectTaking.GetComponent<Rigidbody>().isKinematic = false;
    }

    void takeObject(GameObject gameObject) 
    {
        isTakeObject = true;

        textInteract.text = "Press E To Drop";

        ObjectTaking = gameObject;
        ObjectTaking.GetComponent<Rigidbody>().isKinematic = true;
        ObjectTaking.transform.parent = cameraPlayer.transform;
    }

    void takeTorch(GameObject gameObject)
    {
        takeFlash = true;

        torch = gameObject;
        torch.transform.parent = cameraPlayer.transform;
        torch.transform.localPosition = new Vector3(0.47f, -0.20f, 0.39f);

        torch.transform.localRotation = Quaternion.Euler(93f, -4f, 0f);

        torchLight = this.gameObject.GetComponentInChildren<Light>();
    }
}
