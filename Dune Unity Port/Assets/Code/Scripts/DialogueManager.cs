using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject dialogueText;

    private bool OnOff;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.SetActive(false);
        OnOff = false;
    }

    
    public string GetText()
    {
        return dialogueText.GetComponent<TMP_Text>().text;
    }

    public void SetText(string text)
    {
        dialogueText.GetComponent<TMP_Text>().text = text;
    }

    public void TextOn()
    {
        if (!OnOff)
        {
            OnOff = true;
            dialogueBox.SetActive(true);
            dialogueText.SetActive(true);
        }
    }

    public void TextOff()
    {
        if (OnOff)
        {
            OnOff = false;
            dialogueBox.SetActive(false);
            dialogueText.SetActive(false);
        }
    }
}
