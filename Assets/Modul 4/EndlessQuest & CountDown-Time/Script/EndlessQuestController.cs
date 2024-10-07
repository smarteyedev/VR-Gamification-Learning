using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module4;

public class EndlessQuestController : MonoBehaviour {
    [Header("Quest Dependency")]
    [SerializeField] private QuestController _questController;
    [SerializeField] private ObjectValidatorManager _objValManager;

    [Header("Quests List")]
    [SerializeField] private List<QuestController.ItemFormat> availableQuests = new List<QuestController.ItemFormat>();

    public void GenerateQuest() {
        if (_questController != null && availableQuests.Count > 0) {
            List<QuestController.ItemFormat> randomQuests = GetRandomQuests(3);
            foreach (var quest in randomQuests) {
                _questController.toDoList.Add(quest);
                _objValManager.SetItemType(quest.requiredItem);
            }
        }
    }

    private List<QuestController.ItemFormat> GetRandomQuests(int count) {
        List<QuestController.ItemFormat> randomizedQuests = new List<QuestController.ItemFormat>(availableQuests);

        // Shuffle the list
        for (int i = 0; i < randomizedQuests.Count; i++) {
            QuestController.ItemFormat temp = randomizedQuests[i];
            int randomIndex = Random.Range(i, randomizedQuests.Count);
            randomizedQuests[i] = randomizedQuests[randomIndex];
            randomizedQuests[randomIndex] = temp;
        }

        return randomizedQuests.GetRange(0, Mathf.Min(count, randomizedQuests.Count));
    }
}
