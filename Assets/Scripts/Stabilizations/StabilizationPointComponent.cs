using UnityEngine;

public class StabilizationPointComponent : MonoBehaviour
{
    public bool isInside = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        isInside = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isInside = false;
    }
}