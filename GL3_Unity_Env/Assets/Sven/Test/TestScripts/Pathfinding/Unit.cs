using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public Transform target;
	public float speed;

	public Vector3[] path;
	Vector3 currentWaypoint;
	int targetIndex = 0;

	public float cooldownPath;
	public float repeatRate;
	

	void Start () {

		//target = GameObject.Find("Player(Clone)").GetComponent<Transform>();
		target = GameObject.Find("Pig Benis01").GetComponent<Transform>();

		InvokeRepeating("StartNewPathProcess", 0, repeatRate);

		//PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		
	}


	void StartNewPathProcess (){

		print("InvokeRepeating");
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);

	}

	public void OnPathFound (Vector3[] newPath, bool pathSuccesful){
		if (pathSuccesful == true){
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath (){

		while (true){
			if(transform.position == currentWaypoint){
				targetIndex ++;
				if(targetIndex >= path.Length){
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			transform.LookAt(currentWaypoint);
			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
