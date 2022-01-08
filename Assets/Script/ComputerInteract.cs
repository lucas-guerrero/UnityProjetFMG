using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : MonoBehaviour, Interaction
{

    [SerializeField] private bool isActived = false;
    [SerializeField] public Material close;

    private Light LightComputer;

    [SerializeField] private string tagDoor;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allComputer = GameObject.FindGameObjectsWithTag("computerLight");
        foreach (GameObject child in allComputer)
        {
            if(child.transform.IsChildOf(transform)) LightComputer = child.GetComponent<Light>();
        }
    }

    public void ToInteract() {
        if(!isActived) {
            CloseComputer();
            ActivateDoor();
            isActived = true;
        }
    }

    private void CloseComputer() {
        if(LightComputer != null) {
            LightComputer.enabled = !LightComputer.enabled;
        }

        MeshRenderer[] allMeshRenderer = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer childRenderer in allMeshRenderer)
        {
            Material[] newMaterials = new Material[4];

            for (int i=0; i< 4; ++i) {
                newMaterials[i] = childRenderer.materials[i];
            }
            if(close != null) newMaterials[3] = close;
            childRenderer.materials = newMaterials;
        }
    }

    private void ActivateDoor() {
        GameObject[] doors = GameObject.FindGameObjectsWithTag(tagDoor);
        foreach(GameObject door in doors) {
            door.GetComponent<DoorInteraction>().isEnable = true;
        }
    }
}
