using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TankSelectSystem : MonoBehaviour
{
    public Transform content;
    public List<GameObject> tankImages;
    public List<GameObject> tanks;
    public GameObject menuPause;

    private ClassicGame classicGame;
    public SwipeTanks swipeTanks;
    private GameObject selectedTank;

    public int selectionPoints;
    public TextMeshProUGUI selectionPointsText;
    public float padding = 10f;

    [HideInInspector]
    public List<SelectTankImage> selectTankImages = new List<SelectTankImage>();

    void Start()
    {
        classicGame = FindObjectOfType<ClassicGame>();
        for(int i = 0; i < tankImages.Count; i++){
            GameObject image = Instantiate(tankImages[i], content);
            selectTankImages.Add(image.GetComponent<SelectTankImage>());
        }
        swipeTanks.enabled = true;
        selectionPointsText.text = "selection points: " + selectionPoints;
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        
    }

    public void Select(){
        SelectTankImage selectTankImage = selectTankImages[swipeTanks.selectedIndex];
        int newSelectionPoints = selectionPoints - selectTankImage.cost;
        if (newSelectionPoints >= 0) {
            selectionPoints = newSelectionPoints;
            selectionPointsText.text = "selection points: " + selectionPoints;
            PlayerPrefs.SetInt("menu_tank", swipeTanks.selectedIndex);
            GameObject tank = tanks[swipeTanks.selectedIndex];
            selectedTank = Instantiate(tank, tank.transform.position, tank.transform.rotation);
            selectedTank.SetActive(true);
            selectedTank.transform.parent = null;
            gameObject.SetActive(false);
            menuPause.SetActive(true);
            classicGame.StartGame();
        }
    }


    public void NextTry() {
        gameObject.SetActive(true);
        menuPause.SetActive(true);
        Destroy(selectedTank);
    }

    public void loadLevel(int number){
        SceneManager.LoadScene(number);
    }
}
