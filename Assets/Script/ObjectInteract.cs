using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour, Interaction
{
    public EInteract ToInteract() {
        return EInteract.OBJECT_DYNAMIC;
    }
}
