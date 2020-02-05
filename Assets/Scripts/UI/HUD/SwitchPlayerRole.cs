using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SwitchPlayerRole : MonoBehaviour
{
    [SerializeField]
    private GameObject m_FirstRole;
    [SerializeField]
    private GameObject m_SecondaryRole;
    private void Start()
    {
        AetherInput.GetPlayerActions().SwitchRole.performed += HandleSwitchRole;
    }

    private void HandleSwitchRole(InputAction.CallbackContext context)
    {
        Sprite firstRole = m_FirstRole.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite;
        Sprite secondRole = m_SecondaryRole.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite;

        if (firstRole == null || secondRole == null)
        {
            return;
        }
        m_FirstRole.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = secondRole;
        m_SecondaryRole.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = firstRole;
    }
}
