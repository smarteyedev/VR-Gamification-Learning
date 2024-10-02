using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class StandArea : MonoBehaviour
{
    public UnityEvent onTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            //AnimationCharacter _animator = other.GetComponent<AnimationCharacter>();

            //_animator.StartIdle();

            if(onTriggerEvent != null)
            {
                onTriggerEvent.Invoke();
            }
        }
    }
}
