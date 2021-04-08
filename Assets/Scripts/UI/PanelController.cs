
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PanelController : MonoBehaviour
{

    [System.Serializable]
    public class PlayerCollidedEvent : UnityEvent { }

    Scene ThisScene;
    bool inGame;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject helpMenu;
    public GameObject inGameUI;
    public GameObject gameOverUi;
    public GameObject levelselectUI;
    public GameObject player;
    public GameObject scoreT;
    string LoadedLevel;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4071807", true);

        ThisScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inGame)
        {
            return;
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        } else
        {
            if (!player.GetComponent<SnakeHeadScript>().isAlive)
            {
                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }



            scoreT.GetComponent<Text>().text = player.GetComponent<SnakeHeadScript>().score.ToString();
        } 
    }


    public void onButtonClick(int choiceButt)
    {
        switch (choiceButt)
        {
            case 0:
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                levelselectUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
                break;
            case 1:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                levelselectUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                break;
            case 2:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                break;
            case 4:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                inGameUI.SetActive(false);
                inGame = false;
                SceneManager.LoadScene("Start Screen Scene");
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
                break;
            case 5:
                ChangetoLevel("Level1");
                break;
            case 6:
                ChangetoLevel("Level2");
                break;
            case 7:
                ChangetoLevel("Level3");
                break;
            case 8:
                Debug.Log("Quit game");
                Application.Quit();
                break;
            case 9:
                Application.OpenURL("https://www.ronaldvarela.com/");
                break;
            default:
                break;
        }}

    private void ChangetoLevel(string LevelChange)
    {
        inGameUI.SetActive(true);
        inGame = true;
        //this.GetComponent<Animator>().SetInteger. SetInteger.ChangeScene(1);
        this.GetComponent<Animator>().SetInteger("ChangeScene", 1);
        LoadedLevel = LevelChange;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(LoadedLevel);
        levelselectUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
        this.GetComponent<Animator>().SetInteger("ChangeScene", 2);
    }
}
