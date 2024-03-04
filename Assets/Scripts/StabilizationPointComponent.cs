using UnityEngine;
using UnityEngine.UI;

public enum FishCondition
{
    Catching, Caught, Released
}

public class StabilizationPointComponent : MonoBehaviour
{
    public GameObject fish = null;
    public FishingBaitComponent bait;

    private bool isInside = false;
    private int fishCount = 0;
    private int fishingPoint = 0;
    public int fishingTarget = 1000;
    public FishCondition fishCondition = FishCondition.Catching;

    public Text fishingCountText, fishingPointText;

    private void Update()
    {
        if (fish != null)
        {
            if (fishingPoint > -fishingTarget && fishingPoint < fishingTarget)
            {
                if (isInside)
                {
                    fishingPoint++;
                    fishingPointText.text = fishingPoint.ToString() + "p";
                }
                else
                {
                    fishingPoint--;
                    fishingPointText.text = fishingPoint.ToString() + "p";
                }
            }
            else
            {
                if (fishingPoint <= -fishingTarget && fishCondition == FishCondition.Catching)
                {
                    LoseCondition();
                }
                else if (fishingPoint >= fishingTarget && fishCondition == FishCondition.Catching)
                {
                    WinCondition();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInside = false;
    }

    public FishCondition GetFishCondition() { return fishCondition; }

    private void WinCondition()
    {
        // set condition to caught fish
        fishCondition = FishCondition.Caught;

        // break the joint
        BreakJoint();

        // fish to the boat
        this.fish.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);

        // set the stabilization off
        StopStabilization();

        // reset stabilization fishing point
        ResetFishingPoint();

        // add to fish count
        fishCount += this.fish.GetComponent<FishComponent>().fish.price;
        fishingCountText.text = fishCount.ToString();

        // set fish null
        this.fish = null;

        bait.TakeBait();

        print("You Win");

        fishCondition = FishCondition.Catching;
    }
    private void LoseCondition()
    {
        fishCondition = FishCondition.Released;

        // break the joint
        BreakJoint();

        // fish swim away
        this.fish.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, -5), ForceMode2D.Impulse);

        // set the stabilization off
        StopStabilization();

        // reset stabilization fishing point
        ResetFishingPoint();

        // set fish null
        this.fish = null;

        bait.TakeBait();

        print("You Lose");

        fishCondition = FishCondition.Catching;
    } 

    private void BreakJoint()
    {
        this.fish.GetComponent<SpringJoint2D>().connectedBody = null;
        this.fish.GetComponent<SpringJoint2D>().enabled = false;
    }

    private void StopStabilization() {
        //this.GetComponentInParent<StabilizationComponent>().fish = null;

        this.GetComponentInParent<StabilizationComponent>().StopStabilization();
    }

    private void ResetFishingPoint()
    {
        fishingPoint = 0;
        fishingPointText.text = fishingPoint.ToString() + "p";
    }
}
