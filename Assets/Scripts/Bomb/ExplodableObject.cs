using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    private float m_BombExplosionDelay = 3f;
    private float m_ExplosionRadius = 30.0f;

    private void Start()
    {
        StartCoroutine("WaitForBombDelay");
        
    }

    IEnumerator WaitForBombDelay()
    {
        yield return new WaitForSeconds(m_BombExplosionDelay);
        Explode();
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius);
        foreach (Collider c in colliders)
        {
            PlayerAnimation playerAnimation = c.GetComponent<PlayerAnimation>();
            if (playerAnimation != null)
            {
                playerAnimation.MakePlayerFall();
            }
        }
    }
}
