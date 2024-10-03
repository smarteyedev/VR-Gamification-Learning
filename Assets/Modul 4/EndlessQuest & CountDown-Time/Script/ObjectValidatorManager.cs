using System.Collections.Generic;
using UnityEngine;

public class ObjectValidatorManager : MonoBehaviour {
    public List<ItemType> selectedItemTypes = new List<ItemType>();

    public void SetItemType(ItemType itemType) {
        Debug.Log(itemType);
        selectedItemTypes.Add(itemType);
    }

    public List<ItemType> GetSelectedItemTypes() {
        return selectedItemTypes;
    }

    public void ClearSelectedItemTypes() {
        selectedItemTypes.Clear();
    }

    public bool IsValidItem(ItemType itemType) {
        return selectedItemTypes.Contains(itemType);
    }

    public bool AreAllItemsValid(ObjectValidator[] objectValidators) {
        List<ItemType> itemTypes = new List<ItemType>();

        foreach (var validator in objectValidators) {
            // Ensure the item is checked first before validating
            ItemType itemType = validator.GetItemSocketed();
            if (itemType != ItemType.None) {
                itemTypes.Add(itemType);
            } else {
                itemTypes.Add(ItemType.None);
            }
        }

        foreach (ItemType itemType in itemTypes) {
            if (!IsValidItem(itemType) && itemType != ItemType.None) {
                return false;
            }
        }

        return true;
    }
}


public enum ItemType {
    Mouse, Keyboard, Mousepad,  Monitor, Pc, Laptop, None
}
