using TMPro;
using UnityEngine;

namespace ModulKedua
{
    public class ScoreManager : MonoBehaviour
    {
        public int score;
        public TextMeshProUGUI scoreText;

        private void Start()
        {
            score = 0;
            scoreText.text = score.ToString();
        }

        public void UpdateScore(int value)
        {
            score += value;
            UpdateTextUI();
        }

        public void UpdateTextUI()
        {
            scoreText.text = score.ToString();
        }
    }
}
