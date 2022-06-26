using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour { 


    public static GameObject[,] gameObjects = new GameObject[8,8];
    public static GameObject[] pieces = new GameObject[32];


    public void Start()
    {
        initialise();
    }
    public void initialise()
    {
        gameObjects.SetValue(GameObject.Find("A1"), 0,0);
        gameObjects.SetValue(GameObject.Find("A2"), 0,1);
        gameObjects.SetValue(GameObject.Find("A3"), 0,2);
        gameObjects.SetValue(GameObject.Find("A4"), 0,3);
        gameObjects.SetValue(GameObject.Find("A5"), 0,4);
        gameObjects[0,5] = GameObject.Find("A6");
        gameObjects[0,6] = GameObject.Find("A7");
        gameObjects[0,7] = GameObject.Find("A8");
        gameObjects[1,0] = GameObject.Find("B1");
        gameObjects[1,1] = GameObject.Find("B2");
        gameObjects[1,2] = GameObject.Find("B3");
        gameObjects[1,3] = GameObject.Find("B4");
        gameObjects[1,4] = GameObject.Find("B5");
        gameObjects[1,5] = GameObject.Find("B6");
        gameObjects[1,6] = GameObject.Find("B7");
        gameObjects[1,7] = GameObject.Find("B8");
        gameObjects[2,0] = GameObject.Find("C1");
        gameObjects[2,1] = GameObject.Find("C2");
        gameObjects[2,2] = GameObject.Find("C3");
        gameObjects[2,3] = GameObject.Find("C4");
        gameObjects[2,4] = GameObject.Find("C5");
        gameObjects[2,5] = GameObject.Find("C6");
        gameObjects[2,6] = GameObject.Find("C7");
        gameObjects[2,7] = GameObject.Find("C8");
        gameObjects[3,0] = GameObject.Find("D1");
        gameObjects[3,1] = GameObject.Find("D2");
        gameObjects[3,2] = GameObject.Find("D3");
        gameObjects[3,3] = GameObject.Find("D4");
        gameObjects[3,4] = GameObject.Find("D5");
        gameObjects[3,5] = GameObject.Find("D6");
        gameObjects[3,6] = GameObject.Find("D7");
        gameObjects[3,7] = GameObject.Find("D8");
        gameObjects.SetValue(GameObject.Find("E1"), 4,0);
        gameObjects.SetValue(GameObject.Find("E2"), 4,1);
        gameObjects.SetValue(GameObject.Find("E3"), 4,2);
        gameObjects.SetValue(GameObject.Find("E4"), 4,3);
        gameObjects.SetValue(GameObject.Find("E5"), 4,4);
        gameObjects[4,5] = GameObject.Find("E6");
        gameObjects[4,6] = GameObject.Find("E7");
        gameObjects[4,7] = GameObject.Find("E8");
        gameObjects[5,0] = GameObject.Find("F1");
        gameObjects[5,1] = GameObject.Find("F2");
        gameObjects[5,2] = GameObject.Find("F3");
        gameObjects[5,3] = GameObject.Find("F4");
        gameObjects[5,4] = GameObject.Find("F5");
        gameObjects[5,5] = GameObject.Find("F6");
        gameObjects[5,6] = GameObject.Find("F7");
        gameObjects[5,7] = GameObject.Find("F8");
        gameObjects[6,0] = GameObject.Find("G1");
        gameObjects[6,1] = GameObject.Find("G2");
        gameObjects[6,2] = GameObject.Find("G3");
        gameObjects[6,3] = GameObject.Find("G4");
        gameObjects[6,4] = GameObject.Find("G5");
        gameObjects[6,5] = GameObject.Find("G6");
        gameObjects[6,6] = GameObject.Find("G7");
        gameObjects[6,7] = GameObject.Find("G8");
        gameObjects[7,0] = GameObject.Find("H1");
        gameObjects[7,1] = GameObject.Find("H2");
        gameObjects[7,2] = GameObject.Find("H3");
        gameObjects[7,3] = GameObject.Find("H4");
        gameObjects[7,4] = GameObject.Find("H5");
        gameObjects[7,5] = GameObject.Find("H6");
        gameObjects[7,6] = GameObject.Find("H7");
        gameObjects[7,7] = GameObject.Find("H8");
       


        pieces[0] = GameObject.Find("White King");
        pieces[1] = GameObject.Find("White Queen");
        pieces[2] = GameObject.Find("Left White Bishop");
        pieces[3] = GameObject.Find("Left White Knight");
        pieces[4] = GameObject.Find("Right White Knight");
        pieces[5] = GameObject.Find("Right White Bishop");
        pieces[6] = GameObject.Find("Left White Rook");
        pieces[7] = GameObject.Find("Right White Rook");
        pieces[8] = GameObject.Find("Black King");
        pieces[9] = GameObject.Find("Black Queen");
        pieces[10] = GameObject.Find("Right Black Bishop");
        pieces[11] = GameObject.Find("Left Black Bishop");
        pieces[12] = GameObject.Find("Left Black Knight");
        pieces[13] = GameObject.Find("Right Black Knight");
        pieces[14] = GameObject.Find("Left Black Rook");
        pieces[15] = GameObject.Find("Right Black Rook");
        pieces[16] = GameObject.Find("Black Pawn");
        pieces[17] = GameObject.Find("Black Pawn (1)");
        pieces[18] = GameObject.Find("Black Pawn (2)");
        pieces[19] = GameObject.Find("Black Pawn (3)");
        pieces[20] = GameObject.Find("Black Pawn (4)");
        pieces[21] = GameObject.Find("Black Pawn (5)");
        pieces[22] = GameObject.Find("Black Pawn (6)");
        pieces[23] = GameObject.Find("Black Pawn (7)");
        pieces[24] = GameObject.Find("White Pawn");
        pieces[25] = GameObject.Find("White Pawn (1)");
        pieces[26] = GameObject.Find("White Pawn (2)");
        pieces[27] = GameObject.Find("White Pawn (3)");
        pieces[28] = GameObject.Find("White Pawn (4)");
        pieces[29] = GameObject.Find("White Pawn (5)");
        pieces[30] = GameObject.Find("White Pawn (6)");
        pieces[31] = GameObject.Find("White Pawn (7)");

    }


    public static GameObject[,] getGameOBJS()
    {
        return gameObjects;
    }

    public static GameObject[] getPieces()
    {
        return pieces;
    }

    public void output()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Debug.Log(gameObjects[i, j].name);
            }
        }
    }
  
}

