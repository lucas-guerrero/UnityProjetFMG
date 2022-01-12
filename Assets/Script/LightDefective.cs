using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDefective : MonoBehaviour
{
    [SerializeField] private Material closeMaterial;
    [SerializeField] private Material openMaterial;

    [Space]

    [SerializeField] private float TimeOfAlt = 0.05f;
    [SerializeField] private float TimeOfDefective = 2f;
    [SerializeField] private float TimeOfDisable = 5f;

    private bool IsActivate = false;

    private float CurrentTimeActivate;
    private float CurrentTimeAlt;

    private Light Light;
    private MeshRenderer rendere;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTimeActivate = TimeOfDisable;
        CurrentTimeAlt = TimeOfAlt;
        Light = GetComponentInChildren<Light>();
        rendere = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTimeActivate -= Time.deltaTime;
        
        if(CurrentTimeActivate <= 0f) {
            CurrentTimeAlt = TimeOfAlt;
            if(IsActivate) {
                CurrentTimeActivate = TimeOfDisable;
                Light.enabled = false;
            }
            else {
                CurrentTimeActivate = TimeOfDefective;
                Light.enabled = true;
            }

            IsActivate = !IsActivate;
        }

        if(IsActivate) {
            CurrentTimeAlt -= Time.deltaTime;
            if(CurrentTimeAlt <= 0f) {
                CurrentTimeAlt = TimeOfAlt;
                Light.enabled = !Light.enabled;
            }
        }

        if(Light.enabled) turnLight(openMaterial);
        else turnLight(closeMaterial);
    }

    void turnLight(Material material) {
        Material[] newMaterials = new Material[3];

        for (int i=0; i< 3; ++i) {
            newMaterials[i] = rendere.materials[i];
        }
        newMaterials[2] = material;
        rendere.materials = newMaterials;
    }
}
