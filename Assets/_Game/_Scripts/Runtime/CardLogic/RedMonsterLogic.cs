using Core;
using Runtime.CardLogic.CardUnits;
using UnityEngine;

namespace Runtime.CardLogic
{
    public class RedMonsterLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 30;
        protected override int Weight => BASE_WEIGHT;
    }
}
