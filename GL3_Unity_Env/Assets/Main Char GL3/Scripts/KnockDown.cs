using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;  //voor orderby

public class KnockDown : MonoBehaviour{
//    [SerializeField]
    private  List<Transform> adolfTransform = new List<Transform>();
    [SerializeField]
    private List<Transform> getUpTransform = new List<Transform>();

    private Rigidbody[] adolfBone;
    private GameObject[] getUpBone;

    private GameObject getUp;


    public bool getUpActive;
    [HideInInspector]
    public bool setKinematic;

    MainRagdollController ragdikkScript;
    public Animator anim;

    private Transform rayStart;
    private Ray ray;
    private RaycastHit rayHit;
    private float getUpAnimSwitch; //0 = opstaan face down 1 = opstaan face up

    private bool animLock;
    
    //Bug Issues: als je rotzooit met de animator controller gaat getUpBone[]/GetUpTransform trippen. start scene of unity opnieuw op en gefixt.
    //Samenwerkende scripts: Mainragdollcontroller(Activate ragbool) / MainAnim01: juiste LayerWeight Aanzetten.(optioneel)


	void Start ()
    {
        rayStart = GameObject.Find("Adolf/Circle001/Rotate Model CTRL/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/RaycasterPosDetect").transform;

        ragdikkScript = GetComponent<MainRagdollController>();
        anim = GetComponent<Animator>();

        getUp = GameObject.Find("AdolfFront");

        adolfBone = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in adolfBone)
        {
            adolfTransform.Add(rb.transform);
            adolfTransform = adolfTransform.OrderBy(x => x.name).ToList(); //rangschikken op naam
        }

        //
        getUpBone = GameObject.FindGameObjectsWithTag("BoneF"); // DetectFrontorBack() past dit aan in runtime.

        foreach (GameObject uBone in getUpBone)
        {
            getUpTransform.Add(uBone.transform);
            getUpTransform = getUpTransform.OrderBy(b => b.name).ToList();
        }
   
    }
	
    void GetUp() 
    {
        animLock = false;
        for (int i = 0; i < adolfTransform.Count; i++)
        {
            adolfTransform[i].position = Vector3.Lerp(adolfTransform[i].position, getUpTransform[i].position, Time.deltaTime * 3);

            adolfTransform[i].rotation = Quaternion.Slerp(adolfTransform[i].rotation,getUpTransform[i].rotation, Time.deltaTime *3);

            StartCoroutine("WaitNigga");
            
        }
    }

    IEnumerator WaitNigga()
    {
        yield return new WaitForSeconds(0.38f);
        
        setKinematic = true;

        yield return new WaitForSeconds(0.8f);

        getUpActive = false;
        anim.enabled = true;
        if (getUpAnimSwitch == 1 && !animLock)
        {
            anim.SetBool("*GetUpB", true);      //Bij buggy overgang check transition>settings>transition duration
            animLock = true;
        }
        else if (getUpAnimSwitch == 0 && !animLock)
        {
            anim.SetBool("*GetUpF", true);
            animLock = true;
        }
      
        ragdikkScript.activateRag = false;
        setKinematic = false;  //iskinematic wordt weer bestuurd door MainRagdollController
    }

    void DetectFrontorBack() //bepaald en kiest welke animatie moet afspelen en welke bones gebruikt moeten worden voor transition
    {
        ray = new Ray (rayStart.position, rayStart.forward);
        Debug.DrawRay(rayStart.position, rayStart.forward, Color.green);
        if (Physics.Raycast(ray, out rayHit, 3f))
        {
            if (getUpAnimSwitch != 0)
            {
                getUpAnimSwitch = 0;
                getUpBone = GameObject.FindGameObjectsWithTag("BoneF");

                getUpTransform.RemoveRange(0, 13);
                StartCoroutine("ResetL");
            }
        }
        else
        {
            if (getUpAnimSwitch != 1)
            {
                getUpAnimSwitch = 1;
                getUpBone = GameObject.FindGameObjectsWithTag("BoneB");

                getUpTransform.RemoveRange(0, 13);
                StartCoroutine("ResetL");
            }
            
        }
    }

    IEnumerator ResetL() //Refresh Transform Lists.
    {
        yield return new WaitForEndOfFrame();

        foreach (GameObject uBone in getUpBone)
        {
            getUpTransform.Add(uBone.transform);
            getUpTransform = getUpTransform.OrderBy(b => b.name).ToList();
        }
       
    }


 
    void Update()
    {
        if (setKinematic)
        {
            foreach (Rigidbody rb in adolfBone)
            {
                rb.isKinematic = true;
            }
        }
        //Get up kan alleen gebruikt worden als de animator uitstaat.
        if (getUpActive && anim.enabled == false)
        {
            GetUp();
        }
        else
        {
            getUpActive = false;
        }

        if (anim.enabled == false)
        {
            DetectFrontorBack();
        }
	}
}
