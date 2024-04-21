using UnityEngine;

public class BaitComponent : MonoBehaviour
{
    public GameObject fish = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fish == null)
        {
            fish = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        fish = null;
    }
}
