using System.Collections;
using UnityEngine;

public class FishingLineComponent : MonoBehaviour
{
    public GameObject rod;
    public GameObject bait;

    private void Start() {
        StartCoroutine(DrawFishingLine());
    }

    public IEnumerator DrawFishingLine()
    {
        LineRenderer line = this.rod.GetComponent<LineRenderer>();

        while (true)
        {
            line.positionCount = 2;

            Vector3 fishingRodPost = this.rod.transform.position;
            fishingRodPost.z = -1;
            line.SetPosition(0, fishingRodPost);

            Vector3 fishingBaitPost = this.bait.transform.position;
            fishingBaitPost.z = -1;
            line.SetPosition(1, fishingBaitPost);

            yield return new WaitForEndOfFrame();
        }
    }
}
