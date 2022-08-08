using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questContent;
    [SerializeField] private GameObject questHolder;

    public List<Quest> currentQuests;

    private void Awake()
    {
        foreach (var quest in currentQuests)
        {
            quest.Initialize();
            GameObject questObj = Instantiate(questPrefab, questContent);

            questObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                questHolder.SetActive(true);
            });
        }
    }

    private void OnQuestCompleted(Quest quest)
    {
        //TODO: What happens when a quest is completed?
    }
}
