using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    static int wG, hG;

    public static Element[,] elements;

    public static void createArray(int w, int h)
    {
        elements = new Element[w, h];
    }

    public static void uncoverField(int gameState)
    {
        foreach (Element elem in elements)
        {
            if (elem.mine) elem.loadTexture(0);
        }
    }

    public static bool mineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < FieldCreator.wS && y < FieldCreator.hS)
            return elements[x, y].mine;

        return false;
    }

    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        for (int i = y - 1; i <= y + 1; i++)
            for (int j = x - 1; j <= x + 1; j++)
                if (mineAt(j, i)) { ++count; }

        return count;
    }

    public static void FFuncover(int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < FieldCreator.wS && y < FieldCreator.hS)
        {
            if (visited[x, y])
                return;

            if (elements[x, y].flagged) GameObject.Find("MinesAmount").GetComponent<MinesAmount>().updateMinesAmount(1); 

            elements[x, y].loadTexture(adjacentMines(x, y));

            if (adjacentMines(x, y) > 0) return;

            visited[x, y] = true;

            for (int i = y - 1; i <= y + 1; i++)
                for (int j = x - 1; j <= x + 1; j++)
                    FFuncover(j, i, visited);
        }
    }

    public static bool inFinished()
    {
        foreach(Element elem in elements)
        {
            if (elem.isCovered() && !elem.mine)
                return false;
        }

        return true;
    }
}