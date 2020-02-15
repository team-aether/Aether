using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MeteorPrefab;
    [SerializeField]
    private Transform m_StartPointTransform;
    [SerializeField]
    private Transform m_EndPointTransform;

    private Vector3 m_StartPoint;
    private Vector3 m_EndPoint;

    private void Start()
    {
        m_StartPoint = m_StartPointTransform.position;
        GameObject meteor = Instantiate(m_MeteorPrefab, m_StartPoint, Quaternion.identity);

        m_EndPoint = m_EndPointTransform.position;

        RotateTo(meteor, m_EndPoint);
    }

    private void RotateTo(GameObject meteor, Vector3 endPoint)
    {
        Vector3 travelDirection = endPoint - meteor.transform.position;
        Quaternion rotation = Quaternion.LookRotation(travelDirection);

        meteor.transform.localRotation = Quaternion.Lerp(meteor.transform.rotation, rotation, 1);
    }
}
