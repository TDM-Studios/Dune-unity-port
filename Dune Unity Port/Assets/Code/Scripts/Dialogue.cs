using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private bool active;
    private int line;
    public float waitTime = 0.05f;
    [SerializeField, TextArea(3, 5)] private string[] dialogueLine;

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
            if (dialogueManager.GetText() == dialogueLine[line])
            { 
                if (Input.GetButtonDown("Fire1"))
                NextDialogue();
            }else if (Input.GetButtonDown("Fire1"))
            {
                StopCoroutine(ShowLine());
                dialogueManager.SetText(dialogueLine[line]);
            }
        }
    }

    private void StartDialogue()
    {
        dialogueManager.TextOn();

        line = 0;

        

        dialogueManager.SetText(dialogueLine[line]);

        StartCoroutine(ShowLine());
    }

    private void NextDialogue()
    {
        line++;

        if (line == dialogueLine.Length)
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
        dialogueManager.SetText(string.Empty);

        foreach (char ch in dialogueLine[line])
        {
            string text = dialogueManager.GetText();

            text += ch;

            dialogueManager.SetText(text);
            yield return new WaitForSeconds(waitTime);
        }

    }

}
