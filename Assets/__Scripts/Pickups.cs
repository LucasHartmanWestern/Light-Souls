using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
    Selector_Cust selector; // Reference to the equippable selector

    public enum Item{BigMag, Gas, RocketBoots, HighCalBullet, EnergyDrink, Serum, BodyArmour, Aimbot, LowCalBullet, Clover}; // Pickup options
    public Item pickupItem; // Specific pickup selected

    public LayerMask whatIsPlayer; // Layermask for player

    public ParticleSystem pickupPS; // Particle system to play when player picks up item

    GameObject popup; // Popup
    GameObject canvasPrefab; // Canvas prefab
    GameObject popupCanvas; // Reference to new canvas clone

    GameObject Mag;
    GameObject Gas;
    GameObject RBoots;
    GameObject HighCal;
    GameObject EDrink;
    GameObject Serum;
    GameObject BodyArmour;
    GameObject Aimbot;
    GameObject LowCal;
    GameObject Clover;

    // Start is called before the first frame update
    void Start()
    {
        selector = FindObjectOfType<Selector_Cust>(); // Get instance
        canvasPrefab = FindObjectOfType<StatDisplay>().transform.Find("Popups").Find("PickupPopup").gameObject;

        #region Get Objects From Selector_Cust
        Mag = selector.Mag;
        Gas = selector.Gas;
        RBoots = selector.RBoots;
        HighCal = selector.HighCal;
        EDrink = selector.EDrink;
        Serum = selector.Serum;
        BodyArmour = selector.BodyArmour;
        Aimbot = selector.Aimbot;
        LowCal = selector.LowCal;
        Clover = selector.Clover;
        #endregion

        #region Add and format popup image
        popupCanvas = Instantiate(canvasPrefab, canvasPrefab.transform.parent);
        popup = Instantiate(GetObjectFromEnum(), popupCanvas.transform);
        popup.transform.localScale = new Vector3(0.15f, 0.75f, 1);
        popup.GetComponent<Image>().rectTransform.anchorMin = new Vector2(0.2f, 0.5f);
        popup.GetComponent<Image>().rectTransform.anchorMax = new Vector2(0.2f, 0.5f);
        popup.transform.localPosition = new Vector3(0, 0, 0);
        popup.GetComponent<Image>().rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
        #endregion

        #region Get rid of object if player already has it
        switch (GetObjectFromEnum().name)
        {
            case "Gas": if (selector.gasItem.enabled == true) { Destroy(gameObject); } break;
            case "Serum": if (selector.serumItem.enabled == true) { Destroy(gameObject); } break;
            case "BodyArmor": if (selector.bodyArmourItem.enabled == true) { Destroy(gameObject); } break;
            case "Clover": if (selector.cloverItem.enabled == true) { Destroy(gameObject); } break;
            case "Aimbot": if (selector.aimbotItem.enabled == true) { Destroy(gameObject); } break;
            case "EnergyDrink": if (selector.eDrinkItem.enabled == true) { Destroy(gameObject); } break;
            case "BigMag": if (selector.magItem.enabled == true) { Destroy(gameObject); } break;
            case "HighCal": if (selector.highCalItem.enabled == true) { Destroy(gameObject); } break;
            case "LowCal": if (selector.lowCalItem.enabled == true) { Destroy(gameObject); } break;
            case "RocketBoots": if (selector.rBootItem.enabled == true) { Destroy(gameObject); } break;
            default: break;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, 2, whatIsPlayer))
        {
            #region Handle player in area
            popupCanvas.SetActive(true); // Show popup canvas
            popup.SetActive(true); // Show popup

            #region Handle character picking up item
            if (Input.GetKeyDown(KeyCode.E))
            {
                selector.AddEquippable(GetObjectFromEnum());
                popupCanvas.SetActive(false); // Close popup
                ParticleSystem newPS = Instantiate(pickupPS); // Create a particle system
                newPS.transform.position = transform.position; // Move ps to the pickup
                Destroy(popup); // Destroy popup
                Destroy(popupCanvas); // Destroy canvas
                Destroy(gameObject); // Destroy pickup
                
            }
            #endregion
            #endregion

        }
        else
        {
            #region Handle player not in area
            popupCanvas.SetActive(false); // Hide popup canvas
            popup.SetActive(false); // Don't show popup
            #endregion
        }
    }

    // Get the gameobject based on the enum selected
    private GameObject GetObjectFromEnum()
    {
        switch(pickupItem)
        {
            case (Item.BigMag): return Mag;
            case (Item.Gas): return Gas;
            case (Item.RocketBoots): return RBoots;
            case (Item.HighCalBullet): return HighCal;
            case (Item.EnergyDrink): return EDrink;
            case (Item.Serum): return Serum;
            case (Item.BodyArmour): return BodyArmour;
            case (Item.Aimbot): return Aimbot;
            case (Item.LowCalBullet): return LowCal;
            case (Item.Clover): return Clover;
            default: return null;
        }
    }
}
