using Core;
using Runtime.Battlefield;
using UnityEngine;
using DG.Tweening;
using Runtime.CardLogic.CardUnits;

namespace Runtime.CardLogic
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public abstract class DraggableCard : MonoBehaviour
    {
        private Tween _scaleTween;
        
        [SerializeField] private LayerMask _callLayerMask;
        [SerializeField] private GameObject _unit;
        
        private Camera _camera;
        private Vector3 _startPosition;
        private bool _isDragging;
        private bool _inside;

        protected abstract int Weight {get;}
        protected int Unit {get;}
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_isDragging && Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = GetMouseWorldPosition();
                var hit = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask($"layCard"));
                if (hit && hit.transform == this.transform)
                {
                    StartDrag();
                }
            }

            if (_isDragging)
            {
                Vector3 mousePosition = GetMouseWorldPosition();
                Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
                transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * 20f);
                if (Input.GetMouseButtonUp(0))
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    EndDrag();
                    
            }
        }

        private void OnCardPlaced()
        {
            ScoreCounter.I.SetNewScore(Weight);
            Destroy(this.gameObject);
        }

        private void StartDrag()
        {
            _isDragging = true;
            _startPosition = this.transform.position;
        }

        private void EndDrag()
        {
            _isDragging = false;
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, _callLayerMask);
            if (hit)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                var currentCell  = hit.GetComponent<Cell>();
                if (currentCell && currentCell.IsEmpty && !currentCell.IsNoTarget) 
                {
                    currentCell.PlaceCard(_unit);
                    OnCardPlaced();
                    return;
                }
            }
            transform.DOScale(1f, 0.15f); // вернём к нормальному масштабу
            transform.position = _startPosition;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = _camera.transform.position.z;
            return _camera.ScreenToWorldPoint(mousePosition);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_inside && _isDragging)
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, _callLayerMask);
                if (hit)
                {
                    transform.DOScale(0.5f, 0.15f);
                    _inside = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "BattleField" && _isDragging)
            {
                transform.DOScale(1f, 0.15f); // вернём к нормальному масштабу
                _inside = false;
            }
        }
        
        private void OnDestroy()
        {
            transform.DOKill(); // убьёт все анимации, связанные с этим объектом
        }
    }
}
