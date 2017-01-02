using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public Transform target;
	public float speed;
	Vector3[] path;
    private Vector3 currentWaypoint;
    int targetIndex;
	public float cooldownPath;
	public float repeatRate;

	void Start () {

		InvokeRepeating("StartNewPathProcess", 0, repeatRate);

		//PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		
	}

	void StartNewPathProcess (){

		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);

	}

	public void OnPathFound (Vector3[] newPath, bool pathSuccesful){

		if (pathSuccesful == true){
			path = newPath;
            StopPathCoroutine();
            StartPathCoroutine();
        }
	}

    public void StopPathCoroutine() {
        StopCoroutine("FollowPath");
    }

    public void StartPathCoroutine() {
        StartCoroutine("FollowPath");
    }

	IEnumerator FollowPath (){

        if (path.Length > 0) {
            currentWaypoint = path[0];
        }
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
