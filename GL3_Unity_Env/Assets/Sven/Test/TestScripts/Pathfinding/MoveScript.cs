using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public GameObject player;
	public float speed;

	void Start () {

		player = GameObject.Find("CharachterController");
	
	}
	
	void Update () {

		float step = speed * Time.deltaTime;
		Transform target = player.GetComponent<Transform>();
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.LookAt(target);
	
	}
}
