using Core;
using UnityEngine;

namespace Runtime.CardLogic.CardUnits
{
    public abstract class CardUnit : MonoBehaviour
    {
        protected abstract int Weight {get;}

        internal void OnUnitPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
        }
    }
}
