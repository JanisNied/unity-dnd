using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
   public void ChangeScene (int id)
    {
        SceneManager.LoadScene(id, LoadSceneMode.Single);
    }
    public void Quit ()
    {
        Application.Quit();
    }
}
