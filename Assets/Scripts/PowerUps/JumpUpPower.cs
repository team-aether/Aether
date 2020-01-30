using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c) 
    {
        Debug.Log("XD");
        Debug.Log(c);
        if (c.CompareTag("Player"))
        {
            PlayerAnimation anim = c.GetComponent<PlayerAnimation>();

            if (anim != null && !anim.hasJumpPowerUp())
            {
                anim.JumpHigher();
                Destroy(gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
