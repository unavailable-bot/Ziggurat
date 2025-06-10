using Runtime.DeckLogic;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _deckPrefab;
        
        private enum FirstPlayer
        {
            Red,
            Blue
        }

        private void Awake()
        {
            SetFirstPlayer();
        }

        private void SetFirstPlayer()
        {
            var firstPlayerColor = (FirstPlayer)Random.Range(0, 2);
            
            var firstPlayer = Instantiate(_deckPrefab, new Vector3(0, -12f, 0), _deckPrefab.transform.rotation);
            firstPlayer.GetComponent<PlayerDeck>().Init((PlayerColor)firstPlayerColor);
                
            var secondPlayer = Instantiate(_deckPrefab, new Vector3(-2, 12f, 0), _deckPrefab.transform.rotation);
            secondPlayer.GetComponent<PlayerDeck>().Init(firstPlayerColor == FirstPlayer.Red ? PlayerColor.Blue : PlayerColor.Red);
        }
    }
}
