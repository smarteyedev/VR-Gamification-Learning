using System.Collections.Generic;
using UnityEngine;
using Seville;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectValidator : MonoBehaviour {
    private SESocketInteractor _socketInteractor;
    public ObjectChecker _objChecker;

    private ItemType itemSocketed = ItemType.None;
    public bool socketOccupied = false;

    private void Start() {
        _socketInteractor = GetComponent<SESocketInteractor>();
    }

    public void CheckItem() {
        if (_socketInteractor.hasSelection) {
            IXRSelectInteractable interactable = _socketInteractor.selectTarget;
            if (interactable != null) {
                itemSocketed = interactable.transform.GetComponent<ItemData>().itemData;
                socketOccupied = true;
                _objChecker.ValidateSockets();
            }
        } else {
            itemSocketed = ItemType.None;
            socketOccupied = false;
        }
    }

    public ItemType GetItemSocketed() {
        return itemSocketed;
    }

    public bool IsSocketOccupied() {
        return socketOccupied;
    }
}
