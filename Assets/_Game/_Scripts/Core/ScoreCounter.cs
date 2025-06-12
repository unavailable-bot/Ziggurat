using TMPro;
using UnityEngine;

namespace Core
{
    public class ScoreCounter : MonoBehaviour
    {
        internal static ScoreCounter I{get; private set;}
        private int score;
        private TMP_Text scoreText;

        private void Awake()
        {
            I = this;
        }

        private void Start()
        {
            scoreText = GetComponent<TMP_Text>();
            scoreText.text = score.ToString();
        }

        internal void SetNewScore(int newScore)
        {
            score += newScore;
            scoreText.text = score.ToString();
        }
    }
}
