using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefuseBomb : MonoBehaviour
{

    private bool m_CanDefuse = false;

    private void Start()
    {
        AetherInput.GetPlayerActions().DefuseBomb.performed += HandleDefuseBomb;
    }
    public void AllowBombDefusal()
    {
        m_CanDefuse = true;
    }

    private void HandleDefuseBomb(InputAction.CallbackContext context)
    {
        if(!m_CanDefuse)
        {
            return;
        }

        GameObject[] mushroomBomb = GameObject.FindGameObjectsWithTag("BombMushroom");
        if (mushroomBomb.Length < 1)
        {
            return;
        }
        else
        {
            Destroy(mushroomBomb[0]);
        }

    }
}
