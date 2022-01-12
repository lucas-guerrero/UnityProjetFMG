using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteract : MonoBehaviour, Interaction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EInteract ToInteract() { return EInteract.TORCH; }
}
