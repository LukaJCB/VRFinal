using UnityEngine;
using System.Collections;

public class DroneSpawnerScript : MonoBehaviour {

	public float spawnRate, spawnRateIncrease, shootingSpeed, spawnRadius, minSpawnDistance;
	private float timeToSpawn;

	public Transform prefab;
	public Transform target;
	public Rigidbody projectile;


	// Use this for initialization
	void Start () {

		DroneScript.prefab = projectile;
		DroneScript.target = target;
		DroneScript.shootingSpeed = this.shootingSpeed;
		timeToSpawn = 1 / spawnRate;
		StartCoroutine (SpawnProcess());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnProcess(){
		yield return new WaitForSeconds (2);
		while (true) {
			Vector3 spawnPosition = Random.insideUnitSphere* spawnRadius + target.position;
			if (Vector3.Distance(spawnPosition, target.position) < minSpawnDistance){
				spawnPosition = Vector3.Normalize(spawnPosition-target.position) * minSpawnDistance;
			}
			if (spawnPosition.y < 1){
				spawnPosition.y = 1;
			}
			Instantiate(prefab, spawnPosition, Quaternion.LookRotation(target.position-spawnPosition));
			yield return new WaitForSeconds (timeToSpawn);
			timeToSpawn *= spawnRateIncrease;
			Debug.Log(timeToSpawn);
		}
	}

}
