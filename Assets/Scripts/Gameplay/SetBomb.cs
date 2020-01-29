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

    private void HandleSetBomb(InputAction.CallbackContext context)
    {
        Instantiate(_bombPrefab, transform.position, transform.rotation);
    }
}
