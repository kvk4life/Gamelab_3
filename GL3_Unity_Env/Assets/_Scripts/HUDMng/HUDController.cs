using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDController : MonoBehaviour {
    public delegate void ButtonDelegate();
    public ButtonDelegate buttonDel;
    public GameObject HUDMng;
    public GameObject[] buttonArray;
    public int selectedButton;
    public bool afterStart;

    public void Start() {
        for (int i = 0; i < buttonArray.Length; i++) {
            CustomButton customButton = buttonArray[i].GetComponent<CustomButton>();
            customButton.hUDControl = this;
            customButton.hUDMng = HUDMng;
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
        if (Input.GetButtonDown("Horizontal")) {
            buttonArray[selectedButton].GetComponent<CustomButton>().Unselect();
            if (Input.GetAxis("Horizontal") > 0) {
                selectedButton++;
                if (selectedButton > buttonArray.Length-1) {
                    selectedButton = 0;
                }
            }
            else {
                selectedButton--;
                if (selectedButton < 0) {
                    selectedButton = buttonArray.Length-1;
                }
            }
            SwapSelectedButton();
        }
    }

    public void ActivateSelectedButton() {
        if (Input.GetButtonDown("Jump")) {
            buttonDel();
        }
    }
}
