using UnityEngine;
using System.Collections;

public class RoundMng : MonoBehaviour {
    public int curRound;
    public float curWaveCount;
    public float waveCountDivider;
    public float startCountdown;
    public GameObject controlPointMng;

    public void Start() {
        curRound++;
        StartCoroutine("CountdownToStart");
    }

    public IEnumerator CountdownToStart() {
        yield return new WaitForSeconds(startCountdown);
        controlPointMng.GetComponent<ControlPointMng>().UnpoolControlPoint();
    }

    public void CalculateRound() {
        curWaveCount++;
        float roundCount = curWaveCount / waveCountDivider;
        if (roundCount == 1) {
            curWaveCount = 0;
            curRound++;
            NextRoundMng();
            print("Ik zit in ronde " + curRound);
        }
    }

    public void NextRoundMng() {

    }
}
