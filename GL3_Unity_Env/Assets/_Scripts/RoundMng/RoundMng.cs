using UnityEngine;
using System.Collections;

public class RoundMng : MonoBehaviour {
    public int curRound;
    public float curWaveCount;
    public float waveCountDivider;
    public float startCountdown;
    public float newRoundCd;
    public GameObject controlPointMng;
    public ControlPointMng cpMng;

    public void Start() {
        cpMng = controlPointMng.GetComponent<ControlPointMng>();
        StartCoroutine(CountdownNewRound(startCountdown));
    }

    public IEnumerator CountdownNewRound(float countdownSeconds) {
        yield return new WaitForSeconds(countdownSeconds);
        cpMng.UnpoolControlPoint();
    }

    public void CalculateRound() {
        curWaveCount++;
        float roundCount = curWaveCount / waveCountDivider;
        if (roundCount == 1) {
            curWaveCount = 0;
            curRound++;
            NextRoundMng();
        }
    }

    public void NextRoundMng() {
        cpMng.ControlPointSucces();
        StartCoroutine(CountdownNewRound(newRoundCd));
    }
}
