using UnityEngine;
using System.Collections;

public class PickupZone : MonoBehaviour {
	GameObject m_pickupObject;
	bool isGrabbing;

	// Use this for initialization
	void Start () {
		isGrabbing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Grab() {
		if (m_pickupObject) {
			m_pickupObject.transform.parent = transform;
			m_pickupObject.rigidbody.isKinematic = true;
			isGrabbing = true;
		}
	}

	public void Drop() {
		if (m_pickupObject) {
			m_pickupObject.transform.parent = null;
			m_pickupObject.rigidbody.isKinematic = false;
			isGrabbing = false;
		}
	}

	void OnTriggerEnter (Collider col){
		if (col.tag == "Pickup" && !isGrabbing) {
			m_pickupObject = col.gameObject;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject == m_pickupObject) {
			m_pickupObject = null;
		}
	}
}
