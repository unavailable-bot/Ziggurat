using TMPro;
using UnityEngine;

namespace Core
{
    public class ScoreCounter : MonoBehaviour
    {
        internal static ScoreCounter I{get; private set;}
        private int score;
        private TMP_Text scoreText;

        internal void Initialize()
        {
            I = this;
            
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
