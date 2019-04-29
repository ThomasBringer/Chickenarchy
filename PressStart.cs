using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}