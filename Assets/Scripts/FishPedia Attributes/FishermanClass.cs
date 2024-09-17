using System;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Fisherman", menuName = "Fisherman")]
public class Fisherman : ScriptableObject
{
    // plasyer name and id (unique)
    public string id = "1";
    public new string name = "player1";

    public int Money = 0;

    // owned fishing rod list
    public FishingRod[] ownedFishingRods;

    // current use fishing rod
    public FishingRod activeFishingRod;

    // owned fishing bait list
    public FishingBait[] ownedFishingBaits;

    // current use fishing bait
    public FishingBait activeFishingBait;

    // catched fishes list
    public Fish[] caughtFishes = new Fish[0];

    // Method to add a fish to the caught fishes list
    public void AddFish(Fish fish)
    {
        Array.Resize(ref caughtFishes, caughtFishes.Length + 1); // Resize the array to accommodate the new fish
        caughtFishes[caughtFishes.Length - 1] = fish; // Add the new fish to the end of the array
    }

    // Method to remove a fish from the caught fishes list
    public void RemoveFish(Fish fish)
    {
        caughtFishes = caughtFishes.Where(x => x != fish).ToArray(); // Remove the fish from the array
    }
}
