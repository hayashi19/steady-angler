using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeRod : MonoBehaviour
{
    public FishingRod[] listRods;
    public Text rod_name;
    public Text rod_price;
    private int rod_id;
    public GameObject buttonBuy;
    public GameObject buttonEquip;
    public GameObject buttonEquiped;
    public void findEquiped()
    {
        for (int i = 0; i < listRods.Length; i++)
        {
            if (listRods[i].equip)
            {
                rod_id = i;
            }
        }
        display();
    }

    public void onClickLeft()
    {
        if (rod_id > 0)
        {
            rod_id--;
        }
        display();
    }

    public void onClickRight()
    {
        if (rod_id < listRods.Length - 1)
        {
            rod_id++;
        }  
        display();
    }

    void checkStatus()
    {
        if (listRods[rod_id].status)
        {
            buttonBuy.SetActive(false);
        }
        else if (!listRods[rod_id].status)
        {
            buttonBuy.SetActive(true);
            rod_price.text = listRods[rod_id].price.ToString();

            buttonEquip.SetActive(false);
            buttonEquiped.SetActive(false);
        }
    }
    void checkEquip()
    {
        if (listRods[rod_id].equip && listRods[rod_id].status)
        {
            buttonEquiped.SetActive(true);
            buttonEquip.SetActive(false);
        }
        else if (!listRods[rod_id].equip && listRods[rod_id].status)
        {
            buttonEquip.SetActive(true);
            buttonEquiped.SetActive(false);
        }
    }

    void display()
    {
        rod_name.text = listRods[rod_id].name;
        checkStatus();
        checkEquip();
    }

    public void onClickBuy()
    {
        listRods[rod_id].status = true;
        display();
    }

    public void onClickEquip()
    {
        for (int i = 0;i < listRods.Length; i++)
        {
            listRods[i].equip = false;
        }
        listRods[rod_id].equip = true;
        display();
    }

    void Start()
    {
        findEquiped();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
