using UnityEngine;
using System.Collections;

public class RagToAnimConcept : MonoBehaviour {

    public GameObject legUp;
    public GameObject legLow;

    public Rigidbody legUpBn;
    public Rigidbody legLowBn;

    public GameObject legUpTarget;
    public GameObject legLowTarget;

    public bool startMoveTo = false;


	
	void Start ()
    {
        legUp = GameObject.Find("Limb/Leg up");
        legLow = GameObject.Find("Limb/Leg low");
        
        legUpBn = legUp.GetComponent<Rigidbody>();
        legLowBn = legLow.GetComponent<Rigidbody>();

       legUpTarget = GameObject.Find("Limb2/Leg up");
       legLowTarget = GameObject.Find("Limb2/Leg low");

    }
	
	
	void Update ()
    {
        if (Input.GetKeyDown("q")) //q indrukken en rigidbodys op Iskinematic aan zetten = reageren op physics uit. 
        {
            legUpBn.isKinematic = true;
            legLowBn.isKinematic = true;

            startMoveTo = true;
        }
        else if (startMoveTo) //bool gaat aan na q indrukken en transitiont de objecten netjes naar punt hun target 
        {
            legUp.transform.position = Vector3.Lerp(legUp.transform.position,legUpTarget.transform.position, Time.deltaTime * 3);
            legUp.transform.rotation = Quaternion.Slerp(legUp.transform.rotation, legUpTarget.transform.rotation, Time.deltaTime * 3);

            legLow.transform.position = Vector3.Lerp(legLow.transform.position, legLowTarget.transform.position, Time.deltaTime * 3);
            legLow.transform.rotation = Quaternion.Slerp(legLow.transform.rotation, legLowTarget.transform.rotation, Time.deltaTime * 3);
        }
       
    }
}
