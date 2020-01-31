using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c) 
    {
        if (c.CompareTag("Player"))
        {
            PlayerAnimation anim = c.GetComponent<PlayerAnimation>();

            if (anim != null)
            {
                anim.GetFlag();
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
