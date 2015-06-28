using UnityEngine;
using System.Collections;

public class LaserSword : MonoBehaviour
{

	public float m_sensitivity; // Sixense units are in mm
	Vector3 m_initialPosition;
	Vector3 m_baseOffset;
	Quaternion m_initialRotation;

	AudioSource audio;
	bool 	m_bInitialized;

	// Use this for initialization
	void Start ()
	{
		m_initialRotation = transform.localRotation;
		m_initialPosition = transform.localPosition;
		m_baseOffset = transform.position;
		audio = GetComponent<AudioSource> ();
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool resetSaberPosition = false;
		
		SixenseInput.Controller con = SixenseInput.Controllers [1];
		
		if (IsControllerActive (con) && con.GetButtonDown (SixenseButtons.START)) {
			resetSaberPosition = true;
		} 
		if ( m_bInitialized ) {
			UpdateSaber(con);
		}

		
		if ( resetSaberPosition )
		{
			m_bInitialized = true;
			
			m_baseOffset = Vector3.zero;
			
			// Get the base offset assuming forward facing down the z axis of the base
				m_baseOffset += con.Position;
			
			m_baseOffset /= 2;
		}

	}

	void UpdateSaber(SixenseInput.Controller con) {
		
		bool bControllerActive = IsControllerActive(con);

		if (bControllerActive) {
			this.transform.localPosition = ((SixenseInput.Controllers [1].Position - m_baseOffset) * m_sensitivity) + m_initialPosition;
			this.transform.localRotation = SixenseInput.Controllers [1].Rotation * m_initialRotation;
		} else {
			this.transform.localPosition = m_initialPosition;
			this.transform.localRotation = m_initialRotation;
		}
	}

	
	/** returns true if a controller is enabled and not docked */
	bool IsControllerActive( SixenseInput.Controller controller )
	{
		return ( controller != null && controller.Enabled && !controller.Docked );
	}
}
