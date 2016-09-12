using UnityEngine;
using System.Collections;

public class Charachter_Controller : MonoBehaviour {

	public int rayDistance;
	public int moveSpeed;
	public int mouseSpeedHor;
	public int mouseSpeedVer;
	public int limit;

	public float camRot;

	public GameObject mainCam;

	void Start () {
	
	}
	
	void Update () {

		Movement ();
		CamRotHorizontal();
		CamRotVertical();
	
	}

	public void Movement (){

		if (Input.GetAxis ("Vertical") > 0) {															
			if (Physics.Raycast (transform.position, transform.forward, rayDistance)) {
		}
		else{
			transform.Translate (Vector3.forward * moveSpeed * Input.GetAxis ("Vertical") * Time.deltaTime);
			}
		}

		if (Input.GetAxis ("Vertical") < 0) {															
			if (Physics.Raycast (transform.position, -transform.forward, rayDistance)) {
		}
		else{
			transform.Translate (Vector3.back * moveSpeed * -Input.GetAxis ("Vertical") * Time.deltaTime);
			}
		}

		if (Input.GetAxis ("Horizontal") > 0) {															
			if (Physics.Raycast (transform.position, transform.right, rayDistance)) {
		}
		else{
			transform.Translate (Vector3.right * moveSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime);
			}
		}

		if (Input.GetAxis ("Horizontal") < 0) {															
			if (Physics.Raycast (transform.position, -transform.right, rayDistance)) {
		}
		else{
			transform.Translate (Vector3.left * moveSpeed * -Input.GetAxis ("Horizontal") * Time.deltaTime);
			}
		}
	}

	void CamRotHorizontal (){

		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"),0) * Time.deltaTime * mouseSpeedHor);

	}

	void CamRotVertical (){

		camRot -= Input.GetAxis("Mouse Y") * mouseSpeedVer;
		camRot = Mathf.Clamp(camRot, -limit, limit);
		mainCam.transform.localRotation = Quaternion.Euler(camRot, 0, 0);

	}
}
