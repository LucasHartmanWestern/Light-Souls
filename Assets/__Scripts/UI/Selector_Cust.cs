using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector_Cust : MonoBehaviour
{
    public static Selector_Cust Instance;

    IDictionary<int, GameObject> itemDictionary = new Dictionary<int, GameObject>();
    EquipableItems equipItems; // Reference to the EquipableItems classs
    PlayerAttack playerAttack; // Reference to the PlayerAttack class

    public GameObject UIItemsContainer; // Reference to the UIItems object

    #region Get References
    [Header("Selector Game Objects")]
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
    [Header("Item Images")]
    public Image gasItem;
    public Image magItem;
    public Image rBootItem;
    public Image highCalItem;
    public Image eDrinkItem;
    public Image serumItem;
    public Image bodyArmourItem;
    public Image aimbotItem;
    public Image lowCalItem;
    public Image cloverItem;
    [Header("Seletor Transforms")]
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    #endregion

    public int equipNum1 = 1;
    public int equipNum2 = 2;
    public int equipNum3 = 3;

    public bool mechDead; // Track if mech boss is dead

    void Awake()
    {
        HandleInventory();
    }

    private void Start()
    {
        // Check if already an equippable item in the scene
        if (Instance != null)
        {
            Destroy(gameObject); // Get rid of new instance
            return;
        }
        // Set new instance
        Instance = this;
        DontDestroyOnLoad(gameObject); // Don't destroy this object

        equipItems = FindObjectOfType<EquipableItems>(); // Get EquipableItems instance
        playerAttack = FindObjectOfType<PlayerAttack>(); // Get PlayerAttack instance

        #region Populate Dictionary
        itemDictionary.Add(1, Clover);
        itemDictionary.Add(2, Serum);
        itemDictionary.Add(3, BodyArmour);
        
        foreach(KeyValuePair<int, GameObject> item in itemDictionary)
        {
            item.Value.SetActive(false);
        }
        #endregion

        #region Initialize default items
        itemDictionary[equipNum1].SetActive(true);
        itemDictionary[equipNum1].transform.position = pos1.position;

        itemDictionary[equipNum2].SetActive(true);
        itemDictionary[equipNum2].transform.position = pos2.position;

        itemDictionary[equipNum3].SetActive(true);
        itemDictionary[equipNum3].transform.position = pos3.position;
        #endregion

        SetEquipItemsBoolValues();
        HandleInventory();
    }

    // Runs every frame
    private void Update()
    {
        if (equipItems == null)
            equipItems = FindObjectOfType<EquipableItems>(); // Get EquipableItems instance
        if (playerAttack == null)
            playerAttack = FindObjectOfType<PlayerAttack>(); // Get PlayerAttack instance

        if (Input.GetKeyDown(KeyCode.Escape)) // Check if player pressed the escape key
        {
            UIItemsContainer.SetActive(!UIItemsContainer.activeSelf); // Open/close item container
            playerAttack.dontAttack = UIItemsContainer.activeSelf; // Don't let player attack when menu is open
        }    
    }

    // Add a new possible equippable for the player to use
    public void AddEquippable(GameObject newItem)
    {
        itemDictionary.Add(itemDictionary.Count + 1, newItem);

        HandleInventory();
    }

    // Handles the items in the inventory
    public void HandleInventory()
    {
        #region Disable all items by default
        gasItem.enabled = false;
        serumItem.enabled = false;
        bodyArmourItem.enabled = false;
        cloverItem.enabled = false;
        aimbotItem.enabled = false;
        eDrinkItem.enabled = false;
        magItem.enabled = false;
        highCalItem.enabled = false;
        lowCalItem.enabled = false;
        rBootItem.enabled = false;
        #endregion

        #region Loop through and re-enable the available items
        foreach (GameObject item in itemDictionary.Values)
        {
            switch(item.name)
            {
                case "Gas": gasItem.enabled = true; break;
                case "Serum": serumItem.enabled = true; break;
                case "BodyArmor": bodyArmourItem.enabled = true; break;
                case "Clover": cloverItem.enabled = true; break;
                case "Aimbot": aimbotItem.enabled = true; break;
                case "EnergyDrink": eDrinkItem.enabled = true; break;
                case "BigMag": magItem.enabled = true; break;
                case "HighCal": highCalItem.enabled = true; break;
                case "LowCal": lowCalItem.enabled = true; break;
                case "RocketBoots": rBootItem.enabled = true; break;
                default: return;
            } 
        }
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
                if (equipNum1 > itemDictionary.Count) equipNum1 = 1;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
            case 2:
                equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 > itemDictionary.Count) equipNum2 = 1;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2++;
                itemDictionary[equipNum2].transform.position = pos2.position;
                break;
            case 3:
                equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 > itemDictionary.Count) equipNum3 = 1;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3++;
                itemDictionary[equipNum3].transform.position = pos3.position;
                break;
            default:
                equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1++;
                if (equipNum1 > itemDictionary.Count) equipNum1 = 1;
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
                if (equipNum1 < 1) equipNum1 = itemDictionary.Count;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                itemDictionary[equipNum1].transform.position = pos1.position;
                break;
            case 2:
                equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 < 1) equipNum2 = itemDictionary.Count;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                if (equipNum2 == equipNum1 || equipNum2 == equipNum3) equipNum2--;
                itemDictionary[equipNum2].transform.position = pos2.position;
                break;
            case 3:
                equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 < 1) equipNum3 = itemDictionary.Count;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                if (equipNum3 == equipNum1 || equipNum3 == equipNum2) equipNum3--;
                itemDictionary[equipNum3].transform.position = pos3.position;
                break;
            default:
                equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 == equipNum2 || equipNum1 == equipNum3) equipNum1--;
                if (equipNum1 < 1) equipNum1 = itemDictionary.Count;
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
