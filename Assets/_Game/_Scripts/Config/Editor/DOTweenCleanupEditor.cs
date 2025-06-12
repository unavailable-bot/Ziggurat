#if UNITY_EDITOR
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Config.Editor
{
    [InitializeOnLoad]
    public static class DOTweenCleanupEditor
    {
        static DOTweenCleanupEditor()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                DOTween.KillAll(true);
                DOTween.Clear(true);

                // Удаляем [DOTween] через рефлексию
                var allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
                foreach (var go in allGameObjects)
                {
                    if (go.name == "[DOTween]")
                    {
                        Object.DestroyImmediate(go);
                        Debug.Log("✅ Удалён скрытый объект [DOTween] через рефлексию.");
                        break;
                    }
                }
            }
        }
    }
}
#endif