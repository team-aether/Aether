using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaturesController : MonoBehaviour {

	public bool automaticSpawn;
	public float delay;
	public Text effectName;
	public List<GameObject> VFXs = new List<GameObject> ();
	public List<GameObject> firePoints = new List<GameObject> ();

	private Animator anim;
	private GameObject effectToSpawn;
	private int count;

	void Start () {
		if(VFXs.Count > 0)
			effectToSpawn = VFXs[0];
		
		anim = GetComponent<Animator> ();

		if (effectName != null) effectName.text = effectToSpawn.name;

		if (automaticSpawn) StartCoroutine (AutomatedSpawn());
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			if(VFXs.Count > 0 && !automaticSpawn)
				StartCoroutine (SpawnVFX ());
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			anim.SetTrigger ("Attack01");
			if(VFXs.Count > 0 && !automaticSpawn)
				StartCoroutine (SpawnVFX ());
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			anim.SetTrigger ("Attack02");
			if(VFXs.Count > 0 && !automaticSpawn)
				StartCoroutine (SpawnVFX ());
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			anim.SetTrigger ("Attack03");
			if(VFXs.Count > 0 && !automaticSpawn)
				StartCoroutine (SpawnVFX ());
		}
		if (Input.GetKeyDown (KeyCode.D) && !automaticSpawn)
			Next ();
		if (Input.GetKeyDown (KeyCode.A) && !automaticSpawn) 
			Previous ();	
	}

	IEnumerator SpawnVFX () {
		yield return new WaitForSeconds (delay);
		GameObject vfx = null;

		if (firePoints.Count > 0) {
			if (firePoints.Count == 1) {
				vfx = Instantiate (effectToSpawn, firePoints [0].transform.position, Quaternion.identity);
				vfx.transform.SetParent (firePoints [0].transform);
			}
			else {
				for(int i = 0; i< firePoints.Count ; i++){
					GameObject vfx2 = Instantiate (effectToSpawn, firePoints [i].transform.position, Quaternion.identity);
					vfx2.transform.SetParent (firePoints [i].transform);
					var ps2 = vfx2.GetComponent<ParticleSystem> ();
					if (ps2 != null)
						Destroy (vfx2, ps2.main.duration + ps2.main.startLifetime.constantMax);
					else {
						var psChild2 = vfx2.transform.GetChild (0).GetComponent<ParticleSystem> ();
						Destroy (vfx2, psChild2.main.duration + psChild2.main.startLifetime.constantMax);
					}
				}
				yield break;
			}
		}
		else
			vfx = Instantiate (effectToSpawn);

		var ps = vfx.GetComponent<ParticleSystem> ();
		if (ps != null)
			Destroy (vfx, ps.main.duration);
		else {
			if (vfx.transform.childCount > 0) {
				var psChild = vfx.transform.GetChild (0).GetComponent<ParticleSystem> ();
				Destroy (vfx, psChild.main.duration + psChild.main.startLifetime.constantMax);
			}
		}
	}

	public void Next () {
		count++;

		if (count > VFXs.Count)
			count = 0;

		for(int i = 0; i < VFXs.Count; i++){
			if (count == i)	effectToSpawn = VFXs [i];
			if (effectName != null)	effectName.text = effectToSpawn.name;
		}
	}

	public void Previous () {
		count--;

		if (count < 0)
			count = VFXs.Count;

		for (int i = 0; i < VFXs.Count; i++) {
			if (count == i) effectToSpawn = VFXs [i];
			if (effectName != null)	effectName.text = effectToSpawn.name;
		}
	}

	IEnumerator AutomatedSpawn (){
		for(int i = 0; i<VFXs.Count; i++){

			effectToSpawn = Instantiate (VFXs [i]);
			effectToSpawn.transform.SetParent (firePoints [0].transform);
			effectToSpawn.transform.localPosition = Vector3.zero;

			var ps = effectToSpawn.GetComponent<ParticleSystem> ();
			if (ps != null)
				Destroy (effectToSpawn, ps.main.duration);
			else {
				if (effectToSpawn.transform.childCount > 0) {
					var psChild = effectToSpawn.transform.GetChild (0).GetComponent<ParticleSystem> ();
					Destroy (effectToSpawn, psChild.main.duration + psChild.main.startLifetime.constantMax);
				}
			}

			if (effectName != null) effectName.text = effectToSpawn.name;

			yield return new WaitForSeconds (4);

			Destroy (effectToSpawn);
		}

		StartCoroutine (AutomatedSpawn());
	}
}
