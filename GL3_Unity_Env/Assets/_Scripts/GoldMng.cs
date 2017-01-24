using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldMng : MonoBehaviour {
    public int curGold;
    public int startGold;
    public Text displayedGold;
    public CanvasGroup brokeTxt;
    public GameObject pressX;
    public float goldTimer;

    public void Start() {
        curGold = startGold;
        UpdateHUD();
    }

    public void UpdateHUD() {
        string myGold = string.Format("{0}", curGold); ;
        displayedGold.text = myGold;
    }

    public void AddGold(int addedGold) {
        curGold += addedGold;
        UpdateHUD();
    }

    public void RemoveGold(int removedGold) {
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

    public IEnumerator NoMoney() {
        brokeTxt.alpha = 1;
        yield return new WaitForSeconds(goldTimer);
        brokeTxt.alpha = 0;
    }
}
