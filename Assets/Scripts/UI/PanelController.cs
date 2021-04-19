
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class PanelController : MonoBehaviour
{

    [System.Serializable]
    public class PlayerCollidedEvent : UnityEvent { }

    string ThisScene;
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

        ThisScene = SceneManager.GetActiveScene().name;
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
                SaveHighscore();
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

                LoadHighscores();



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
                LoadHighscores();
                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                levelselectUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                inGameUI.SetActive(false);
                inGame = false;
                SceneManager.LoadScene("Start Screen Scene");

                ThisScene = SceneManager.GetActiveScene().name;
                if (Advertisement.IsReady())
                {
                    Advertisement.Show();
                }
                break;
            case 5:
                Debug.Log("Quit game");
                Application.Quit();
                break;
            case 6:
                Application.OpenURL("https://www.ronaldvarela.com/");
                break;
            case 7:
                ChangetoLevel("Level1");
                break;
            case 8:
                ChangetoLevel("Level2");
                break;
            case 9:
                ChangetoLevel("Level3");
                break;
            case 10:
                ChangetoLevel("Level4");
                break;
            case 11:
                ChangetoLevel("Level5");
                break;
            case 12:
                ChangetoLevel("Level6");
                break;
            default:
                break;
        }}

    private void SaveHighscore()
    {
        Debug.Log("Saving: " + "Highscore" + SceneManager.GetActiveScene().name[5]);
        if (player.GetComponent<SnakeHeadScript>().score > int.Parse(PlayerPrefs.GetString("Highscore"+ SceneManager.GetActiveScene().name[5])))
        {
            PlayerPrefs.SetString("Highscore" + SceneManager.GetActiveScene().name[5], player.GetComponent<SnakeHeadScript>().score.ToString());

        }
        PlayerPrefs.Save();
    }
    private void LoadHighscores()
    {
        if (PlayerPrefs.HasKey("Highscore1"))
        {

            levelselectUI.transform.Find("LevelOne").Find("Highscore1").GetComponent<Text>().text 
                = PlayerPrefs.GetString("Highscore1");
        } else
        {
            PlayerPrefs.SetString("Highscore1", "0");
        }
        if (PlayerPrefs.HasKey("Highscore2"))
        {

            levelselectUI.transform.Find("LevelTwo").Find("Highscore2").GetComponent<Text>().text
                = PlayerPrefs.GetString("Highscore2");
        }
        else
        {
            PlayerPrefs.SetString("Highscore2", "0");
        }
        if (PlayerPrefs.HasKey("Highscore3"))
        {

            levelselectUI.transform.Find("LevelThree").Find("Highscore3").GetComponent<Text>().text
                = PlayerPrefs.GetString("Highscore3");
        }
        else
        {
            PlayerPrefs.SetString("Highscore3", "0");
        }
        if (PlayerPrefs.HasKey("Highscore4"))
        {

            levelselectUI.transform.Find("LevelFour").Find("Highscore4").GetComponent<Text>().text
                = PlayerPrefs.GetString("Highscore4");
        }
        else
        {
            PlayerPrefs.SetString("Highscore4", "0");
        }
        if (PlayerPrefs.HasKey("Highscore5"))
        {

            levelselectUI.transform.Find("LevelFive").Find("Highscore5").GetComponent<Text>().text
                = PlayerPrefs.GetString("Highscore5");
        }
        else
        {
            PlayerPrefs.SetString("Highscore5", "0");
        }
        if (PlayerPrefs.HasKey("Highscore6"))
        {

            levelselectUI.transform.Find("LevelSix").Find("Highscore6").GetComponent<Text>().text
                = PlayerPrefs.GetString("Highscore6");
        }
        else
        {
            PlayerPrefs.SetString("Highscore6", "0");
        }
    }

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

        ThisScene = SceneManager.GetActiveScene().name;
    }
}
