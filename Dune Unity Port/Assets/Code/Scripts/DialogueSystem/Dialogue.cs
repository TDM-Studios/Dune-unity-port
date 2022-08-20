using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public enum Autor
{
    Paul,
    Chani,
    Stilgar
}

[Serializable]
public class Line
{
    [SerializeField]
    private Autor autor;
    [SerializeField, TextArea(3, 5)]
    public string text;
    public string Name() { return autor.ToString(); }

    public int IDImage() { return ((int)autor); }
}

public class Dialogue : MonoBehaviour
{

    private bool active;
    private int line;
    public float waitTime = 0.05f;
    //[SerializeField, TextArea(3, 5)] private string[] speech;
    
    [SerializeField] List<Line> speech;

    [SerializeField] private DialogueManager dialogueManager;
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        active = false;

    }
    
    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                active = true;
                StartDialogue();
            }
        }
        else
        {
            if (dialogueManager.GetText() == speech[line].text)
            {
                if (Input.GetButtonDown("Fire1"))
                    NextDialogue();
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                StopCoroutine(ShowLine());
                dialogueManager.SetText(speech[line].text,speech[line].Name());
            }
        }
    }

    private void StartDialogue()
    {
        dialogueManager.TextOn();

        line = 0;

        //dialogueManager.SetText(speech[line].text, speech[line].Name());

        StartCoroutine(ShowLine());
    }

    private void NextDialogue()
    {
        line++;

        if (line == speech.Count)
        {
            active = false;
            dialogueManager.TextOff();
        }
        else
        {

            StartCoroutine(ShowLine());
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueManager.SetText(string.Empty,string.Empty);
        dialogueManager.SetOnlyAutor(speech[line].Name());
        dialogueManager.SetImage(speech[line].IDImage());


        foreach (char ch in speech[line].text)
        {
            string text = dialogueManager.GetText();

            text += ch;

            dialogueManager.SetOnlyText(text);
            yield return new WaitForSeconds(waitTime);
        }

    }

}
