using Core;
using Runtime.Battlefield;
using UnityEngine;
using DG.Tweening;

namespace Runtime.CardLogic
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public abstract class DraggableCard : MonoBehaviour
    {
        private Tween _scaleTween;
        
        [SerializeField] private GameObject _unit;
        private LayerMask _callLayerMask;
        
        private Camera _camera;
        private Vector3 _startPosition;
        private bool _isDragging;
        private bool _inside;

        protected abstract int Weight {get;}
        
        private void Start()
        {
            _camera = Camera.main;
            _callLayerMask = LayerMask.GetMask("layCell");
        }
        
        private void Update()
        {
            if (!_isDragging && Input.GetMouseButtonDown(0))
            {
                HandleStartDrag();
            }

            if (!_isDragging) return;
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            HandleDragging();
        }

        private void HandleStartDrag()
        {
            var startMousePosition = GetMouseWorldPosition();
            var hit = Physics2D.OverlapPoint(startMousePosition, LayerMask.GetMask($"layCard"));
            if (hit && hit.transform == this.transform)
            {
                StartDrag();
            }
        }

        private void HandleDragging()
        {
            var dragMousePosition = GetMouseWorldPosition();
            var targetPosition = new Vector3(dragMousePosition.x, dragMousePosition.y, this.transform.position.z);
            transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * 20f);
            if (Input.GetMouseButtonUp(0))
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                EndDrag();
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
            var mouseWorldPosition = GetMouseWorldPosition();
            var hit = Physics2D.OverlapPoint(mouseWorldPosition, _callLayerMask);
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
            transform.DOScale(1f, 0.15f);
            transform.position = _startPosition;
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = _camera.transform.position.z;
            return _camera.ScreenToWorldPoint(mousePosition);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_inside || !_isDragging) return;
            
            var mouseWorldPosition = GetMouseWorldPosition();
            var hit = Physics2D.OverlapPoint(mouseWorldPosition, _callLayerMask);
                
            if (!hit) return;
                
            transform.DOScale(0.5f, 0.15f);
            _inside = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name != "BattleField" || !_isDragging) return;
            
            transform.DOScale(1f, 0.15f);
            _inside = false;
        }
        
        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}
