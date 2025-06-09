using Core;
using UnityEngine;

namespace Runtime.CardLogic
{
    public class RedMonsterLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 30;
        internal override int Weight => BASE_WEIGHT;
        
        protected override void OnCardPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
        }
    }
}
