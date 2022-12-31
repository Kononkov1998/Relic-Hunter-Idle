using UnityEditor;

namespace Utilities.Editor.EditorWindows
{
    public class ScriptsReloader : EditorWindow
    {
        [MenuItem("Tools/ReloadScripts")]
        private static void Init()
        {
            ReloadScripts();
        }

        private static void ReloadScripts()
        {
            EditorUtility.RequestScriptReload();
        }
    }
}