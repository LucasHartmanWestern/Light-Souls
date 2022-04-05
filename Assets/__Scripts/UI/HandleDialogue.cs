using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDialogue : MonoBehaviour
{
    [Header("Dialogue game obejcts")]
    [SerializeField] private GameObject _gruntDialogue;
    [SerializeField] private GameObject _boss1Dialogue;
    [SerializeField] private GameObject _boss2Dialogue;
    [SerializeField] private GameObject _bossBetrayDialogue;

    void Update()
    {
        // Allow player to disable dialogue after killing mech boss
        if (_boss1Dialogue.activeSelf && Input.GetKeyDown(KeyCode.E))
            _boss1Dialogue.SetActive(false);
    }

    #region Public methods to toggle the dialogue
    public void GruntDialogue()
    {
        _gruntDialogue.SetActive(!_gruntDialogue.activeSelf);
    }
    public void Boss1Dialogue()
    {
        _boss1Dialogue.SetActive(true);
    }
    public void Boss2Dialogue()
    {
        _boss2Dialogue.SetActive(!_boss2Dialogue.activeSelf);
    }
    public void BossBetrayDialogue()
    {
        if (!_bossBetrayDialogue.activeSelf)
            _bossBetrayDialogue.SetActive(true);
        else
        {
            _bossBetrayDialogue.SetActive(false);
            EnemyGeneral[] enemyScripts = GameObject.Find("Enemies").GetComponentsInChildren<EnemyGeneral>();

            foreach (EnemyGeneral script in enemyScripts)
                script.isHostile = true;
        }
    }
    #endregion

}
