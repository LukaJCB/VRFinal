using UnityEngine;
using System.Collections;

public class LaserSword : MonoBehaviour
{

	public SixenseInput input;
	
	float 	m_sensitivity = 0.001f; // Sixense units are in mm
	Vector3 m_initialPosition;
	Quaternion m_initialRotation;

	// Use this for initialization
	void Start ()
	{
		m_initialRotation = transform.localRotation;
		m_initialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (SixenseInput.Controllers [0] != null) {
			Debug.Log ("BEWEGE CONTROLLER 1");
			this.transform.localPosition = SixenseInput.Controllers [0].Position * m_sensitivity;
			this.transform.localRotation = SixenseInput.Controllers [0].Rotation * m_initialRotation;
			Debug.Log ("postiton: " + this.transform.localPosition);
			Debug.Log ("rotation: " + this.transform.localRotation);
		}


	}
}
