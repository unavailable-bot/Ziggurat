using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Runtime.BankLogic.VFX
{
    public class HoverVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hoverEffect;
        private const float FADE_DURATION = 0.25f;
        private Bank _parent;
        
        private Coroutine _emissionRoutine;
        private ParticleSystem.EmissionModule _emission;
        private ParticleSystem.MainModule _mainModule;

        private Vector3 _baseScale;
        private Tween _scaleTween;

        private void Awake()
        {
            _baseScale = _hoverEffect.transform.localScale;
            _parent = GetComponentInParent<Bank>();
            _emission = _hoverEffect.emission;
            _mainModule = _hoverEffect.main;
            _emission.rateOverTime = 1;
            _mainModule.simulationSpeed = 0.5f;
            _hoverEffect.Play();
        }

        private void Start()
        {
            var colorOverLifetime = _hoverEffect.colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(new Color(1f, 0f, 0f), 0f),   // Алый в начале
                    new GradientColorKey(new Color(1f, 0f, 0f), 1f),   // Алый в конце
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(0f, 0f),   // Прозрачный в начале
                    new GradientAlphaKey(1f, 0.5f), // Полностью видимый по центру
                    new GradientAlphaKey(0f, 1f)    // Прозрачный в конце
                }
            );
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
        }

        private void OnMouseEnter()
        {
            /*
            if (_emissionRoutine != null) StopCoroutine(_emissionRoutine);
            _emissionRoutine = StartCoroutine(FadeEmission(0, 2)); // 20 – желаемый rate
            */
            Debug.Log("Mouse enter");
            _scaleTween?.Kill();
            
            _hoverEffect.Play();
            _scaleTween = _hoverEffect.transform
                .DOScale(_baseScale * 2, 0.15f);
        }

        private void OnMouseExit()
        {
            /*
            if (_emissionRoutine != null) StopCoroutine(_emissionRoutine);
            _emissionRoutine = StartCoroutine(FadeEmission(_mainModule.simulationSpeed, 0.5f));
            */
            Debug.Log("Mouse exit");
            _scaleTween?.Kill(); // Убиваем текущую анимацию

            _scaleTween = _hoverEffect.transform
                .DOScale(_baseScale, 0.15f)
                .OnComplete(() =>
                {
                    _hoverEffect.Stop();
                });
        }
        
        private IEnumerator FadeEmission(float from, float to)
        {
            float time = 0;
            while (time < FADE_DURATION)
            {
                float value = Mathf.Lerp(from, to, time / FADE_DURATION); // Очень резкий старт
                _mainModule.simulationSpeed = value;
                time += Time.deltaTime;
                yield return null;
            }
            _mainModule.simulationSpeed = to;
        }
    }
}
