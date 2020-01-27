using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab;
    private void Start()
    {
        AetherInput.GetPlayerActions().SetBomb.performed += HandleSetBomb;
    }

    private void HandleSetBomb(InputAction.CallbackContext obj)
    {
        // Hold B on keyboard for 2 seconds, instantiate bomb-mushroom
        Instantiate(_bombPrefab, transform.position, transform.rotation);
    }
}
