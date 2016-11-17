using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDController : MonoBehaviour {
    public delegate void ButtonDelegate();
    public ButtonDelegate buttonDel;
    public GameObject hUDMng;
    public GameObject[] buttonArray;
    public int selectedButton;
    public bool afterStart;
    private float swapButtonRate;
    private float nextSwapButton;

    public void Start() {
        swapButtonRate = hUDMng.GetComponent<HUDMng>().swapButtonRate;
        for (int i = 0; i < buttonArray.Length; i++) {
            CustomButton customButton = buttonArray[i].GetComponent<CustomButton>();
            customButton.hUDControl = this;
            customButton.hUDMng = hUDMng;
            if (i == selectedButton) {
                SwapSelectedButton();
            }
        }
    }

    public void Awake() {
        if (afterStart) {
            SwapSelectedButton();
        }
        else {
            afterStart = true;
        }
    }

    public void SwapSelectedButton() {
        buttonArray[selectedButton].GetComponent<CustomButton>().Selected();
    }

    public void Update() {
        SwitchButton();
        ActivateSelectedButton();
    }

    public void SwitchButton() {
        bool leftJoyYBool = Input.GetAxis("LeftJoyY") != 0;
        if (leftJoyYBool && Time.time > nextSwapButton) {
            buttonArray[selectedButton].GetComponent<CustomButton>().Unselect();
            if (Input.GetAxis("LeftJoyY") > 0) {
                selectedButton--;
                if (selectedButton < 0) {
                    selectedButton = buttonArray.Length - 1;
                }
            }
            else {
                selectedButton++;
                if (selectedButton > buttonArray.Length - 1) {
                    selectedButton = 0;
                }
            }
            SwapSelectedButton();
            nextSwapButton = Time.time + swapButtonRate;
        }
    }

    public void ActivateSelectedButton() {
        if (Input.GetButtonDown("A")) {
            buttonDel();
        }
    }
}
