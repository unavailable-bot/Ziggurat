using DG.Tweening;
using Runtime.BankLogic;
using Runtime.DeckLogic;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        internal static GameManager I { get; private set; }
        [SerializeField] private GameObject _deckPrefab;
        [SerializeField] private GameObject _bankPrefab;
        
        internal Bank RedBank { get; private set; }
        internal Bank BlueBank { get; private set; }
        
        private enum FirstPlayer
        {
            Red,
            Blue
        }

        private void Awake()
        {
            I = this;
            
            DOTween.Clear();
            
            var firstPlayerColor = (FirstPlayer)Random.Range(0, 2);
            
            SetBankColor(firstPlayerColor);
            SetFirstPlayer(firstPlayerColor);
        }
        
        private void SetFirstPlayer(FirstPlayer color)
        {
            int playerNumber = 1;
            var firstPlayer = Instantiate(_deckPrefab, new Vector3(0, -12f, 0), _deckPrefab.transform.rotation);
            firstPlayer.GetComponent<PlayerDeck>().Init((PlayerColor)color, playerNumber++);

            var secondPlayer = Instantiate(_deckPrefab, new Vector3(-2, 12f, 0), _deckPrefab.transform.rotation);
            secondPlayer.GetComponent<PlayerDeck>().Init(color == FirstPlayer.Red ? PlayerColor.Blue : PlayerColor.Red, playerNumber);
        }
        
        private void SetBankColor(FirstPlayer color)
        {
            var firstBank = Instantiate(_bankPrefab, new Vector3(15.5f, -5.5f, 0), _bankPrefab.transform.rotation);
            Bank firstBankComponent = firstBank.GetComponent<Bank>();
            firstBankComponent.Init((BankColor)color);
            
            var secondBank = Instantiate(_bankPrefab, new Vector3(15.5f, 5.5f, 0), _bankPrefab.transform.rotation);
            Bank secondBankComponent = secondBank.GetComponent<Bank>();
            secondBankComponent.Init(color == FirstPlayer.Red ? BankColor.Blue : BankColor.Red);

            RedBank = firstBankComponent.BankColor == (BankColor)color ? firstBankComponent : secondBankComponent;
            BlueBank = secondBankComponent.BankColor == (BankColor)color ? firstBankComponent : secondBankComponent;
        }
    }
}
