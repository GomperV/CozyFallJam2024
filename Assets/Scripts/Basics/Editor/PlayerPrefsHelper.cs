using UnityEditor;

using UnityEngine;

namespace Basics
{
    public class PlayerPrefsHelper : MonoBehaviour
    {
        [MenuItem("Tools/Reset all player prefs (game options)")]
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
