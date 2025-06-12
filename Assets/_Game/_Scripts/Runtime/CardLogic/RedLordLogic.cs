using Core;
using Runtime.CardLogic.CardUnits;
using UnityEngine;

namespace Runtime.CardLogic
{
    public class RedLordLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 50;
        protected override int Weight => BASE_WEIGHT;
    }
}
