using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("EscenaPrincipal");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
