using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlPoint : MonoBehaviour {
    public int curHealth;
    private int startHealth;
    public bool pooled;
    private Animator myAnim;
    private AnimationClip myClip;
    public AnimationEvent myEvent;
    public int myClipInt;
    public float eventTimerPercentage;
    public string eventFunctionName;
    public GameObject myCrystal;

    void Awake() {
        startHealth = curHealth;
        myAnim = GetComponent<Animator>();
        myClip = myAnim.runtimeAnimatorController.animationClips[myClipInt];
        myEvent.time = myClip.length / 100 * eventTimerPercentage;
        myEvent.functionName = eventFunctionName;
        myClip.AddEvent(myEvent);
    }

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            HealthChecker(30);
        }
    }

    public void HealthChecker(int recievedDamage) {
        if (!pooled) {
            curHealth -= recievedDamage;
            if (curHealth < 1)
            {
                curHealth = 0;
                Death();
            }
        }
    }

    public void Death() {
        myAnim.SetBool("destroy", true);
    }

    public void Enabler(bool trueOrFalse) {
        if (myCrystal != null) {
            myCrystal.SetActive(trueOrFalse);
        }
    }

    public void Unpool(Vector3 controlPointPos) {
        pooled = false;
        transform.position = controlPointPos;
        curHealth = startHealth;
        if (myAnim != null) {
            myAnim.SetBool("destroy", false);
        }  
        Enabler(true);
    }

    public void Repool() {
        pooled = true;
        Enabler(false);
    }
}
