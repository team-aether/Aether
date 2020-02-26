using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SpellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        AetherInput.GetPlayerActions().SpellCast.performed += HandleSpell;
    }

    // Update is called once per frame
    void HandleSpell(InputAction.CallbackContext ctx)
    {
        m_SpellPrefab.SetActive(true);
        m_SpellPrefab.GetComponent<ParticleSystem>().Play();
    }
}
