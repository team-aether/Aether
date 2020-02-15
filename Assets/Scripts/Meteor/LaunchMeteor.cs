using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchMeteor : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MeteorPrefab;

    public float throwForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        AetherInput.GetPlayerActions().LaunchMeteor.performed += HandleLaunch;
    }

    // Update is called once per frame
    void HandleLaunch(InputAction.CallbackContext context)
    {
        GameObject meteor = Instantiate(m_MeteorPrefab, transform.position, transform.rotation);
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
