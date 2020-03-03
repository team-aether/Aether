using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Code here is for Testing only.
 * Game play should use a reference to the player's reticle.
 */
public class SpellHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SpellPrefab;

    void Start()
    {
        // For play test purpose
        //AetherInput.GetPlayerActions().Fire.performed += HandleSpell;
    }

    // For test purpose only
    void HandleSpell(InputAction.CallbackContext ctx)
    {
        // Current spell is Play On Awake for both the EnergyStart and the Spell, Spell plays after Delay of 1.5 Seconds
        Instantiate(m_SpellPrefab, this.transform);
    }
}
