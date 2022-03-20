using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_Cust : MonoBehaviour
{
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

    private Vector3 CharacterPosition;
    private Vector3 OffScreen;
    private int CharacterInt = 1;
   private SpriteRenderer MagRender,GasRender,RBootsRender,HighCalRender,EDrinkRender,SerumRender,BodyArmourRender,AimbotRender,LowCalRender,CloverRender;

    private void Awake()
   {
      CharacterPosition = Aimbot.transform.position;
      OffScreen = EDrink.transform.position;
      AimbotRender = Aimbot.GetComponent<SpriteRenderer>();
      MagRender = Aimbot.GetComponent<SpriteRenderer>();
      GasRender = Aimbot.GetComponent<SpriteRenderer>();
      RBootsRender = Aimbot.GetComponent<SpriteRenderer>();
      HighCalRender = Aimbot.GetComponent<SpriteRenderer>();
      EDrinkRender = Aimbot.GetComponent<SpriteRenderer>();
      SerumRender = Aimbot.GetComponent<SpriteRenderer>();
      BodyArmourRender = Aimbot.GetComponent<SpriteRenderer>();
      
      LowCalRender = Aimbot.GetComponent<SpriteRenderer>();
      CloverRender = Aimbot.GetComponent<SpriteRenderer>();
      
   }

   public void NextCharacter()
   {
         switch(CharacterInt)
         {
            case 1:
               AimbotRender.enabled = false;
               Aimbot.transform.position = OffScreen;
               Mag.transform.position = CharacterPosition;
               MagRender.enabled = true;
               CharacterInt++;
               
               break;
            case 2:
               MagRender.enabled = false;
               Mag.transform.position = OffScreen;
               Gas.transform.position = CharacterPosition;
               GasRender.enabled = true;
               CharacterInt++;
               
               break;
            case 3:
               GasRender.enabled = false;
               Gas.transform.position = OffScreen;
               RBoots.transform.position = CharacterPosition;
               RBootsRender.enabled = true;
               CharacterInt++;
               
               break;
            case 4:
               RBootsRender.enabled = false;
               RBoots.transform.position = OffScreen;
               HighCal.transform.position = CharacterPosition;
               HighCalRender.enabled = true;
               CharacterInt++;
               
               break;
            case 5:
               HighCalRender.enabled = false;
               HighCal.transform.position = OffScreen;
               EDrink.transform.position = CharacterPosition;
               EDrinkRender.enabled = true;
               CharacterInt++;
               
               break;
            case 6:
               EDrinkRender.enabled = false;
               EDrink.transform.position = OffScreen;
               Serum.transform.position = CharacterPosition;
               SerumRender.enabled = true;
               CharacterInt++;
               
               break;
            case 7:
               SerumRender.enabled = false;
               Serum.transform.position = OffScreen;
               BodyArmour.transform.position = CharacterPosition;
               BodyArmourRender.enabled = true;
               CharacterInt++;
               
               break;
            case 8:
               BodyArmourRender.enabled = false;
               BodyArmour.transform.position = OffScreen;
               LowCal.transform.position = CharacterPosition;
               LowCalRender.enabled = true;
               CharacterInt++;
               
               break;
            case 9:
               LowCalRender.enabled = false;
               LowCal.transform.position = OffScreen;
               Clover.transform.position = CharacterPosition;
               CloverRender.enabled = true;
               CharacterInt++;
               
               break;
            case 10:
               CloverRender.enabled = false;
               Clover.transform.position = OffScreen;
               Aimbot.transform.position = CharacterPosition;
               AimbotRender.enabled = true;
               CharacterInt++;
               ResetInt();
               break;
            default:
               ResetInt();
               break;         

         }
   }

   public void PreviousCharacter()
   {
      switch(CharacterInt)
         {
            case 1:
               AimbotRender.enabled = false;
               Aimbot.transform.position = OffScreen;
               Clover.transform.position = CharacterPosition;
               CloverRender.enabled = true;
               CharacterInt--;
               ResetInt();
               break;
            case 2:
               MagRender.enabled = false;
               Mag.transform.position = OffScreen;
               Aimbot.transform.position = CharacterPosition;
               AimbotRender.enabled = true;
               CharacterInt--;
               break;
            case 3:
               GasRender.enabled = false;
               Gas.transform.position = OffScreen;
               Mag.transform.position = CharacterPosition;
               MagRender.enabled = true;
               CharacterInt--;
               
               break;
            case 4:
               RBootsRender.enabled = false;
               RBoots.transform.position = OffScreen;
               Gas.transform.position = CharacterPosition;
               GasRender.enabled = true;
               CharacterInt--;
               
               break;
            case 5:
               HighCalRender.enabled = false;
               HighCal.transform.position = OffScreen;
               RBoots.transform.position = CharacterPosition;
               RBootsRender.enabled = true;
               CharacterInt--;
               
               break;
            case 6:
               EDrinkRender.enabled = false;
               EDrink.transform.position = OffScreen;
               HighCal.transform.position = CharacterPosition;
               HighCalRender.enabled = true;
               CharacterInt--;
               
               break;
            case 7:
               SerumRender.enabled = false;
               Serum.transform.position = OffScreen;
               EDrink.transform.position = CharacterPosition;
               EDrinkRender.enabled = true;
               CharacterInt--;
               
               break;
            case 8:
               BodyArmourRender.enabled = false;
               BodyArmour.transform.position = OffScreen;
               Serum.transform.position = CharacterPosition;
               SerumRender.enabled = true;
               CharacterInt--;
               
               break;
            case 9:
               LowCalRender.enabled = false;
               LowCal.transform.position = OffScreen;
               BodyArmour.transform.position = CharacterPosition;
               BodyArmourRender.enabled = true;
               CharacterInt--;
               
               break;
            case 10:
               CloverRender.enabled = false;
               Clover.transform.position = OffScreen;
               LowCal.transform.position = CharacterPosition;
               LowCalRender.enabled = true;
               CharacterInt--;
        
               break;
            default:
               ResetInt();
               break;         

         }
   }

   private void ResetInt()
   {
      if(CharacterInt >= 10 )
      {
         CharacterInt = 1;
      }
      else
      {
         CharacterInt = 10;
      }
   }


}
