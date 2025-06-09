using Core;

namespace Runtime.CardLogic
{
    public class RedDocLogic : DraggableCard
    {
        private const int BASE_WEIGHT = 20;
        internal override int Weight => BASE_WEIGHT;

        protected override void OnCardPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
        }
    }
}
