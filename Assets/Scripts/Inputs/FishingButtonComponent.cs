using UnityEngine;
using UnityEngine.EventSystems;

public class FishingButtonComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public FishermanComponent fishermanComponent;

    private bool isButtonHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeld = true;
        fishermanComponent.Fishing(isButtonHeld);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;
        fishermanComponent.Fishing(isButtonHeld);
    }
}