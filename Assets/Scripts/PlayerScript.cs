using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int startingLives;
	private int currentLives;

	// Use this for initialization
	void Start () {
		currentLives = startingLives;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("LaserBeam")){
			ApplyDamage();
		}
	
	}

	void ApplyDamage(){

		BroadcastMessage ("RemainingLives", currentLives - 1);
		Debug.Log (currentLives);

		if (currentLives > 1) {		
			currentLives--;
		} else {
			LoadMainMenu();
		}
	}

	void LoadMainMenu(){

		Application.LoadLevel (0);
	}

}
