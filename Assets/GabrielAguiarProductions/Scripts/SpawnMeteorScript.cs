using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorScript : MonoBehaviour {

	public GameObject comet;
	public GameObject startPoint;
	public GameObject endPoint;
	public float delay;
	public float rateOfFire;
	public float radius;
	public float quantity;
	public float waves;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(SpawnVFX(comet, delay, rateOfFire));
        }
    }

	void Start () {
		StartCoroutine (SpawnVFX(comet, delay, rateOfFire));
	}

    IEnumerator SpawnVFX (GameObject vfx, float delay, float rateDelay){	
		for (int j = 0; j < waves; j++) { 	
			yield return new WaitForSeconds (delay);
			for (int i = 0; i < quantity; i++) {
				var startPos = startPoint.transform.position;
				if(radius != 0)
					startPos = new Vector3 (startPoint.transform.position.x + Random.Range (-radius, radius), startPoint.transform.position.y + Random.Range (-radius, radius), startPoint.transform.position.z + Random.Range (-radius, radius));					
				GameObject objVFX = Instantiate (vfx, startPos, Quaternion.identity) as GameObject;

				var endPos = endPoint.transform.position;
				if(radius != 0)
					endPos = new Vector3 (endPoint.transform.position.x + Random.Range (-radius, radius), endPoint.transform.position.y + Random.Range (-radius, radius), endPoint.transform.position.z + Random.Range (-radius, radius));
				RotateTo (objVFX, endPos);

				yield return new WaitForSeconds (rateDelay);
			}
		}
	}

	void RotateTo (GameObject obj, Vector3 destination ) {
		var direction = destination - obj.transform.position;
		var rotation = Quaternion.LookRotation (direction);
		obj.transform.localRotation = Quaternion.Lerp (obj.transform.rotation, rotation, 1);
	}

}
