using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEditor.SceneManagement;

public class SceneStartManager : EditorWindow
{
    [MenuItem("Play/Lobby")]
    // Start is called before the first frame update
    public static void PlayLoginScene()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        EditorSceneManager.OpenScene("Assets/Scenes/LobbyScene.unity");

        EditorApplication.isPlaying = true;
    }
}
