using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_Cust : MonoBehaviour
{
    IDictionary<int, GameObject> itemDictionary = new Dictionary<int, GameObject>();
    EquipableItems equipItems; // Reference to the EquipableItems classs

    public GameObject Mag;
    public GameObject Gas;
    public GameObject RBoots;
    public GameObject HighCal;
    public GameObject EDrink;
    public GameObject Serum;
    public GameObject BodyArmour;
    public GameObject Aimbot;
    public GameObject LowCal;
    public GameObject Clover;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    public int equipNum1 = 1;
    public int equipNum2 = 2;
    public int equipNum3 = 3;

    private Vector3 CharacterPosition;
    private Vector3 OffScreen;
    private int CharacterInt = 1;
    private SpriteRenderer MagRender,GasRender,RBootsRender,HighCalRender,EDrinkRender,SerumRender,BodyArmourRender,AimbotRender,LowCalRender,CloverRender;

    private void Awake()
    {
        equipItems = FindObjectOfType<EquipableItems>();

        itemDictionary.Add(1, Mag);
        itemDictionary.Add(2, Gas);
        itemDictionary.Add(3, RBoots);
        itemDictionary.Add(4, HighCal);
        itemDictionary.Add(5, EDrink);
        itemDictionary.Add(6, Serum);
        itemDictionary.Add(7, BodyArmour);
        itemDictionary.Add(8, Aimbot);
        itemDictionary.Add(9, LowCal);
        itemDictionary.Add(10, Clover);    
        
        foreach(KeyValuePair<int, GameObject> item in itemDictionary)
        {
            item.Value.SetActive(false);
        }

        #region Initialize default items
        itemDictionary[equipNum1].SetActive(true);
        itemDictionary[equipNum1].transform.position = pos1.position;

        itemDictionary[equipNum2].SetActive(true);
        itemDictionary[equipNum2].transform.position = pos2.position;

        itemDictionary[equipNum3].SetActive(true);
        itemDictionary[equipNum3].transform.position = pos3.position;
        #endregion
    }

    public void NextCharacter(int buttonNum)
    {
        foreach (KeyValuePair<int, GameObject> item in itemDictionary)
        {
            item.Value.SetActive(false);
        }

        #region Get Transform and Set equipnum
        switch (buttonNum)
        {
            case 1:
                equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 > 10) equipNum1 = 1;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
            case 2:
                equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 > 10) equipNum2 = 1;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                itemDictionary[equipNum2].transform.position = pos2.position;
                break;
            case 3:
                equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 > 10) equipNum3 = 1;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                itemDictionary[equipNum3].transform.position = pos3.position;
                break;
            default:
                equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 > 10) equipNum1 = 1;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
        }
        #endregion

        itemDictionary[equipNum1].SetActive(true);
        itemDictionary[equipNum2].SetActive(true);
        itemDictionary[equipNum3].SetActive(true);
        itemDictionary[equipNum1].transform.position = pos1.position;
        itemDictionary[equipNum2].transform.position = pos2.position;
        itemDictionary[equipNum3].transform.position = pos3.position;

        SetEquipItemsBoolValues();
    }

    public void PreviousCharacter(int buttonNum)
    {
        foreach (KeyValuePair<int, GameObject> item in itemDictionary)
        {
            item.Value.SetActive(false);
        }
        #region Get Transform and Set equipnum
        switch (buttonNum)
        {
            case 1:
                equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 < 1) equipNum1 = 10;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
            case 2:
                equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 < 1) equipNum2 = 10;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                itemDictionary[equipNum2].transform.position = pos2.position;
                break;
            case 3:
                equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 < 1) equipNum3 = 10;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                itemDictionary[equipNum3].transform.position = pos3.position;
                break;
            default:
                equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 < 1) equipNum1 = 10;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
        }
        #endregion

        itemDictionary[equipNum1].SetActive(true);
        itemDictionary[equipNum2].SetActive(true);
        itemDictionary[equipNum3].SetActive(true);
        itemDictionary[equipNum1].transform.position = pos1.position;
        itemDictionary[equipNum2].transform.position = pos2.position;
        itemDictionary[equipNum3].transform.position = pos3.position;

        SetEquipItemsBoolValues();
    }

    void SetEquipItemsBoolValues()
    {
        #region Set Boolean values in EquipableItems class
        equipItems.bigMagazine = Mag.activeSelf;
        equipItems.gasoline = Gas.activeSelf;
        equipItems.rocketBoots = RBoots.activeSelf;
        equipItems.highCalBullets = HighCal.activeSelf;
        equipItems.energyDrink = EDrink.activeSelf;
        equipItems.specialSerum = Serum.activeSelf;
        equipItems.bodyArmor = BodyArmour.activeSelf;
        equipItems.aimbotChip = Aimbot.activeSelf;
        equipItems.lowCalBullet = LowCal.activeSelf;
        equipItems.fourLeafClover = Clover.activeSelf;
        #endregion

        equipItems.ApplyEffects(); // Apply the effects only after modifying them
    }
}
