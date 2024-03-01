using UnityEngine;

[CreateAssetMenu(fileName = "New Fisherman", menuName = "Fisherman")]
public class Fisherman : ScriptableObject
{
    // plasyer name and id (unique)
    public string id = "player1";

    // owned fishing rod list
    public FishingRod[] ownedFishingRods;

    // current use fishing rod
    public FishingRod activeFishingRod;

    // owned fishing bait list
    public FishingBait[] ownedFishingBaits;

    // current use fishing bait
    public FishingBait activeFishingBait;

    // catched fishes list
    public Fish[] catchedFishes;
}
