using UnityEngine;
using System.Collections;

public class DroneScript : MonoBehaviour {
	
	public static Transform target;
	Vector3 target2;
	public static Rigidbody prefab;
	public static float shootingSpeed;
	
	public float speed = 20f;
	public Vector3 destination;
	public float range = 40f;
	
	/*
	 * AudioSource is now a part of the OSPAudioSource
	 * */
	public OSPAudioSource ospSource;
	public AudioClip shootSound, destroySound;
	
	// Use this for initialization
	void Start () {
		float clipLength = shootSound.length;
		InvokeRepeating ("ChargeUp",0,3);
		InvokeRepeating ("ShootTarget", clipLength, 3);
		newDestination ();
		target2 = target.transform.position;
		target2 += Vector3.up * -0.5f * range;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 diff = this.transform.position - target2;
		
		if (transform.position.y < target.position.y) {
			destination = diff ;
		}
		else if ((diff.magnitude > range && Vector3.Dot (destination, diff) > 0 ) || diff.magnitude < 5) {
			destination = Random.rotation* -diff;
		}
		destination.Normalize ();
		
		Vector3 movement = destination * speed * Time.deltaTime;
		this.transform.position += movement;
		
		transform.LookAt(target);
	}
	
	void ShootTarget() {
		Vector3 direction = -this.transform.position  + target.position;
		Rigidbody clone = Instantiate(prefab, transform.position,transform.rotation) as Rigidbody;
		clone.velocity = direction * shootingSpeed;
	}
	
	void ChargeUp(){
		// this.GetComponent<AudioSource> ().Play ();
		
		// trigger osp audio source
		ospSource.GetComponent<AudioSource> ().clip = shootSound;
		ospSource.Play ();
	}
	
	void newDestination()
	{ 
		destination = Random.insideUnitSphere*20;
	}

	void OnDestroy(){
		ospSource.GetComponent<AudioSource> ().clip = destroySound;
		ospSource.Play ();
		Destroy (this.gameObject);
	}
}
