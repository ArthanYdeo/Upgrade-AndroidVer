using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public Image popupImage;
     // Reference to the Image GameObject
    private bool isPopupActive = false;

    // Called when the button is clicked
    public void TogglePopup()
    {
        isPopupActive = !isPopupActive;
        popupImage.gameObject.SetActive(isPopupActive);
    }


}
