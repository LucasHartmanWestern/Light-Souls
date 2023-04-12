using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public int lvlNum;

    void ChangeLevel()
    {
        Transform lvlTransform = GameObject.Find("Levels").transform;

        for (int i = 0; i < lvlTransform.childCount; i++)
        {
            // get the child game object at index i
            GameObject childObject = lvlTransform.GetChild(i).gameObject;

            if (lvlNum == i) childObject.SetActive(true);
            else childObject.SetActive(false);
        }
    }
}
