using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private List<string> names;

    [SerializeField] private Sprite[] imageAutor;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private GameObject dialogueAutor;
    [SerializeField] private GameObject dialogueText;

    private bool OnOff;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.SetActive(false);
        dialogueAutor.SetActive(false);
        OnOff = false;
    }

    public string GetText()
    {
        return dialogueText.GetComponent<TMP_Text>().text;
    }

    public void SetImage(int image) {
        dialogueImage.GetComponent<Image>().sprite = imageAutor[image];
    }
    public void SetText(string text,string autor)
    {
        dialogueText.GetComponent<TMP_Text>().text = text;
        dialogueAutor.GetComponent<TMP_Text>().text = autor;

    }
    public void SetOnlyText(string text)
    {
        dialogueText.GetComponent<TMP_Text>().text = text;

    }
    public void SetOnlyAutor(string autor)
    {
        dialogueAutor.GetComponent<TMP_Text>().text = autor;
    }

    public void SetOnlyIDAutor(int autor)
    {
        dialogueAutor.GetComponent<TMP_Text>().text = names[autor];
    }
    public void TextOn()
    {
        if (!OnOff)
        {
            OnOff = true;
            dialogueBox.SetActive(true);
            dialogueText.SetActive(true);
            dialogueAutor.SetActive(true);
        }
    }

    public void TextOff()
    {
        if (OnOff)
        {
            OnOff = false;
            dialogueBox.SetActive(false);
            dialogueText.SetActive(false);
            dialogueAutor.SetActive(false);
        }
    }



}
