using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour {
    public enum ButtonType{
        StartButton,
        OptionButton,
        SkilltreeButton,
        HighscoreButton,
        ExitGameButton
    }
    public ButtonType myButtonType;
    [HideInInspector]
    public HUDController hUDControl;
    public Image activeMarker;
    public HUDController.ButtonDelegate buttonDel;

    public void Start() {
        DecideTheDel();
    }

    public void DecideTheDel() {
        switch (myButtonType) {
            case ButtonType.StartButton:
                buttonDel = ButtonStart;
                break;
            case ButtonType.OptionButton:
                buttonDel = ButtonOption;
                break;
            case ButtonType.SkilltreeButton:
                buttonDel = ButtonSkilltree;
                break;
            case ButtonType.HighscoreButton:
                buttonDel = ButtonHighscore;
                break;
            case ButtonType.ExitGameButton:
                buttonDel = ButtonExit;
                break;
        }
    }

    public virtual void Unselect() {
        activeMarker.GetComponent<CanvasGroup>().alpha = 0;
        EmptyDelegate();
    }

    public virtual void Selected() {
        activeMarker.GetComponent<CanvasGroup>().alpha = 1;
        FillDelegate();
    }

    public virtual void EmptyDelegate() {
        hUDControl.buttonDel = null;
    }

    public virtual void FillDelegate() {
        hUDControl.buttonDel = buttonDel;
    }

    public void ButtonOption() {
        print("I'm the option button!");
    }

    public void ButtonHighscore() {
        print("I'm the Highscores button!");
    }

    public void ButtonStart() {
        print("I'm the Start button!");
    }

    public void ButtonExit() {
        print("I'm the Exit Game Button!");
    }

    public void ButtonSkilltree() {
        print("I'm the Skilltree button!");
    }
}
