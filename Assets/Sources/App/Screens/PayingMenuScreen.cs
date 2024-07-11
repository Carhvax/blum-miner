using UnityEngine;

public class PayingMenuScreen : MenuScreen {
    [SerializeField] private NotificationPopup _popup;
    
    public void Notify() => _popup.Show();
}