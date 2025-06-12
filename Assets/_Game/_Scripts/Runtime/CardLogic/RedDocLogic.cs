using Core;
using Runtime.CardLogic.CardUnits;
using UnityEngine;

namespace Runtime.CardLogic
{
    public class RedDocLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 20;
        protected override int Weight => BASE_WEIGHT;
    }
}
