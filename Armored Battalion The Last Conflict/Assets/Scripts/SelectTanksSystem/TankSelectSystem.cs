using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankSelectSystem : MonoBehaviour
{
    public Transform content;
    public GameObject tankImage;
    public List<GameObject> tankImages;
    public List<GameObject> tanks;
    public GameObject menuPause;

    private ClassicGame classicGame;
    public SwipeTanks swipeTanks;


    public float padding = 10f;
    void Start()
    {
        classicGame = FindObjectOfType<ClassicGame>();
        for(int i = 0; i < tankImages.Count; i++){
            RectTransform image = Instantiate(tankImages[i], content).GetComponent<RectTransform>();
            Button btn = image.GetChild(1).GetComponent<Button>();
            btn.name = "" + i;
            btn.onClick.AddListener(() => Select(int.Parse(btn.name)));            
        }
        swipeTanks.enabled = true;
    }



    void Update()
    {

    }

    public void Select(){
        PlayerPrefs.SetInt("menu_tank", swipeTanks.selectedIndex);
        tanks[swipeTanks.selectedIndex].SetActive(true);
        Destroy(gameObject);
        menuPause.SetActive(true);
        classicGame.StartGame();
    }

    void Select(int index)
    {
        PlayerPrefs.SetInt("menu_tank", index);
        tanks[index].SetActive(true);
        Destroy(gameObject);
        menuPause.SetActive(true);
        classicGame.StartGame();
    }

    public void loadLevel(int number){
        SceneManager.LoadScene(number);
    }
}
