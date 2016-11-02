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
    private Coroutine comboCoroutine;

    public void Start() {
        startComboDivider = comboDivider;
        startMultiplyer = multiplyer;
    }

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            ComboChain();
        }
    }

    public void UpdateHUD() {
        string myPoints = string.Format("{0}", curPoints); ;
        displayedPoints.text = myPoints;
    }

    public void AddPoints(int addPoints) {
        int multiplyedPoints = addPoints * multiplyer;

        ComboChain();

        curPoints += addPoints;
        UpdateHUD();
    }

    public void ComboChain() {
        comboCount++;
        int comboChecker = comboDivider - comboCount;
        if (comboCount == comboDivider) {
            comboDivider += comboCount;
        }
        if (comboChecker == 0 && multiplyer < maxMultiplyer) {
            multiplyer *= multiplyerIncreaser;
            print(multiplyer);
        }
        if (comboCoroutine != null) {
            StopCoroutine(comboCoroutine);
        }
        comboCoroutine = StartCoroutine(ChainCountdown());
    }

    public IEnumerator ChainCountdown() {
        yield return new WaitForSeconds(chainTimer);
        comboCount = 0;
        comboDivider = startComboDivider;
        multiplyer = startMultiplyer;
        print("Chain has ended");
    }
}
