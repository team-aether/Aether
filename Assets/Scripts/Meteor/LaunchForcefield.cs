using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchForcefield : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ForceFieldPrefab;
    // Start is called before the first frame update
    void Start()
    {
        AetherInput.GetPlayerActions().LaunchForcefield.performed += HandleLaunch;
    }

    // Update is called once per frame
    void HandleLaunch(InputAction.CallbackContext context)
    {
        Instantiate(m_ForceFieldPrefab, transform.position, transform.rotation);
    }
}
