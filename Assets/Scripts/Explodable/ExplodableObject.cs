using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{

    private float m_TerrainRevealRadius = 30.0f;

    private float m_ExecutionDelay = 3.0f;

    [SerializeField] GameObject ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForDelay");
        Destroy(gameObject, m_ExecutionDelay);
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(m_ExecutionDelay); 
        HurtSurroundingPlayers();
    }

    // Update is called once per frame
    void Update()
    {

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
