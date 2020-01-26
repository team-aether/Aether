using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{

    private float m_TerrainRevealRadius = 30.0f;
    private static float m_TimeOfLastSFX = 0.0f;

    private int timeDlay = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeDlay);
    }

    void OnDestroy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_TerrainRevealRadius);

        foreach (Collider c in colliders)
        {
            RevealableTerrain target = c.GetComponent<RevealableTerrain>();
            
            if (target != null) {
                target.PaintAtPosition(transform.position, m_TerrainRevealRadius);
            }

            RevealableObject theObject = c.GetComponent<RevealableObject>();

            if (theObject != null) {
               theObject.Reveal();
            }

            PlayerAnimation anim = c.GetComponent<PlayerAnimation>();
            
            // I would need a trigger to call Fallen which calls the Falling and getting up once 
            if (anim != null) {
                anim.MakeCharacterFall();
                // theMovement.m_Animator.SetTrigger("Fallen");
            }
        }
    }
}
