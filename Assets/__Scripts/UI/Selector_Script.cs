using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector_Script : MonoBehaviour
{
   public GameObject Gunner;
   public GameObject Snips;
   public GameObject Book;
   private Vector3 CharacterPosition;
   private Vector3 OffScreen;
   private int CharacterInt = 1;
   private SpriteRenderer GunnerRender, SnipsRender, BookRender;

   private void Awake()
   {
      CharacterPosition = Gunner.transform.position;
      OffScreen = Snips.transform.position;
      GunnerRender = Gunner.GetComponent<SpriteRenderer>();
      SnipsRender = Gunner.GetComponent<SpriteRenderer>();
      BookRender = Gunner.GetComponent<SpriteRenderer>();
   }

   public void NextCharacter()
   {
         switch(CharacterInt)
         {
            case 1:
               GunnerRender.enabled = false;
               Gunner.transform.position = OffScreen;
               Snips.transform.position = CharacterPosition;
               SnipsRender.enabled = true;
               CharacterInt++;
               break;
            case 2:
               SnipsRender.enabled = false;
               Snips.transform.position = OffScreen;
               Book.transform.position = CharacterPosition;
               BookRender.enabled = true;
               CharacterInt++;
               break;
            case 3:
               BookRender.enabled = false;
               Book.transform.position = OffScreen;
               Gunner.transform.position = CharacterPosition;
               GunnerRender.enabled = true;
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
               GunnerRender.enabled = false;
               Gunner.transform.position = OffScreen;
               Book.transform.position = CharacterPosition;
               BookRender.enabled = true;
               CharacterInt--;
               ResetInt();
               break;
            case 2:
               SnipsRender.enabled = false;
               Snips.transform.position = OffScreen;
               Gunner.transform.position = CharacterPosition;
               GunnerRender.enabled = true;
               CharacterInt--;
               break;
            case 3:
               BookRender.enabled = false;
               Book.transform.position = OffScreen;
               Snips.transform.position = CharacterPosition;
               SnipsRender.enabled = true;
               CharacterInt--;
               break;
            default:
               ResetInt();
               break;         

         }
   }

   private void ResetInt()
   {
      if(CharacterInt >= 3 )
      {
         CharacterInt = 1;
      }
      else
      {
         CharacterInt = 3;
      }
   }

   public void startGame()
   {
      SceneManager.LoadScene("Level 1");
   }

}
