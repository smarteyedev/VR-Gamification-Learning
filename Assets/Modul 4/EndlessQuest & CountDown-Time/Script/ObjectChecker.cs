using Tproject.Quest;
using UnityEngine;
using Module4;

public class ObjectChecker : MonoBehaviour {
    public Module4.QuestController questController;
    public ObjectValidatorManager objectValidatorManager;
    public ObjectValidator[] objectValidators;


    public void ValidateSockets() {
        bool allSocketsOccupied = true; 
        foreach (ObjectValidator validator in objectValidators) {
            if (!validator.socketOccupied)
            {
                allSocketsOccupied = false; 
                break; 
            }
        }

        // If all sockets are occupied, validate the items
        if (allSocketsOccupied) {
            if (objectValidatorManager.AreAllItemsValid(objectValidators)) {
                //add score here
                //reset and finish quest here
                Debug.Log("All socketed items are valid.");
                FinishAllTask();
            } else {
                Debug.LogWarning("Some socketed items are invalid.");
                //reset quest here and finish
            }
        } else {
            Debug.LogWarning("Not all sockets are occupied.");
        }
    }

    private void FinishAllTask() {
        questController.FinishItem(0);
        questController.FinishItem(1);
        questController.FinishItem(2);
        questController.toDoList.Clear();
        objectValidatorManager.ClearSelectedItemTypes();
    }
}
