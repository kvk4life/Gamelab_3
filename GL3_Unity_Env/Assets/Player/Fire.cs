using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    public AudioSource source;
	// Use this for initialization
	void Start () {
	source=GetComponent<AudioSource>();
    StartCoroutine("PlayFire");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator PlayFire()
    {
        yield return new WaitForSeconds(Random.Range(0.1f,4f));
        source.Play();
    }
}
