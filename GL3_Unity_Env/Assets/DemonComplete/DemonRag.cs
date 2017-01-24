using UnityEngine;
using System.Collections;

public class DemonRag : MonoBehaviour {

	public Rigidbody[] bones;
	private Animator anim;
    private Coroutine decay;
    public float decaySpd;

	// Use this for initialization
	public void Start () 
	{
		anim = GetComponent<Animator> ();

		bones = GetComponentsInChildren<Rigidbody> ();

		foreach (Rigidbody rb in bones) 
		{
			rb.isKinematic = true;
		}
	
	}

	public void RagActive()
	{
        decay = StartCoroutine(Decaying());
		anim.enabled = false;
		foreach (Rigidbody rb in bones) 
		{
			rb.isKinematic = false;
		}
	}

    IEnumerator Decaying() {
        yield return new WaitForSeconds(decaySpd);
        Destroy(gameObject);   
    }
}
