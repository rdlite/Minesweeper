using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class FieldCreator : MonoBehaviour {
    public static int gameState = 0;

    public int w, h;

    public static bool askWindowIsOpen = false;

    bool isToMenuAsker, isReloadGameAsker, isSaveGameAsker;

    public static int wS, hS;

    private int x, y;

    public static bool isTouched = false;

    public int minesAmount;

    GameObject toMenuAsker, reloadGameAsker, saveGameAsker;

    void Start()
    {
        startNewGame();
        reloadGameAsker = GameObject.Find("ReloadGameAsker");
        reloadGameAsker.SetActive(false);

        toMenuAsker = GameObject.Find("ToMenuAsker");
        toMenuAsker.SetActive(false);

        saveGameAsker = GameObject.Find("SaveAsker");
        saveGameAsker.SetActive(false);
    }

    public void setXY(int x, int y)
    {
        this.x = x;
        this.y = y;

        locateMines();
    }

    public void startNewGame()
    {

        if (askWindowIsOpen) return;
        
        GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileDeafult; 
        isTouched = false;
        gameState = 0;
        Timer.timer = 0;
        Timer.stopTimer = false;

        if (GameObject.Find("element") != null)
        {
            Element[] elem = FindObjectsOfType<Element>();

            foreach (Element item in elem)
                Destroy(item.gameObject);
        }

        if (PlayerPrefs.GetString("isCustom") == "true")
        {
            w = PlayerPrefs.GetInt("WidthC");
            h = PlayerPrefs.GetInt("HeightC");
            minesAmount = PlayerPrefs.GetInt("MinesAmountC");
            GameObject.Find("MinesAmount").GetComponent<MinesAmount>().minesAmount = PlayerPrefs.GetInt("MinesAmountC");  
        }else
        {
            w = PlayerPrefs.GetInt("Width");
            h = PlayerPrefs.GetInt("Height");
            minesAmount = PlayerPrefs.GetInt("MinesAmount");
            GameObject.Find("MinesAmount").GetComponent<MinesAmount>().minesAmount = PlayerPrefs.GetInt("MinesAmount");  
        }

        GameObject.Find("MinesAmount").GetComponent<MinesAmount>().updateMinesAmount(0);

        wS = w;
        hS = h;
        Grid.createArray(w, h);

        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
            {
                GameObject elem = Instantiate(Resources.Load("Prefabs/Element") as GameObject);
                Grid.elements[j, i] = elem.GetComponent<Element>();
                Grid.elements[j, i].transform.position = new Vector3(j, i, -1);
                Grid.elements[j, i].xEl = j; Grid.elements[j, i].yEl = i;
                elem.transform.SetParent(GameObject.Find("panels").transform);
                elem.name = "element";
            }

        GameObject boundBox = GameObject.Find("BoundBox");
        GameObject mainCamera = GameObject.Find("Main Camera");

        mainCamera.GetComponent<Camera>().transform.position = new Vector3(0, h, -2);

        mainCamera.GetComponent<CameraController>().maxSize = mainCamera.GetComponent<Camera>().orthographicSize = Mathf.Min(h, w);

        if (mainCamera.GetComponent<CameraController>().maxSize > 10) mainCamera.GetComponent<CameraController>().maxSize = 10;

        boundBox.GetComponent<BoxCollider2D>().offset = new Vector2((w - 1) / 2f, (h - 1) / 2f);
        boundBox.transform.position = Vector3.zero;

        if (mainCamera.GetComponent<Camera>().orthographicSize <= 5)
        {
            mainCamera.GetComponent<Camera>().orthographicSize = 2f;
            boundBox.GetComponent<BoxCollider2D>().size = new Vector2(w - 0.4f, h);
        }
        else
        if (mainCamera.GetComponent<Camera>().orthographicSize > 5 && mainCamera.GetComponent<Camera>().orthographicSize < 16)
        {
            mainCamera.GetComponent<Camera>().orthographicSize = 4;
            boundBox.GetComponent<BoxCollider2D>().size = new Vector2(w - 0.7f, h );
        }
        else
        {
            mainCamera.GetComponent<Camera>().orthographicSize = 6;
            boundBox.GetComponent<BoxCollider2D>().size = new Vector2(w - 1, h);
        }
    }

    void locateMines()
    {
        for (int i = 0; i < minesAmount; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);

            if (Grid.elements[x, y].mine == true || (this.x == x && this.y == y && isTouched)) { i--; continue; }

            Grid.elements[x, y].mine = true;
        }

        x = y = 0;
    }

    public void toMenu()
    {
        toMenuAsker.SetActive(!askWindowIsOpen);
        askWindowIsOpen = !askWindowIsOpen;
        isToMenuAsker = askWindowIsOpen;
    }

    public void reloadGame()
    {
        if (isTouched && gameState == 0)
        {
            reloadGameAsker.SetActive(!askWindowIsOpen);
            askWindowIsOpen = !askWindowIsOpen;
            isReloadGameAsker = askWindowIsOpen;
        }else startNewGame();
    }

    public void saveGame()
    {
        saveGameAsker.SetActive(!askWindowIsOpen);
        askWindowIsOpen = !askWindowIsOpen;
        isSaveGameAsker = askWindowIsOpen;
        
        saveGameAsker.transform.GetChild(4).GetComponent<Text>().text = Timer.timer.ToString();
    }

    public void yesButtonController()
    {
        if (isToMenuAsker) 
        {
            toMenu();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
        else if (isReloadGameAsker)
        {
            reloadGame();
            startNewGame();
        }else
        {
            string gameType = PlayerPrefs.GetString("GameType");

            PlayerPrefs.SetInt(gameType, PlayerPrefs.GetInt(gameType) + 1);
            
            int rowsCount = PlayerPrefs.GetInt(gameType);

            PlayerPrefs.SetString(gameType + "Name" + rowsCount, saveGameAsker.transform.GetChild(2).GetComponent<InputField>().text);
            PlayerPrefs.SetString(gameType + "Time" + rowsCount, Timer.timer.ToString());

            saveGame();
        }
    }

    public void noButtonController()
    {
        if (isToMenuAsker) 
        {
            toMenu();
        }
        else if (isReloadGameAsker)
        {
            reloadGame();
        }else
        {
            saveGame();
        }
    }
}
