using TMPro;
using UnityEngine;

namespace ModulKelima
{
    public class PlayerPrefsManager : MonoBehaviour
    {
        public string scoreKey = "playerScore";
        public TextMeshProUGUI textHighScore;
        public ScoreManager scoreManager;

        private void Start()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            textHighScore.text = LoadScore().ToString();
        }

        public void ResetScore()
        {
            PlayerPrefs.SetInt(scoreKey, 0);
            PlayerPrefs.Save();

            UpdateUI();
        }

        public void SaveScore()
        {
            if (scoreManager.score > LoadScore())
            {
                PlayerPrefs.SetInt(scoreKey, scoreManager.score);
                PlayerPrefs.Save();

                UpdateUI();
            }
        }

        public int LoadScore()
        {
            int score = PlayerPrefs.GetInt(scoreKey, 0);
            return score;
        }
    }
}
