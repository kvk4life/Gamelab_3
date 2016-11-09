using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDController : MonoBehaviour {
    public delegate void ButtonDelegate();
    public ButtonDelegate buttonDel;
    public GameObject HUDMng;
    public GameObject[] buttonArray;
    public int selectedButton;
    public bool firstTimeAwoken;

    public void Awake() {
       if (!firstTimeAwoken) {
            foreach (GameObject buttonsInArray in buttonArray) {
                buttonsInArray.GetComponent<CustomButton>().hUDControl = this;
                buttonsInArray.GetComponent<CustomButton>().hUDMng = HUDMng;
            }
            firstTimeAwoken = true;
        }
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
            buttonArray[selectedButton].GetComponent<CustomButton>().Selected();
        }
    }

    public void ActivateSelectedButton() {
        if (Input.GetButtonDown("Jump")) {
            print(selectedButton);
            buttonDel();
        }
    }
}
