using Core;
using UnityEngine;

namespace Runtime.CardLogic
{
    public class RedSCLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 10;
        internal override int Weight => BASE_WEIGHT;
        
        protected override void OnCardPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
        }
    }
}
