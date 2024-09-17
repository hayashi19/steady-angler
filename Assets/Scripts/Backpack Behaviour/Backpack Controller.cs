using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    [SerializeField] private Fisherman fishingAccount; //this script is equipped in inventory backpack gameobject
    public Transform inventoryParent;
    [SerializeField] private Sprite[] inventorySlots;
    private StabilizationPointComponent showingMoney;
    Image HighlightItems;
    void Start()
    {
        UpdateInventory();
    }
    
    public void SellAllFish()
    {
        int totalMoney = 0;

        foreach (var fish in fishingAccount.caughtFishes)
        {
            totalMoney += fish.price;
        }
        // Add totalMoney to the fishingAccount money
        fishingAccount.Money += totalMoney;
        

        // Clear caughtFishes array
        fishingAccount.caughtFishes = new Fish[0];
        UpdateInventory();
        //showingMoney.updateMoney();
    }
    public void UpdateInventory()
    {
        ClearInventory();
        int inventorySlotCount = inventoryParent.childCount;

        for(int i = 0; i < inventorySlotCount; i++)
        {
            if(i >= fishingAccount.caughtFishes.Length)
            {
                break;
            }
            HighlightItems = inventoryParent.GetChild(i).GetChild(0).GetComponentInChildren<Image>(); //a parent can have multiple children properties, Getchild must ask which index it is
            // and because inventoryParents is backpack, GetChild(i) means to get which index is the inventorySlot, and Getchild(0) because there's only 1 child and it's Button (legacy)
            //object located at index 0
            Image Slot = inventoryParent.GetChild(i).GetComponentInChildren<Image>();
            Slot.sprite = fishingAccount.caughtFishes[i].sprite;
            HighlightItems.sprite = fishingAccount.caughtFishes[i].sprite;
        }
    }
    private void ClearInventory()
    {
        // Clear all inventory slots by destroying their children
        foreach (Transform slot in inventoryParent)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
