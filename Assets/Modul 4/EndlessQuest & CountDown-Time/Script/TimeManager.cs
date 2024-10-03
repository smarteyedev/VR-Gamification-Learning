using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Module4 {
    public class TimeManager : MonoBehaviour {
        [SerializeField] private float timer = 60;
        [SerializeField] private TextMeshProUGUI timerText;

        bool isTracking = false;

        private void Update() {
            if (isTracking) {
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    //do somthng
                    isTracking = false;
                }
            }
            timerText.text = timer.ToString("F2");
        }

        public void StartTimer() {
            isTracking = true;
        }
    }
}
