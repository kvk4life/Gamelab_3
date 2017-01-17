using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldMng : MonoBehaviour {
    public int curGold;
    public int startGold;
    public int maxGold;
    public Text displayedGold;
    public CanvasGroup maxGoldTxt;
    public CanvasGroup brokeTxt;
    public GameObject pressX;
    public float goldTimer;
    public bool testAdd;

    public void Start() {
        curGold = startGold;
        UpdateHUD();
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            if (testAdd) {
                AddGold(30);
            }
            else {
                RemoveGold(25);
            }
        }
    }

    public void UpdateHUD() {
        string myGold = string.Format("{0}", curGold); ;
        displayedGold.text = myGold;
    }

    public void AddGold(int addedGold) {
        curGold += addedGold;
        if (curGold > maxGold) {
            curGold = maxGold;
            StartCoroutine(MaxedOutWarning());
        }
        UpdateHUD();
    }

    public void RemoveGold(int removedGold) {
        curGold -= removedGold;
        if (curGold < 0) {
            curGold = 0;
            StartCoroutine(NoMoney());
        }
        if (curGold > 0) {
            curGold -= removedGold;
            if (curGold < 0) {
                curGold = 0;
            }
            UpdateHUD();
        }
    }

    public void NotEnoughGold() {
        StartCoroutine(NoMoney());
    }

    public void PressToInteract(bool activeInteract) {
        pressX.SetActive(activeInteract);
    }

    public IEnumerator MaxedOutWarning() {
        maxGoldTxt.alpha = 1;
        yield return new WaitForSeconds(goldTimer);
        maxGoldTxt.alpha = 0;
    }

    public IEnumerator NoMoney() {
        brokeTxt.alpha = 1;
        yield return new WaitForSeconds(goldTimer);
        brokeTxt.alpha = 0;
    }
}
