using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void OpenScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
