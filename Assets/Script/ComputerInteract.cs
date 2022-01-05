using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : MonoBehaviour
{

    public bool isActived = false;
    public Material close;

    private Light LightComputer;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allComputer = GameObject.FindGameObjectsWithTag("computerLight");
        foreach (GameObject child in allComputer)
        {
            if(child.transform.IsChildOf(transform)) LightComputer = child.GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseComputer() {
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
}
