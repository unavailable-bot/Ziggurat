using Runtime.CardLogic;
using Runtime.CardLogic.CardUnits;
using UnityEngine;

namespace Runtime.Battlefield
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private int _x, _y;
        [SerializeField] private GameObject _currentUnit;
        
        internal bool IsEmpty => _currentUnit == null;
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

        internal void PlaceCard(GameObject card)
        {
            if(!IsEmpty) return;
            
            _currentUnit = card;
            Vector3 unitPosition = new(this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z);
            Instantiate(_currentUnit, unitPosition, Quaternion.identity);
        }

        internal void Clear()
        {
            _currentUnit = null;
        }
    }
}
