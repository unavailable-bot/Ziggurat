using DG.Tweening;
using UnityEngine;

namespace Runtime.BankLogic.VFX
{
    public class HoverVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hoverEffect;
        private ParticleSystem.EmissionModule _emission;
        private ParticleSystem.MainModule _mainModule;

        private Vector3 _baseScale;
        private Tween _scaleTween;

        private void Awake()
        {
            _baseScale = _hoverEffect.transform.localScale;
            
            _emission = _hoverEffect.emission;
            _emission.rateOverTime = 1;
            
            _mainModule = _hoverEffect.main;
            _mainModule.simulationSpeed = 0.5f;
            
            _hoverEffect.Play();
        }

        private void Start()
        {
            var colorOverLifetime = _hoverEffect.colorOverLifetime;
            Gradient gradient = new();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new(new Color(1f, 0f, 0f), 0f),   // Алый в начале
                    new(new Color(1f, 0f, 0f), 1f),   // Алый в конце
                },
                new GradientAlphaKey[]
                {
                    new(0f, 0f),   // Прозрачный в начале
                    new(1f, 0.5f), // Полностью видимый по центру
                    new(0f, 1f)    // Прозрачный в конце
                }
            );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }

        private void OnMouseEnter()
        {
            _scaleTween?.Kill();
            
            _scaleTween = _hoverEffect.transform
                .DOScale(_baseScale * 2, 0.15f);
        }

        private void OnMouseExit()
        {
            _scaleTween?.Kill(); // Убиваем текущую анимацию

            _scaleTween = _hoverEffect.transform
                .DOScale(_baseScale, 0.15f);
        }
    }
}
