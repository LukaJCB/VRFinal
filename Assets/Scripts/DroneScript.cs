 using UnityEngine;
using System.Collections;

public class DroneScript : MonoBehaviour {

	public static Transform target;
	public static Rigidbody prefab;
	public static float shootingSpeed;
	/*
	 * AudioSource is now a part of the OSPAudioSource
	 * */
	public OSPAudioSource ospSource;

	// Use this for initialization
	void Start () {
		float clipLength = ospSource.GetComponent<AudioSource>().clip.length;
		InvokeRepeating ("ChargeUp",0,3);
		InvokeRepeating ("ShootTarget", clipLength, 3);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void ShootTarget() {
		Vector3 direction = -this.transform.position  + target.position;
		Rigidbody clone = Instantiate(prefab, transform.position,transform.rotation) as Rigidbody;
		clone.velocity = direction * shootingSpeed;
	}

	void ChargeUp(){
		// this.GetComponent<AudioSource> ().Play ();

		// trigger osp audio source
		ospSource.Play ();
	}
}
