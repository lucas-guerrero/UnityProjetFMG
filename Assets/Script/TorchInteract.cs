using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteract : MonoBehaviour, Interaction
{
    public EInteract ToInteract() { return EInteract.TORCH; }
}
