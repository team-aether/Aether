using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{

    private float m_TerrainRevealRadius = 3.0f;

    public const float STANDARD_BOMB_DELAY  = 3.0f; // Constnat Value

    private float m_ExecutionDelay;

    private bool m_hasExploded = false;

    [SerializeField] GameObject ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        m_ExecutionDelay = STANDARD_BOMB_DELAY;
    }

    // Update is called once per frame
    void Update()
    {
        m_ExecutionDelay -= Time.deltaTime;
        if (m_ExecutionDelay <= 0 && !m_hasExploded) {
            m_hasExploded = true;
            HurtSurroundingPlayers();
            Destroy(gameObject);
        }
    }

    private void HurtSurroundingPlayers()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_TerrainRevealRadius);

        foreach (Collider c in colliders)
        {

            PlayerAnimation anim = c.GetComponent<PlayerAnimation>();
            
            if (anim != null) {
                anim.MakeCharacterFall();
            }
        }
    }
}
