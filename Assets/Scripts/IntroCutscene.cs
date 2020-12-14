using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public GameObject sock;
    public GameObject hairTies;

    public Dialogue hairTiesDialogue;
    //public Dialogue sockDialogue;

    public Button attack;
    public Button defend;
    public Button items;
    public Button flee;

    public void Awake()
    {
        attack.interactable = false;
        defend.interactable = false;
        items.interactable = false;
        flee.interactable = false;

        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(hairTiesDialogue);
    }

}
