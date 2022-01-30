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


    public float padding = 10f;
    void Start()
    {
        classicGame =  FindObjectOfType<ClassicGame>();

        float w = tankImage.GetComponent<RectTransform>().rect.width / 2f + 50f;
        for(int i = 0; i < tankImages.Count; i++){
            RectTransform image = Instantiate(tankImages[i], content).GetComponent<RectTransform>();
            Button btn = image.GetChild(1).GetComponent<Button>();
            btn.name = "" + i;
            btn.onClick.AddListener(() => Select(int.Parse(btn.name)));

            image.localPosition = new Vector2(w, -175f);
            if(i < tankImages.Count - 1){
                w += image.rect.width + padding;
            }else{
                w += image.rect.width / 2f;
            }
            
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(w + 50f, content.GetComponent<RectTransform>().rect.height);
    }



    void Update()
    {

    }

    void Select(int index){
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
