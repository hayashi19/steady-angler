using System.Collections;
using UnityEngine;

public class StabilizationComponent : MonoBehaviour
{
    public GameObject stabilizationBar, stabilizationArea, stabilizationPoint, stabilizationLeftBound, stablizationRightBound, fish;

    [SerializeField] private float areaStrengthLeft = 1f;
    [SerializeField] private float areaStrengthRight = 1f;
    [SerializeField] private float areaDirection = 1f;
    [SerializeField] private float pointForce = 0.4f;

    public IEnumerator AnimAreaMovement(GameObject baitedFish)
    {
        // get baited fish info
        FishComponent fishComponent = baitedFish.GetComponent<FishComponent>();
        FishAttribute fishAttribute = fishComponent.fish;

        // set drag or slowness of area movement
        Rigidbody2D areaBody = stabilizationArea.GetComponent<Rigidbody2D>();
        areaBody.drag = 10f * fishAttribute.strenght;

        while (true)
        {
            // get position information
            Vector3 areaPost = stabilizationArea.transform.position;
            Vector3 rightBoundPost = stabilizationLeftBound.transform.position;
            Vector3 leftBoundPost = stablizationRightBound.transform.position;

            // calculate distance with left and right bound, prevent out of bound and stuck at one side
            areaStrengthLeft = Vector2.Distance(areaPost, rightBoundPost);
            areaStrengthRight = Vector2.Distance(areaPost, leftBoundPost);

            // calculate move direction
            areaDirection = Mathf.PerlinNoise1D(Time.time);
            areaDirection = Calculation.Remap(areaDirection, 0, 1, -1 * areaStrengthLeft, 1 * areaStrengthRight);

            // move the area randomly
            ConstantForce2D areaCForce = stabilizationArea.GetComponent<ConstantForce2D>();
            areaCForce.relativeForce = areaDirection * Vector2.right;

            yield return new WaitForEndOfFrame();
        }
    }

    public void ForcePoint(GameObject baitedFish)
    {
        // get baited fish info
        FishComponent fishComponent = baitedFish.GetComponent<FishComponent>();
        FishAttribute fishAttribute = fishComponent.fish;

        // set drag or slowness of area movement
        Rigidbody2D pointBody = stabilizationPoint.GetComponent<Rigidbody2D>();
        pointBody.drag = 16f * fishAttribute.strenght;
        pointBody.AddRelativeForce(Vector2.right * pointForce, ForceMode2D.Impulse);
    }

    public void Activate()
    {
        ConstantForce2D areaForce = stabilizationArea.GetComponent<ConstantForce2D>();
        ConstantForce2D pointForce = stabilizationPoint.GetComponent<ConstantForce2D>();

        areaForce.enabled = true;
        pointForce.enabled = true;
    }

    public void Deactivate()
    {
        ConstantForce2D areaForce = stabilizationArea.GetComponent<ConstantForce2D>();
        ConstantForce2D pointForce = stabilizationPoint.GetComponent<ConstantForce2D>();

        areaForce.enabled = false;
        pointForce.enabled = false;
    }
}