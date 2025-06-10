using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.BankLogic
{
    internal enum BankColor
    {
        Red,
        Blue
    }
    public class Bank : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField]private Sprite _red;
        [SerializeField]private Sprite _blue;
        private SpriteRenderer _cardBack;
        
        [Header("CardList")]
        [SerializeField]private List<GameObject> _cardList = new();
        private Queue<GameObject> _deckQueue;

        internal BankColor BankColor { get; private set; }

        internal void Init(BankColor color)
        {
            BankColor = color;
            
            _cardBack = gameObject.GetComponentInChildren<SpriteRenderer>();
            _cardBack.sprite = BankColor == BankColor.Red ? _red : _blue;
            
            LoadAllCards();
            ShuffleDeck();
            Resources.UnloadUnusedAssets(); // сброс неиспользуемых объектов
            // можно добавить Setup: UI, камера и т.д.
        }
        
        private void LoadAllCards()
        {
            var loadedCards = Resources.LoadAll<GameObject>($"CardPrefabs");
            
            foreach (var card in loadedCards)
            {
                _cardList.Add(card);
                _cardList.Add(card);
            }

            Debug.Log($"Загружено карт: {loadedCards.Length}, итоговая колода: {_cardList.Count}");
        }
        
        private void ShuffleDeck()
        {
            _deckQueue = new Queue<GameObject>(_cardList.OrderBy(x => Random.value));
        }
        
        internal GameObject DrawCard()
        {
            if (_deckQueue.Count > 0) return _deckQueue.Dequeue();
            
            Debug.LogWarning("Колода пуста!");
            return null;
        }
    }
}
