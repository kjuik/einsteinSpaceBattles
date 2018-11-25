using UnityEngine;
using UnityEditor;

public class TimeController : EditorWindow
{
    [MenuItem("Window/Time Controller")]
    static void Init()
    {
        TimeController window = (TimeController)EditorWindow.GetWindow(typeof(TimeController));
        window.Show();
    }

    void OnGUI()
    {
        Time.timeScale = EditorGUILayout.Slider("Time speed", Time.timeScale, 0.1f, 10f);
    }
}