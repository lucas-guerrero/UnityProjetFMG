using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerInteract : MonoBehaviour, Interaction
{

    [SerializeField] private bool isActived = false;
    [SerializeField] public Material close;
    [Space(10)]
    [SerializeField] private Sprite sprite;
    [SerializeField] private Canvas canvas;
    // [SerializeField] private Image imageComputer;
    // [SerializeField] private Text textOuverture;

    private Light LightComputer;

    [SerializeField] private string tagDoor;

    private Image imageComputer;
    private Text textOuverture;
    
    // Start is called before the first frame update
    void Start()
    {
        imageComputer = canvas.GetComponentInChildren<Image>();
        textOuverture = canvas.GetComponentInChildren<Text>();

        GameObject[] allComputer = GameObject.FindGameObjectsWithTag("computerLight");
        foreach (GameObject child in allComputer)
        {
            if(child.transform.IsChildOf(transform)) LightComputer = child.GetComponent<Light>();
        }
    }

    public EInteract ToInteract() {
        if(!isActived) {
            CloseComputer();
            ActivateDoor();

            isActived = true;

            canvas.enabled = true;

            imageComputer.sprite = sprite;
            textOuverture.text = "Les portes des " + tagDoor + " est débloquées";

            return EInteract.COMPUTER;
        }
        return EInteract.NONE;
    }

    private void CloseComputer() {
        if(LightComputer != null) {
            LightComputer.enabled = !LightComputer.enabled;
        }

        MeshRenderer[] allMeshRenderer = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer childRenderer in allMeshRenderer)
        {
            Material[] newMaterials = new Material[2];

            for (int i=0; i< 2; ++i) {
                newMaterials[i] = childRenderer.materials[i];
            }
            if(close != null) newMaterials[0] = close;
            childRenderer.materials = newMaterials;
        }
    }

    private void ActivateDoor() {
        GameObject[] doors = GameObject.FindGameObjectsWithTag(tagDoor);
        foreach(GameObject door in doors) {
            door.GetComponent<OpeningDoor>().isEnable = true;
        }
    }
}
