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
    public FishCaughtAttributes AchievementIkan = new FishCaughtAttributes(0, 0, 0);
    public Fisherman fishermanAccount;

    private bool isInside = false;
    private string fishType;
    private int fishingPoint = 0;
    public int fishingTarget = 1000;
    public FishCondition fishCondition = FishCondition.Catching;

    public Text fishingCountText, fishingPointText;
    public Text ikanBadut, IkanMarlin, ikanTiger;

    private void Start()
    {
        fishingCountText.text = fishermanAccount.Money.ToString();
    }

    private void Update()
    {
        if (fish != null)
        {
            fishingTarget = fish.GetComponent<FishComponent>().fish.maxPoint;

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
        //fishermanAccount.Money += this.fish.GetComponent<FishComponent>().fish.price; to produce money
        fishType = this.fish.GetComponent<FishComponent>().fish.name;
        if(fishType == "Tiger Shark")
        {
            AchievementIkan.checkNangkap(false, false, true);
            ikanTiger.text = AchievementIkan.IkanTigerText();
             //= this.fish.GetComponent<FishComponent>().fish;
        }
        else if(fishType == "Marlin")
        {
            AchievementIkan.checkNangkap(false, true, false);
            IkanMarlin.text = AchievementIkan.IkanMarlinTeks();
        }else
        {
            AchievementIkan.checkNangkap(true,false, false);
            ikanBadut.text = AchievementIkan.IkanBadutTeks();
        }
        fishermanAccount.AddFish(this.fish.GetComponent<FishComponent>().fish);
        fishingCountText.text = fishermanAccount.Money.ToString();

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
