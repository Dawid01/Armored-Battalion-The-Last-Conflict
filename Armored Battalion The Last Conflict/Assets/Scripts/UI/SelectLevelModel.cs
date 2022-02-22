using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectLevelModel : MonoBehaviour
{

    public TextMeshProUGUI indexText;
    public enum LevelType{Tutiorial, DestroyAllEnemies, Survical};
    public LevelType levelType;
    public TextMeshProUGUI typeText;
    public bool unlocked = false;
    public GameObject lockedObj;
    public bool complited = false;
    public Transform statusObj;
    public Button startButton;
    private int index;


    void Start()
    {
        Load();
    }

    void Update()
    {
        
    }

    private void Load()
    {
        index = transform.GetSiblingIndex();
        indexText.text = "lvl " + index;
        statusObj.GetChild(0).gameObject.SetActive(complited);
        statusObj.GetChild(1).gameObject.SetActive(!complited);
        lockedObj.gameObject.SetActive(!unlocked);

        switch (levelType) {
            case LevelType.Tutiorial:
                typeText.text = "Tutorial";
                break;
            case LevelType.DestroyAllEnemies:
                typeText.text = "Destroy all enemies";
                break;
            case LevelType.Survical:
                typeText.text = "Survival";
                break;
        }

        startButton.onClick.AddListener(() => loadLevel());

    }

    private void loadLevel() {

        if (levelType == LevelType.Tutiorial)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("Level" + index);

    }
}
