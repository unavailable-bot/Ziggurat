using Runtime.CardLogic;
using UnityEngine;

namespace Runtime.Battlefield
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private int _x, _y;
        [SerializeField] private DraggableCard _currentCard;
        
        internal bool IsEmpty => _currentCard == null;
        internal bool IsNoTarget { get; private set; }

        internal void SetCoordinates(int x, int y)
        {
            _x = x; _y = y;
        }

        internal void SetNoTargetCell()
        {
            if (CompareTag($"NoTarget"))
            {
                IsNoTarget = true;
            }
        }

        internal bool PlaceCard(DraggableCard card)
        {
            if(!IsEmpty) return false;
            _currentCard = card;
            card.transform.position = this.transform.position;
            return true;
        }

        internal void Clear()
        {
            _currentCard = null;
        }
    }
}
