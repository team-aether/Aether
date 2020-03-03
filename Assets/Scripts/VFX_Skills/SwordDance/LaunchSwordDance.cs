﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Code here is for Testing only.
 * Game play should use a reference to the player's reticle.
 */
public class LaunchSwordDance : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SwordDanceStart;
    [SerializeField]
    private GameObject m_SwordDanceEnd;
    [SerializeField]
    private GameObject m_SwordDanceCollision;
    [SerializeField]
    private LayerMask m_Layers;

    void Start()
    {
        AetherInput.GetPlayerActions().Fire.performed += HandleSwordLaunch;
    }

    private void HandleSwordLaunch(InputAction.CallbackContext ctx)
    {
        StartCoroutine(SwordAttack());
    }

    IEnumerator SwordAttack()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0.0f;
        Ray ray = new Ray(Camera.main.transform.position + new Vector3(0, 2, 0), Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_Layers))
        {
            GameObject swordStart = Instantiate(m_SwordDanceStart, transform);
            
            yield return new WaitForSeconds(2f);
            Destroy(swordStart, 1f);
            GameObject swordEnd = Instantiate(m_SwordDanceEnd, hit.point, Quaternion.LookRotation(forward));
            Destroy(swordEnd, 7.0f);
        }
    }
}
