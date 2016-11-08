using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour {
    public enum ButtonType{
        StartButton,
        OptionButton
    }
    public ButtonType myButtonType;
    public Image activeMarker;
    public HUDController hUDControl;

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
        hUDControl.buttonDel = MyButtonAction;
    }

    public virtual void MyButtonAction() {

    }
}
