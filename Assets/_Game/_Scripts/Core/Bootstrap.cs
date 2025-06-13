using Config;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private DisplayConfig _displayConfig;
        private void Awake()
        {
            _displayConfig.Initialize();
            _gameManager.Initialize();
            _scoreCounter.Initialize();
        }
    }
}
