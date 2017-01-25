using UnityEngine;
using System.Collections;

public class NewMakeShiftCombat : MonoBehaviour {
    public RaycastHit hit;
    public int damage;
    public Combat combat;
    public AudioClip swing;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetButtonDown("LB") && Physics.Raycast(transform.position,transform.forward, out hit, 2f) && combat.mayAttack)
        {
            print(hit.transform.name);
            if(hit.transform.tag=="Enemy")
            {
                hit.transform.GetComponent<DemonMakeShiftBs>().TakeDamage(damage);
            }
        }
    if(Input.GetButtonDown("LB") && combat.mayAttack)
        {
            GetComponent<AudioSource>().PlayOneShot(swing);

        }
    }
}
