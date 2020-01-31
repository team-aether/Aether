using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DefuseBomb test = other.GetComponent<DefuseBomb>();
            if (test != null)
            {
                test.AllowBombDefusal();
            }
        }
    }
}
