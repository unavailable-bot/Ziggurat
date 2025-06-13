using UnityEngine;

namespace Runtime.CardLogic.CardUnits
{
    public class DocUnit : CardUnit
    {
        private const int BASE_WEIGHT = 15;
        protected override int Weight => BASE_WEIGHT;
    }
}
