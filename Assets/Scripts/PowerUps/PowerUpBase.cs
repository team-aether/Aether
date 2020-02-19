using System.Reflection;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField]
    protected const float m_BuffDuration = 5.5f;

    protected float m_TimeOfActivation = -1.0f;

    void Update()
    {
        if (m_TimeOfActivation != -1.0f && Time.time > m_TimeOfActivation + m_BuffDuration)
        {
            OnPowerUpExpired();
            Destroy(this);
        }
    }

    public void InitializePowerUp()
    {
        if (interactor != null && interactor is Player player)
        {
            PlayPickUpSound();
            HandlePowerUp(player);
            Destroy(gameObject);
        }
    }

    private void PlayPickUpSound()
    {
        m_TimeOfActivation = Time.time;
        OnPowerUpActivated();
    }
    
    public abstract void OnPowerUpActivated();

    public abstract void OnPowerUpExpired();
}
