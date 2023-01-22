using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] int index;

    public void StartLevel()
    {
        SceneManager.LoadScene(index);
    }
}
