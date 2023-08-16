using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeSceneCharacter()
    {
        SceneManager.LoadScene("animalScene");
    }

    public void ChangeSceneDesign()
    {
        SceneManager.LoadScene("DesignScene");
    }

    public void ChangeSceneMain()
    {
        SceneManager.LoadScene("main");
    }
}
