using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchMeteor : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_LayerMask = new LayerMask();

    [SerializeField]
    private GameObject m_MeteorPrefab;

    private void Start()
    {
        //AetherInput.GetPlayerActions().MeteorLaunch.performed += HandleLaunchMeteor;
    }

    void HandleLaunchMeteor(InputAction.CallbackContext ctx)
    { 
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_LayerMask))
        {
            //Debug.DrawRay(Camera.main.transform.position, new Vector2(Screen.width / 2, Screen.height / 2), Color.green);
            Debug.Log(hit.transform.position);
            GameObject meteor = Instantiate(m_MeteorPrefab, hit.point, Quaternion.identity);
            Destroy(meteor, 7.0f);
        }
    }
}
