using UnityEngine;
using System.Collections;

public class JumpThrough : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter(Collider other) {
		this.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	void OnTriggerExit(Collider other) {
		this.gameObject.GetComponent<BoxCollider>().enabled = true;
	}

}
