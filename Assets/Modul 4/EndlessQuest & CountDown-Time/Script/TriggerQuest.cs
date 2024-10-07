using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuest : MonoBehaviour
{
    [SerializeField] private EndlessQuestController _endlessQuest;
    [SerializeField] private Module4.QuestController _questController;
    private GameObject _NpcGameobject;


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Npc")) {
            _NpcGameobject = other.gameObject;
            _endlessQuest.GenerateQuest();
            _questController.OpenQuest();
        }
    }

    public void DestroyNpc() {
        var gameobjectToDestroy = _NpcGameobject;
        Destroy(gameobjectToDestroy);
    }


}
