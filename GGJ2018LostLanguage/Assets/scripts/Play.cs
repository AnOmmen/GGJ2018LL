using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public void LoadByIndex(int scenelvl)
    {
        SceneManager.LoadScene(scenelvl);
    }
}