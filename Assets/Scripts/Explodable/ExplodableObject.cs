using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{

    private float m_TerrainRevealRadius = 3.0f;

    public const float STANDARD_BOMB_DELAY  = 3.0f; // Constnat Value

    private float m_ExecutionDelay;

    private bool m_hasExploded = false;

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
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        // TODO: Implement particle effect with vfx graph

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_TerrainRevealRadius);

        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                Vector3 finalPosition = c.transform.position + (c.transform.position - this.gameObject.transform.position).normalized * 2;
                finalPosition.y = c.transform.position.y;
                c.transform.position = Vector3.Lerp(c.transform.position, finalPosition, 0.6f);
                PlayerAnimation anim = c.GetComponent<PlayerAnimation>();
                if (anim != null)
                {
                    anim.MakeCharacterFall();
                }
            }
        }
    }
}
