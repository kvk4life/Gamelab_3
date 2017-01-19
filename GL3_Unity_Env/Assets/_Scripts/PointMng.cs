using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointMng : MonoBehaviour {
    public int curPoints;
    public int multiplyer;
    public int multiplyerIncreaser;
    private int startMultiplyer;
    public int maxMultiplyer;
    public int comboCount;
    public int comboDivider;
    public int startComboDivider;
    public float chainTimer;
    public Text displayedPoints;
    public Text comboDisplay;//dit wordt later waarschijnlijk een image
    private Coroutine comboCoroutine;

    public void Start() {
        startComboDivider = comboDivider;
        startMultiplyer = multiplyer;
    }

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            AddPoints(1);
        }
    }

    public void UpdateHUD(string whichHUD) {
        switch (whichHUD) {
            case "AddPoints":
                string myPoints = string.Format("{0}", curPoints);
                displayedPoints.text = myPoints;
                break;
            case "ComboChain":
                //verander hier die combomultiplier
                string myCombo = "X" + (string.Format("{0}", multiplyer));
              //  comboDisplay.text = myCombo;
                break;
            case "EmptyChain":
               // comboDisplay.text = null;
                break;
        }
    }

    public void AddPoints(int addPoints) {
        int multiplyedPoints = addPoints * multiplyer;
        ComboChain();
        curPoints += multiplyedPoints;
        UpdateHUD("AddPoints");
    }

    public void ComboChain() {
        comboCount++;
        int comboChecker = comboDivider - comboCount;
        if (comboCount == comboDivider) {
            comboDivider += comboCount;
        }
        if (comboChecker == 0 && multiplyer < maxMultiplyer) {
            multiplyer *= multiplyerIncreaser;
            UpdateHUD("ComboChain");
            print(multiplyer);
        }
        if (comboCoroutine != null) {
            StopCoroutine(comboCoroutine);
        }
        comboCoroutine = StartCoroutine(ChainCountdown());
    }

    public IEnumerator ChainCountdown() {
        yield return new WaitForSeconds(chainTimer);
        EndComboChain();
    }

    public void EndComboChain() {
        comboCount = 0;
        comboDivider = startComboDivider;
        multiplyer = startMultiplyer;
        UpdateHUD("EmptyChain");
    }

    public void ForcedComboShutdown() {
        if (comboCoroutine != null) {
            StopCoroutine(comboCoroutine);
            EndComboChain();
        }
    }

    public void EndGame() {
        GetComponent<HighScore>().CheckHighScore(curPoints);
    }
}
