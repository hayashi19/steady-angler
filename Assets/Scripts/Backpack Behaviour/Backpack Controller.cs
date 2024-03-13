using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    [SerializeField] private Fisherman fishingAccount;
    public Transform inventoryParent;
    [SerializeField] private Sprite[] inventorySlots;
    Image HighlightItems;
    void Start()
    {
        UpdateInventory();
    }
    

    public void UpdateInventory()
    {
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
  
}
