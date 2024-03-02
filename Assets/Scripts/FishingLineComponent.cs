using UnityEngine;

public class FishingLineComponent : MonoBehaviour
{
    public GameObject fisherman;
    public GameObject fishingBait;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (fisherman != null && fishingBait != null)
        {
            lineRenderer.SetPosition(0, fisherman.transform.position);
            lineRenderer.SetPosition(1, fishingBait.transform.position);
        }
        else
        {
            Debug.LogError("Start or End GameObject not assigned!");
        }
    }
}
