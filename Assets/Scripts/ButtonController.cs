using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CharacterController characterController;

    // Set this to -1 for left, 1 for right, and 0 for no movement.
    private int movementDirection = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "LeftButton")
        {
            movementDirection = -1;
        }
        else if (eventData.pointerEnter.name == "RightButton")
        {
            movementDirection = 1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movementDirection = 0;
    }

    private void Update()
    {
        characterController.Move(movementDirection, false, false);
    }
}
