using System.Collections;
using UnityEngine;

public class FishComponent : MonoBehaviour
{
    public FishAttribute fish;

    public IEnumerator AnimSwimAway()
    {
        Rigidbody2D fishBody = this.GetComponent<Rigidbody2D>();
        fishBody.freezeRotation = true;
        while (fishBody)
        {
            fishBody.AddForce(new Vector2(0.4f, -0.8f) * (1f - fish.rarity), ForceMode2D.Impulse);

            float calmDuration = Mathf.PerlinNoise1D(Time.time) * 0.5f;

            yield return new WaitForSeconds(calmDuration);
        }
    }
}