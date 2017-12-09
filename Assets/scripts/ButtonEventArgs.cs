using System;

public class ButtonEventArgs : EventArgs {
    public bool status;
    public ButtonEventArgs(bool status) {
        this.status = status;
    }
}