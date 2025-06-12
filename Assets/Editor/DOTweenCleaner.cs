#if UNITY_EDITOR
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class DOTweenCleaner
    {
        static DOTweenCleaner()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                Debug.Log("Exiting PlayMode");
                // 1) убиваем ВСЕ твины
                DOTween.KillAll(true);
                DOTween.Clear(true);

                // 2) ищем и удаляем скрытый DOTweenComponent (он в DontDestroyOnLoad)
                //    ищем по его типу, а не по имени GameObject
                var comps = Resources.FindObjectsOfTypeAll<DG.Tweening.Core.DOTweenComponent>();
                foreach (var comp in comps)
                {
                    Object.DestroyImmediate(comp.gameObject);
                    Debug.Log("[DOTweenCleaner] Destroyed hidden DOTweenComponent");
                }
            }
        }
    }
}
#endif