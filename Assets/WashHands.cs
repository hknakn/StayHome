using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WashHands : MonoBehaviour
{
    public Button nextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevelButtonClick);
    }

    private void NextLevelButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
