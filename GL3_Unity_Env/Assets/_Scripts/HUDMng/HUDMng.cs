using UnityEngine;
using System.Collections;

public class HUDMng : MonoBehaviour {
    public GameObject[] hUDArray;
    private bool correctHUD;
    public float swapButtonRate;

    public void SwapHUD(GameObject hUDToSwap) {
        for (int i = 0; i < hUDArray.Length; i++) {
            if (hUDArray[i] == hUDToSwap) {
                correctHUD = true;
            }
            else {
                correctHUD = false;
            }
            hUDArray[i].SetActive(correctHUD);
        }
    }
}
