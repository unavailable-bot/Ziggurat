using UnityEngine;
using System.Text.RegularExpressions;
using Core;

namespace Runtime.CardLogic
{
    public class RedNumberCard : DraggableCard
    {
        private int _weight;

        internal override int Weight => _weight;

        private void Awake()
        {
            if (_weight != 0) return;
            
            var match = Regex.Match(gameObject.name, @"\d+");
            if (match.Success)
            {
                _weight = int.Parse(match.Value);
                Debug.Log($"CardName: {gameObject.name}, Weight : {_weight}");
            }
            else
            {
                Debug.LogWarning($"[NumberCardLogic] Не удалось найти число в имени: {gameObject.name}");
            }
        }

        protected override void OnCardPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
        }
    }
}
