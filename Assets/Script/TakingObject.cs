using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingObject : MonoBehaviour
{

    private GameObject Player;
    private float time;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        time = 0.1f;
    }

    void Update() {
        time -= Time.deltaTime;
    }

    void OnTriggerEnter()
    {
        if(time < 0) Player.GetComponent<DeplacementPersonnage>().dropObject();
    }
}
