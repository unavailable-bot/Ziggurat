using UnityEngine;
using System.Collections.Generic;
using Core;

namespace Runtime.DeckLogic
{
    internal enum PlayerColor
    {
        Red,
        Blue
    }
    public class PlayerDeck : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cards = new();
        private readonly float[] cardXCoo = { -8, -4, 0, 4, 8, 12 };
        private int maxCard;
        
        private PlayerColor PlayerColor { get; set; }
        private bool isFirstPlayer;
        
        internal void Init(PlayerColor color, int playerNumber)
        {
            PlayerColor = color;
            isFirstPlayer = playerNumber == 1;
            
            maxCard = isFirstPlayer ? 5 : 6;
            Debug.Log($"Color: {PlayerColor} | First player? {isFirstPlayer}");
            GetStartDeck(maxCard);
        }

        private void GetStartDeck(int cardCount)
        {
            var myBank = PlayerColor == PlayerColor.Red ? GameManager.I.RedBank : GameManager.I.BlueBank;
            
            for (int i = 0; i < cardCount; i++)
            {
                var cardPrefab = myBank.DrawCard();
                if (!cardPrefab)
                {
                    Debug.LogWarning("Колода пуста! Прекращаем раздачу стартовой руки.");
                    return;
                }
                var instance = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, this.transform);
                instance.transform.localPosition = new Vector3(cardXCoo[i], 0f, 0f);
                cards.Add(instance);
            }
        }
    }
}
