using UnityEngine;

namespace Runtime.CardLogic.CardUnits
{
    public class LordUnit : CardUnit
    {
        private const int BASE_WEIGHT = 10;
        protected override int Weight => BASE_WEIGHT;
    }
}
