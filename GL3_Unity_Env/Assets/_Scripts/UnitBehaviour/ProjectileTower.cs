using UnityEngine;
using System.Collections;

public class ProjectileTower : MonoBehaviour {
	public bool pooled;
	public float projectileSpeed;
	public GameObject myTarget;
	public Transform projectileHolder;
	private Renderer rend;
	private Collider myCol;

	public void Start(){
		myCol = GetComponent<Collider> ();
		rend = GetComponent<Renderer> ();
	}

	public void Update(){
		Attack ();
	} 

	public void Unpool(GameObject recievedTarget){
		myTarget = recievedTarget;
		transform.rotation = projectileHolder.rotation;
		myCol.enabled = true;
		rend.enabled = true;
		pooled = false;
	}

	public void Repool(){
		myTarget = null;
		myCol.enabled = false;
		rend.enabled = false;
		transform.position = transform.parent.transform.position;
		pooled = true;
	}

	public void Attack(){
		if(!pooled){
			if (myTarget != null) {
				transform.LookAt (myTarget.GetComponent<Transform> ());
				transform.Translate (Vector3.forward * projectileSpeed * Time.deltaTime);
			}
			else {
				Repool ();
			}
		}
	}

	public void OnTriggerEnter(Collider col){
		if(col.gameObject == myTarget){
			print ("Target Hit");
			Repool ();
		}
	}
}
