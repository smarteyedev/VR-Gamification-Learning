using TMPro;
using UnityEngine;

namespace ModulKelima
{
    public class ScoreManager : MonoBehaviour
    {
        public int score;
        public TextMeshProUGUI scoreText;
        public GameObject panelScore;

        private void Start()
        {
            score = 0;
            scoreText.text = score.ToString();
        }

        public void StartQuest()
        {
            score = 0;
            panelScore.SetActive(false);
        }

        public void ShowResult()
        {
            panelScore.SetActive(true);
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
