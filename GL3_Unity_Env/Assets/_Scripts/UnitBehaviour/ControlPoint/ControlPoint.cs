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
    private Collider crystalCol;
    private Renderer crystalRend;

    void Start() {
        startHealth = curHealth;
        ListUpdater();
        myAnim = GetComponent<Animator>();
        myClip = myAnim.runtimeAnimatorController.animationClips[myClipInt];
        float clipLength = myClip.length;
        myEvent.time = clipLength / 100 * eventTimerPercentage;
        myEvent.functionName = eventFunctionName;
        myClip.AddEvent(myEvent);
    }

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            HealthChecker(30);
        }
    }

    public void ListUpdater() {
        if (myCrystal != null) {
            crystalCol = myCrystal.GetComponent<Collider>();
            crystalRend = myCrystal.GetComponent<Renderer>();
        }
    }

    public void HealthChecker(int recievedDamage) {
        curHealth -= recievedDamage;
        if (curHealth < 1) {
            curHealth = 0;
            Death();
        }
    }

    public void Death() {
        myAnim.SetBool("Destroy", true);
    }

    public void Enabler(bool trueOrFalse) {
        if (crystalCol != null) {
            crystalCol.enabled = trueOrFalse;
            crystalRend.enabled = trueOrFalse;
        }
    }

    public void Unpool(Vector3 controlPointPos) {
        pooled = false;
        transform.position = controlPointPos;
        curHealth = startHealth;
        if (myAnim != null) {
            myAnim.SetBool("Destroy", false);
        }  
        Enabler(true);
    }

    public void Repool() {
        pooled = true;
        Enabler(false);
    }
}
