using UnityEngine;
using System.Text.RegularExpressions;

namespace Runtime.CardLogic
{
    public class RedNumberCard : DraggableCard
    {
        private int _weight;

        protected override int Weight => _weight;

        private void Awake()
        {
            if (_weight != 0) return;
            
            var match = Regex.Match(gameObject.name, @"\d+");
            if (match.Success)
            {
                _weight = int.Parse(match.Value);
            }
            else
            {
                Debug.LogWarning($"[NumberCardLogic] Не удалось найти число в имени: {gameObject.name}");
            }
        }
    }
}
