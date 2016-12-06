using UnityEngine;
using System.Collections;

public class RoundMng : MonoBehaviour {
    public int curRound;
    public float curWaveCount;
    public float waveCountDivider;
    public float startCountdown;
    public float newRoundCd;
    public GameObject controlPointMng;
    [HideInInspector]
    public SpawnManager spawnMng;
    public ControlPointMng cpMng;
    public bool waitingForNextRound;

    public void Start() {
        cpMng = controlPointMng.GetComponent<ControlPointMng>();
        StartCoroutine(CountdownNewRound(startCountdown));
    }

    public IEnumerator CountdownNewRound(float countdownSeconds) {
        yield return new WaitForSeconds(countdownSeconds);
        curRound++;
        waitingForNextRound = false;
        spawnMng.CheckConditionsNewWave();
        cpMng.UnpoolControlPoint();
    }

    public void CalculateRound() {
        curWaveCount++;
        float roundCount = curWaveCount / waveCountDivider;
        if (roundCount == 1) {
            curWaveCount = 0;
            waitingForNextRound = true;
            NextRoundMng();
        }
    }

    public void NextRoundMng() {
        cpMng.ControlPointSucces();
        StartCoroutine(CountdownNewRound(newRoundCd));
    }
}
