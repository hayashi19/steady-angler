using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishermanComponent : MonoBehaviour
{
    // [SerializeField] private FishingStateController fishingStateController;

    [SerializeField] private GameObject audioO, rod, bait, seaLevel, stabilization, stabilizationBar, stabilizationArea, stabilizationPoint;
    private GameObject baitedFish;
    [SerializeField] private Text fishingPoinText, caughtPointText;

    public enum FishingState { IDLE, CASTING, CATCHING }
    public FishingState currentState = FishingState.IDLE;

    private Coroutine castHook, pullHook, fishSwim, areaMove, countingPoint = null;

    private float fishingPoint = 0;
    private float caughtPoint = 0;

    public void Fishing(bool isHeld)
    {
        switch (currentState)
        {
            // Condition: When the fisherman does nothing (idle)
            // Trigger: Player click fishing button
            // Action: Fisherman cast the hook
            case FishingState.IDLE:
                {
                    // when button down, cast hook
                    if (isHeld)
                    {
                        // check if the hook or bait already above water
                        // then player allow to cast
                        if (bait.transform.position.y > seaLevel.transform.position.y)
                        {
                            // play audio and animation
                            audioO.GetComponent<AudioComponent>().PlaySFX("THROW");
                            castHook = StartCoroutine(AnimateCastHook());

                            // change state to casting, as the fisherman already cast the hook
                            currentState = FishingState.CASTING;
                        }
                    }
                    // when button up, 
                    else
                    {

                    }
                    break;
                }
            // Condition: When the fisherman just casted the hook
            // Trigger: Player click fishing button, down and up
            // Action:
            //  - Button down, for pull back hook
            //  - Button up, for still fishing stop the pull
            case FishingState.CASTING:
                {
                    // when button down, pull hook
                    if (isHeld)
                    {
                        // play audio and animation
                        audioO.GetComponent<AudioComponent>().PlaySFX("RETRIEVE", volume: 0.3f, loop: true);
                        pullHook = StartCoroutine(AnimatePullHook(isHeld));
                    }
                    // when button up, stop pulling the hook
                    else
                    {
                        // check if there is a fish near to get baited
                        CheckBait();

                        // stop audio
                        audioO.GetComponent<AudioComponent>().StopSFX();

                        // stop animation
                        if (pullHook != null)
                        {
                            StopCoroutine(pullHook);
                            pullHook = null;
                        }
                    }
                    break;
                }
            // Condition: when there is a fish just get baited
            // Tigger: player click fishing button, the fish near the hook and bait
            // Action: start the stabilization, and player start to stabilize the point
            case FishingState.CATCHING:
                {
                    // when button down, balancing point
                    if (isHeld)
                    {
                        // play reel audio while catching
                        audioO.GetComponent<AudioComponent>().PlaySFX("CATCHING");

                        // stabilize the point
                        StabilizationComponent stabilizationComponent = stabilization.GetComponent<StabilizationComponent>();
                        stabilizationComponent.ForcePoint(baitedFish);
                    }
                    // when button up, 
                    else
                    {
                        // stop audio
                        audioO.GetComponent<AudioComponent>().StopSFX();
                    }
                    break;
                }
        }
    }

    private IEnumerator AnimateCastHook()
    {
        // set the distance to far away
        SpringJoint2D rodJoint = rod.GetComponent<SpringJoint2D>();
        rodJoint.distance = 5.6f;

        // do throwing effect
        Rigidbody2D baitBody = bait.GetComponent<Rigidbody2D>();
        Vector2 throwDirection = (Vector2.right + Vector2.up) * 2f;
        baitBody.AddForce(throwDirection, ForceMode2D.Impulse);

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator AnimatePullHook(bool isPull)
    {
        while (isPull)
        {
            SpringJoint2D rodJoint = rod.GetComponent<SpringJoint2D>();

            // when hook not at the fisherman hand
            // do pull animation
            if (rodJoint.distance >= 0.05f)
            {
                rodJoint.distance -= 2 * Time.deltaTime;
            }
            // when hook at the fisherman hand
            // set of the sound and pulling action
            else
            {
                audioO.GetComponent<AudioComponent>().StopSFX();
                StopCoroutine(pullHook);

                currentState = FishingState.IDLE;
            };

            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckBait()
    {
        // get fish that near the bait
        BaitComponent baitComponent = bait.GetComponent<BaitComponent>();
        baitedFish = baitComponent.fish;

        // if the fish is baited
        if (baitedFish != null)
        {
            // join the baited fish to the bait and hook
            SpringJoint2D baitJoint = bait.GetComponent<SpringJoint2D>();
            baitJoint.enabled = true;
            baitJoint.connectedAnchor = new Vector2(0.1f, 0.5f);
            baitJoint.connectedBody = baitedFish.GetComponent<Rigidbody2D>();

            // play fish swim animation
            FishComponent fishComponent = baitedFish.GetComponent<FishComponent>();
            fishSwim = StartCoroutine(fishComponent.AnimSwimAway());

            // move and start the stabilization
            StabilizationComponent stabilizationComponent = stabilization.GetComponent<StabilizationComponent>();
            areaMove = StartCoroutine(stabilizationComponent.AnimAreaMovement(baitedFish));
            stabilizationComponent.Activate();

            // start counting fishing point
            countingPoint = StartCoroutine(CountFishingPoint());

            // set to catching
            currentState = FishingState.CATCHING;
        }
    }

    private IEnumerator CountFishingPoint()
    {
        // get stabilization information
        StabilizationComponent stabilizationComponent = stabilization.GetComponent<StabilizationComponent>();
        StabilizationPointComponent stabilizationPointComponent = stabilizationPoint.GetComponent<StabilizationPointComponent>();

        // get the baited fish information
        Rigidbody2D fishBody = baitedFish.GetComponent<Rigidbody2D>();
        FishAttribute fishAttribute = baitedFish.GetComponent<FishComponent>().fish;

        while (true)
        {
            // when fishing point reach max of fish point
            // player get the fish
            if (fishingPoint >= fishAttribute.max)
            {
                // play audio
                audioO.GetComponent<AudioComponent>().PlaySFX("CAUGHT");

                // add caught point and display
                caughtPoint += fishAttribute.point;
                caughtPointText.text = caughtPoint.ToString();

                // reset
                ResetToIdle(stabilizationComponent, baitedFish);

                // get the fish
                fishBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            // when fishing point reach the min of fish point
            // the fish swim away
            else if (fishingPoint <= fishAttribute.min)
            {
                // play audio
                audioO.GetComponent<AudioComponent>().PlaySFX("SWIM");

                // add caught point and display
                caughtPoint -= fishAttribute.point;
                caughtPointText.text = caughtPoint.ToString();

                // reset
                ResetToIdle(stabilizationComponent, baitedFish);

                // get the fish
                fishBody.AddForce(Vector2.right * 8 + Vector2.down * 6f, ForceMode2D.Impulse);
            }

            // add fishing point when stabilization point inside the stabilization area
            if (stabilizationPointComponent.isInside) fishingPoint += 0.4f;
            else fishingPoint -= 0.4f;

            // display fishing point
            fishingPoinText.text = Math.Round(fishingPoint).ToString();

            yield return new WaitForEndOfFrame();
        }
    }

    private void ResetToIdle(StabilizationComponent stabilizationComponent, GameObject baitedFish)
    {
        // prevent to bait the same fish that have been caught 
        baitedFish.GetComponent<CapsuleCollider2D>().enabled = false;
        
        // remove baited fish
        baitedFish = null;

        // stop stabilization
        stabilizationComponent.Deactivate();

        // stop all prev state animation
        StopCoroutine(fishSwim);
        StopCoroutine(areaMove);
        StopCoroutine(countingPoint);

        // pull back the hook
        SpringJoint2D rodJoint = rod.GetComponent<SpringJoint2D>();
        rodJoint.distance = 0.05f;

        // release the fish
        SpringJoint2D baitJoint = bait.GetComponent<SpringJoint2D>();
        baitJoint.enabled = false;

        // reset fishing point
        fishingPoint = 0;
        fishingPoinText.text = fishingPoint.ToString();

        // fisherman do nothing again
        currentState = FishingState.IDLE;
    }
}
