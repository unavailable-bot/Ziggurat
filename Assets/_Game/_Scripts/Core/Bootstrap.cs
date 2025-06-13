using Config;
using Runtime.Battlefield;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private DisplayConfig _displayConfig;
        [SerializeField] private BoardManager _borderManager;
        private void Awake()
        {
            _displayConfig.Initialize();
            _borderManager.Initialize();
            _gameManager.Initialize();
            _scoreCounter.Initialize();
        }
    }
}
