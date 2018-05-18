using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour {
    Vector2 cameraStartPosAfterPressed;
    Vector2 delta;

    public bool mine;

    public int mines;

    public bool isExploded;
    public bool flagged;

    public int xEl, yEl;

    public void loadTexture(int adjacentCount)
    {
        if (!isExploded)
            if (mine)
            {    
                GetComponent<SpriteRenderer>().sprite = (FieldCreator.gameState == 1) ? transform.GetComponentInParent<SpritesDB>().mineTexture : transform.GetComponentInParent<SpritesDB>().flagTexture;
            }
            else
                GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().emptyTextures[adjacentCount];
    }

    public bool isCovered()
    {
        return (GetComponent<SpriteRenderer>().sprite.name == "Default");
    }

    private void OnMouseDown()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().startTimer = true;
        cameraStartPosAfterPressed = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        delta = new Vector2(Input.mousePosition.x - cameraStartPosAfterPressed.x,
                            Input.mousePosition.y - cameraStartPosAfterPressed.y);

        if (!FieldCreator.askWindowIsOpen && FieldCreator.gameState == 0 && isCovered() && Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y) < 20) 
            GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileGameTouched;
        else GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileDeafult;
        // if ( && GameObject.Find("Main Camera").GetComponent<CameraController>().timer >= 1f && (isCovered()) && Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y) < 10)
        // {
        //    

        //     if (FieldCreator.gameState == 0)
        //         if (!flagged) { flagged = true; }
        //         else flagged = false;

        //    //if (FieldCreator.gameState == 0)
        //    //    if (flagged) { print("2"); GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().defaultTexture; flagged = false; }
        //    //    else { print("3"); GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().flagTexture; flagged = true; }
        // }
    }

    private void OnMouseUp()
    {
        if (FieldCreator.askWindowIsOpen) { GameObject.Find("Main Camera").GetComponent<CameraController>().startTimer = false; return; } 

        GameObject.Find("Main Camera").GetComponent<CameraController>().startTimer = false;
        if (FieldCreator.gameState == 0) GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileDeafult;

        if (FieldCreator.gameState == 0)
            if (Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y) < 20)
            {
                if (!flagged && GameObject.Find("Main Camera").GetComponent<CameraController>().timer <= 0.7f)
                {
                    if (mine && FieldCreator.isTouched)
                    {
                        GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileLose;

                        if (PlayerPrefs.GetString("Sound") == "true") GameObject.Find("AudioExplosion").GetComponent<AudioSource>().Play();

                        if (PlayerPrefs.GetString("Vibration") == "true") Handheld.Vibrate();
                        FieldCreator.gameState = 1;
                        isExploded = true;
                        GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().explodedMineTexture;
                    }
                    else
                    {
                        if (!FieldCreator.isTouched) { FieldCreator.isTouched = true; GameObject.Find("panels").GetComponent<FieldCreator>().setXY(xEl, yEl); }

                        if (PlayerPrefs.GetString("Sound") == "true" && isCovered()) GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();

                        loadTexture(Grid.adjacentMines(xEl, yEl));

                        Grid.FFuncover(xEl, yEl, new bool[FieldCreator.wS, FieldCreator.hS]);

                        if (Grid.inFinished())
                        {
                            if (PlayerPrefs.GetString("GameType") == "Beginner" || 
                                PlayerPrefs.GetString("GameType") == "Intermeditate" ||
                                PlayerPrefs.GetString("GameType") == "Expert")
                                GameObject.Find("panels").GetComponent<FieldCreator>().saveGame();
                                
                            GameObject.Find("SmileButton").GetComponent<Image>().sprite = GameObject.Find("panels").GetComponent<SpritesDB>().smileWin;
                            FieldCreator.gameState = 2;
                        }
                    }
                }
                else
                if (GameObject.Find("Main Camera").GetComponent<CameraController>().timer >= 0.7f && GameObject.Find("MinesAmount").GetComponent<MinesAmount>().minesAmount > 0)
                {                   
                    if (PlayerPrefs.GetString("Vibration") == "true") Handheld.Vibrate();
                    if (!flagged)
                    {
                        flagged = true;
                        GameObject.Find("MinesAmount").GetComponent<MinesAmount>().updateMinesAmount(-1);
                        GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().flagTexture;
                    }
                    else
                    {
                        flagged = false;
                        GameObject.Find("MinesAmount").GetComponent<MinesAmount>().updateMinesAmount(1);
                        GetComponent<SpriteRenderer>().sprite = transform.parent.GetComponent<SpritesDB>().defaultTexture;
                    }
                }

            }

        if (FieldCreator.gameState != 0) { Timer.stopTimer = true; Grid.uncoverField(FieldCreator.gameState); }
    }
}
