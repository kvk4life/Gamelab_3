using UnityEngine;
using System.Collections;

public class Fence : MonoBehaviour {
    private Animator anim;
    public int reqGold;
    public GoldMng goldMng;
    private Collider boxCol;
    private Collider sphereCol;

    public void Start() {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider>();
        sphereCol = GetComponent<SphereCollider>();
    }

    public void Update() {
        if (goldMng != null) {
            CheckForGold();
        }
    }

    public void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "player") {
            goldMng = col.transform.GetComponent<GoldMng>();
        }
    }

    public void OnTriggerExit(Collider col) {
        if (col.transform.tag == "player") {
            goldMng.PressToInteract(false);
            goldMng = null;
        }
    }

    public void CheckForGold() {
        int playerGold = goldMng.curGold;
        goldMng.PressToInteract(true);
        if (Input.GetButtonDown("X")) {
            if (playerGold >= reqGold) {
                ActivateFence();
            }
            else {
                goldMng.NotEnoughGold();
            }
        }
    }

    public void ActivateFence() {
        anim.SetTrigger("open");

    }	
}
