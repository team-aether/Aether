using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Destruction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Meteor"))
        {
            Debug.Log("METEORRRRR");
            GameObject meteorHit = Instantiate(m_Destruction, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(meteorHit, 1.5f);
        }
    }
}
