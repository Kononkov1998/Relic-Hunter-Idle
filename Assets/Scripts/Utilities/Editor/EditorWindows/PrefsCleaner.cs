using UnityEditor;
using UnityEngine;

namespace Utilities.Editor.EditorWindows
{
    public class PrefsCleaner : EditorWindow
    {
        [MenuItem("Tools/Clear Player Prefs")]
        private static void Init()
        {
            ClearPlayerPrefs();
        }

        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}