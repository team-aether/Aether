using UnityEngine;
using UnityEngine.InputSystem;

public class SetBomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private Canvas m_Clock;

    private void Start()
    {
        AetherInput.GetPlayerActions().SetBomb.performed += HandleSetBomb;
    }

    private void HandleSetBomb(InputAction.CallbackContext context)
    {
        Instantiate(_bombPrefab, transform.position, transform.rotation);
        m_Clock.GetComponent<CanvasGroup>().alpha = 1;
        ClockUI clockUI = m_Clock.GetComponentInChildren<ClockUI>();
        if(clockUI != null)
        {
            clockUI.HandleBombSet();
        }
    }
}
