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
   public int CharacterInt = 1;
   public static int refInt;
   private SpriteRenderer GunnerRender, SnipsRender, BookRender;
   private readonly string selectChar = "SelectedCharacter";

   private void Awake()
   {
      CharacterPosition = Gunner.transform.position;
      OffScreen = Snips.transform.position;
      GunnerRender = Gunner.GetComponent<SpriteRenderer>();
      SnipsRender = Gunner.GetComponent<SpriteRenderer>();
      BookRender = Gunner.GetComponent<SpriteRenderer>();

      refInt = 1; // Set the refInt based on the newly selected CharacterInt

    }

    public void NextCharacter()
   {
        #region Cycle through character ints in the upwards direction
        switch (CharacterInt)
         {
            case 1:
               GunnerRender.enabled = false;
               Gunner.transform.position = OffScreen;
               Snips.transform.position = CharacterPosition;
               SnipsRender.enabled = true;
               CharacterInt = 2;
               break;
            case 2:
               SnipsRender.enabled = false;
               Snips.transform.position = OffScreen;
               Book.transform.position = CharacterPosition;
               BookRender.enabled = true;
               CharacterInt = 3;
               break;
            case 3:
               BookRender.enabled = false;
               Book.transform.position = OffScreen;
               Gunner.transform.position = CharacterPosition;
               GunnerRender.enabled = true;
               CharacterInt = 1;
               break;      
         }
        #endregion

        refInt = CharacterInt; // Set the refInt based on the newly selected CharacterInt
   }

   public void PreviousCharacter()
   {
        #region Cycle through character ints in the downwards direction
        switch (CharacterInt)
         {
            case 1:
               GunnerRender.enabled = false;
               Gunner.transform.position = OffScreen;
               Book.transform.position = CharacterPosition;
               BookRender.enabled = true;
               CharacterInt = 3;
               break;
            case 2:
               SnipsRender.enabled = false;
               Snips.transform.position = OffScreen;
               Gunner.transform.position = CharacterPosition;
               GunnerRender.enabled = true;
               CharacterInt = 1;
               break;
            case 3:
               BookRender.enabled = false;
               Book.transform.position = OffScreen;
               Snips.transform.position = CharacterPosition;
               SnipsRender.enabled = true;
               CharacterInt = 2;
               break;
         }
        #endregion

        refInt = CharacterInt; // Set the refInt based on the newly selected CharacterInt
    }

    public void startGame()
   {
        PlayerPrefs.SetInt(selectChar, CharacterInt); // Set the int
        SceneManager.LoadScene("Level 1");
   }
   public void instructions()
   {
      SceneManager.LoadScene("HowToPlay");
   }
   public void multinstructions()
   {
      SceneManager.LoadScene("multihowtoplay");
   }
   public void startdesert()
   {
         PlayerPrefs.SetInt(selectChar, CharacterInt); // Set the int
         SceneManager.LoadScene("DessertMulti");
   }
   public void starttavern()
   {
         PlayerPrefs.SetInt(selectChar, CharacterInt); // Set the int
         SceneManager.LoadScene("TavernMulti");
   }
   public void startvolcano()
   {
         PlayerPrefs.SetInt(selectChar, CharacterInt); // Set the int
         SceneManager.LoadScene("VolcanoMulti");
   }
   public void startjungle()
   {
         PlayerPrefs.SetInt(selectChar, CharacterInt); // Set the int
         SceneManager.LoadScene("JungleMulti");
   }
   
}
