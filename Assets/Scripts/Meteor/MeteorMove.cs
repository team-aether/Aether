using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private GameObject m_ImpactPrefab;
    [SerializeField]
    private Transform m_EndPoint;
    [SerializeField]
    private Transform m_StartPoint;

    private void Update()
    {
        transform.position = Vector3.Lerp(m_StartPoint.position, m_EndPoint.position, 2.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_Speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion impactRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 impactPosition = contact.point;

        if (m_ImpactPrefab != null)
        {
            GameObject impactObject = Instantiate(m_ImpactPrefab, impactPosition, impactRotation);
        }

        Destroy(gameObject);
    }
}
