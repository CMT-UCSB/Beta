using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();

    public TextMeshProUGUI dialogueText;

    public Button attack;
    public Button defend;
    public Button items;
    public Button flee;

    private void Start()
    {
        //sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with... " + dialogue.name);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(sentences.Count == 10)
        {
            attack.interactable = true;
        }

        if(sentences.Count == 8)
        {
            defend.interactable = true;
        }

        if(sentences.Count == 7)
        {
            items.interactable = true;
        }

        if(sentences.Count == 6)
        {
            flee.interactable = true;
        }

        string s = sentences.Dequeue();
        dialogueText.text = s;
    }

    void EndDialogue()
    {
        SceneManager.LoadScene("Level 1"); 
    }
}
