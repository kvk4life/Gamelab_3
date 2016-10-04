using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockOnStrafe : MonoBehaviour
{
    public float speed;
    public Image border1, border2;
    public Orbit orbit;
    public bool triggerPressed;
    public Combat combat;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RB"))
        {
            orbit.ExitSprint();
            orbit.camMode=CameraMode.ThirdPersonStrafe;
            StopAllCoroutines();
            StartCoroutine(LockOn());
        }
        if(Input.GetButtonUp("RB"))
        {
            orbit.camMode = CameraMode.ThirdPerson;
            StopAllCoroutines();
            StartCoroutine(LockOff());
        }

        if(Input.GetAxis("Triggers")<0)
        {
            orbit.ExitSprint();
            orbit.camMode=CameraMode.FirstPerson;
            if(triggerPressed==false)
            {
                StopAllCoroutines();
                StartCoroutine(LockOn());
                triggerPressed=true;
            }

        }

        if(Input.GetButtonDown("R3"))
        {
            orbit.ExitSprint();
            orbit.camMode=CameraMode.LockOn;
            StopAllCoroutines();
            StartCoroutine(LockOn());
        }

        if(Input.GetAxis("Triggers")==0)
        {
            if(triggerPressed==true)
            {
                print("Lerpy");
                orbit.camMode = CameraMode.ThirdPerson;
                StopAllCoroutines();
                StartCoroutine(LockOff());
                orbit.StartCoroutine("ExitFirstPerson");
            }
            triggerPressed=false;

        }
    }

    IEnumerator LockOn()
    {
        while (border1.fillAmount < 1)
        {
            border1.fillAmount += speed*Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            if (border1.fillAmount > 1)
            {
                border1.fillAmount = 1;
            }
            border2.fillAmount += speed * Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            if (border2.fillAmount > 1)
            {
                border2.fillAmount = 1;
            }
        }
    }

    IEnumerator LockOff()
    {
        while (border1.fillAmount > 0.5f)
        {
            border1.fillAmount -= speed * Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            if (border1.fillAmount <0.5f)
            {
                border1.fillAmount = 0.5f;
            }
            border2.fillAmount -= speed * Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            if (border2.fillAmount < 0.5f)
            {
                border2.fillAmount = 0.5f;
            }
        }
    }
}
