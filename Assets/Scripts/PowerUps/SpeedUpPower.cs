using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c) 
    {
        if (c.CompareTag("Player"))
        {
            PlayerPowerUps powerUps = c.GetComponent<PlayerPowerUps>();

            if (powerUps != null && !powerUps.GetDoubleSpeed())
            {
                powerUps.GoFaster();
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
