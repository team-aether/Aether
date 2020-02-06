using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SwitchPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PrimaryPickUp;
    [SerializeField]
    private GameObject m_SecondaryPickUp;
    private void Start()
    {
        AetherInput.GetPlayerActions().SwitchPickUp.performed += HandleSwitchPickUp;
    }

    private void HandleSwitchPickUp(InputAction.CallbackContext context)
    {
        Sprite primaryPickUp = m_PrimaryPickUp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        Sprite secondaryPickUp = m_SecondaryPickUp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;

        if (primaryPickUp == null || secondaryPickUp == null)
        {
            return;
        }
        m_PrimaryPickUp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = secondaryPickUp;
        m_SecondaryPickUp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = primaryPickUp;
    }
}

