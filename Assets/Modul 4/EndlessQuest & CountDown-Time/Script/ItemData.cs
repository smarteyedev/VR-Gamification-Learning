using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {
    public ItemType itemData;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Start() {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation; 
    }

    public void ResetPosition() {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation; 
    }
}
