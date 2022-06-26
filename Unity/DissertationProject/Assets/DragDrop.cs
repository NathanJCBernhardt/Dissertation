using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Threading;


public class DragDrop : MonoBehaviour
{

    private GameObject selectedObject;
    private GameObject closestObject;
    private GameObject closeValid;
    private GameObject piece;
    public GameObject[,] gameObjects = new GameObject[8, 8];
    public GameObject[] pieces = new GameObject[32];
    public Vector3 oldpos;
   
    private float oldDistance = 9999;
    private string[] ValidMoves;
    private bool turn = true;
    public int test = 1;
    public bool first = true;
    public string fen = "";
    public string bigmove = "";

    void Start()
    {

    
        gameObjects = Startup.getGameOBJS();
        pieces = Startup.getPieces();
        ValidMoves = new string[20] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

    }


    private void Update()
    {

        if (turn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {


                if (selectedObject == null)
                {
                    RaycastHit hit = CastRay();

                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("drag"))
                        {
                            return;
                        }


                        selectedObject = hit.collider.gameObject;
                        closestObject = nearest(selectedObject.transform.position, 1);
                        oldpos = selectedObject.transform.position;


                        if (selectedObject.name.Contains("White Pawn"))
                        {

                            string test = closestObject.name;
                            string test2 = closestObject.name;
                            int k = (int)test[1] + 1;
                            int k2 = (int)test[1] + 2;
                            test = test.Replace(test[1], (char)k);
                            test2 = test2.Replace(test2[1], (char)k2);

                            if (!closestObject.name.Contains("2"))
                            {
                                ValidMoves[0] = test;
                            }
                            else
                            {
                                ValidMoves[0] = test;
                                ValidMoves[1] = test2;
                            }

                        }
                        else if (selectedObject.name.Contains("Rook"))
                        {
                            int count = 0;

                            foreach (GameObject g in gameObjects)
                            {
                                if (g.name.Contains(closestObject.name[1]))
                                {
                                    ValidMoves[count] = g.name;
                                    count = count + 1;
                                }
                                if (g.name.Contains(closestObject.name[0]))
                                {
                                    ValidMoves[count] = g.name;
                                    count = count + 1;
                                }
                            }
                        }
                        else if (selectedObject.name.Contains("Bishop"))
                        {
                            int count = 0;
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (gameObjects[i, j].name.Equals(closestObject.name))
                                    {
                                        for (int k = 0; k < 8; k++)  //1
                                        {

                                            if (i + k >= 0 && j - k >= 0) // 0 +1 = 1 //2 - 1 = 1
                                            {
                                               // ValidMoves[count] = gameObjects[i + k, j - k].name;
                                               // UnityEngine.Debug.Log(ValidMoves[count]);
                                                count = count + 1;
                                            }
                                            else
                                            {
                                                k = 20;

                                                i = 20;
                                                j = 20;
                                            }

                                        }
                                    }
                                }
                            }
                        }





                        Cursor.visible = false;
                    }
                }

                else
                {
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                    Vector3 testVec = new Vector3(worldPosition.x, .25f, worldPosition.z);
                    closeValid = nearest(testVec, 1);

                    //  if (ValidMoves.Contains(closeValid.name))
                    //{
                    Vector3 newVector = new Vector3(worldPosition.x, selectedObject.transform.position.y, worldPosition.z);
                    GameObject closeobj = nearest(newVector, 3);
                    float dist = Vector3.Distance(selectedObject.transform.position, closeobj.transform.position);

                    if (dist < 0.25)
                    {
                        if (closeobj.name.Contains("Black"))
                        {
                            closeobj.transform.position = new Vector3(-0.8f, -0.5f, 0.0004f);
                        }
                    }


                    GameObject checker = nearest(oldpos, 1);
                    GameObject checker2 = nearest(newVector, 1);
                    string s1 = checker.name;
                    string s2 = checker2.name;

                    bigmove = s1 + s2;
                    UnityEngine.Debug.Log(bigmove);
                    bigmove = bigmove.ToLower();

                    if(bigmove == "e1g1")
                    {
                        GameObject rook = GameObject.Find("Right White Rook");
                        GameObject f1 = GameObject.Find("F1");
                        UnityEngine.Debug.Log(rook);
                        UnityEngine.Debug.Log(f1);
                        Vector3 testVector = new Vector3(f1.transform.position.x, rook.transform.position.y, f1.transform.position.z);
                     

                        rook.transform.position = testVector;


                    }else if (bigmove == "e1c1"){
                        GameObject rook = GameObject.Find("Left White Rook");
                        GameObject d1 = GameObject.Find("D1");
                        UnityEngine.Debug.Log(rook);
                        UnityEngine.Debug.Log(d1);
                        Vector3 testVector = new Vector3(d1.transform.position.x, rook.transform.position.y, d1.transform.position.z);

                        rook.transform.position = testVector;

                    }

                    


                    selectedObject.transform.position = newVector;



                    turn = false;
                    //  }
                    //  else
                    //  {
                    //      selectedObject.transform.position = oldpos;
                    //  }
                    selectedObject = null;
                    Cursor.visible = true;
                }

            }



            if (selectedObject != null)
            {

                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);



            }
        }




        else if (turn == false)
        {
            turn = true;
            if (test == 1)
            {
                string move1 = "";
                string move2 = "";
                string[] lines;

                var k = bestmove(bigmove,1, "");
                string output = "";
                UnityEngine.Debug.Log(k);
               
                k = k.Remove(k.LastIndexOf(System.Environment.NewLine));
                 
                output = k.Split('\n').Last();


                

                      
                move1 = output.Substring(9, 4);
                UnityEngine.Debug.Log(move1);
                string experiment = move1;
                move1 = move1.ToUpper();
                

                move2 = move1.Substring(2);
                move1 = move1.Substring(0, 2);


                lines = k.Replace("\r", "").Split('\n');
                fen = lines.Length >= 22 ? lines[22 - 1] : null;

                fen = fen.Substring(5);
                var newfen = bestmove(bigmove, 2, experiment.ToLower());

                lines = newfen.Replace("\r", "").Split('\n');
                newfen = lines.Length >= 22 ? lines[22 - 1] : null;

                fen = newfen.Substring(5);
           


                foreach (GameObject g in gameObjects)
                {
                    if (g.name == move1)
                    {
                        foreach (GameObject s in gameObjects)
                        {
                            if (s.name == move2)
                            {
                                Vector3 newVector = new Vector3(s.transform.position.x, .25f, s.transform.position.z); 
                                piece = nearest(g.transform.position, 2);
                                GameObject closeobj = nearest(newVector, 4);
                                float dist = Vector3.Distance(newVector, closeobj.transform.position);

                                if (dist < 0.25)
                                {
                                    if (closeobj.name.Contains("White"))
                                    {
                                        closeobj.transform.position = new Vector3(-0.8f, -0.5f, 0.0004f);
                                    }
                                }



                                StartCoroutine(MovePieceTowards(piece, newVector, 0.7f));
                                if (experiment == "e8g8")
                                {
                                    GameObject rook = GameObject.Find("Left Black Rook");
                                    GameObject f8 = GameObject.Find("F8");
                           
                                    Vector3 testVector = new Vector3(f8.transform.position.x, rook.transform.position.y, f8.transform.position.z);

                                    rook.transform.position = testVector;
                                }else if(experiment == "e8c8")
                                {
                                    GameObject rook = GameObject.Find("Right Black Rook");
                                    GameObject d8 = GameObject.Find("D8");
                       
                                    Vector3 testVector = new Vector3(d8.transform.position.x, rook.transform.position.y, d8.transform.position.z);

                                    rook.transform.position = testVector;
                                }


                                    

                            }
                        }
                    }
                }
            }
            turn = true;
            
        }
    }
    IEnumerator MovePieceTowards(GameObject piece, Vector3 end, float speed)
    {
        while(piece.transform.position != end)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, end, speed * Time.deltaTime);
            yield return null;
        }
    }


    private RaycastHit CastRay()
    {

        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
    private GameObject nearest(Vector3 Obj, int x)
    {
        if (x == 1)
        {

            foreach (GameObject g in gameObjects)
            {

                float dist = Vector3.Distance(Obj, g.transform.position);
                if (dist < oldDistance)
                {
                    closestObject = g;
                    oldDistance = dist;
                }
            }
            oldDistance = 9999;
            return closestObject;
        }
        else if (x == 2)
        {
            foreach (GameObject g in pieces)
            {
                float dist = Vector3.Distance(Obj, g.transform.position);
                if (dist < oldDistance)
                {

                    closestObject = g;
                    oldDistance = dist;
                }
            }

            oldDistance = 9999;

            return closestObject;
        }
        else if  (x==3)
        {

            foreach (GameObject g in pieces)
            {
                float dist = Vector3.Distance(Obj, g.transform.position);
                if (dist < oldDistance)
                {
                    if (g.name.Contains("Black"))
                    {

                        closestObject = g;
                        oldDistance = dist;
                    }

                }
            }


           
            oldDistance = 9999;
        
            return closestObject;
        }
        else
        {
            foreach (GameObject g in pieces)
            {
                float dist = Vector3.Distance(Obj, g.transform.position);
                if (dist < oldDistance)
                {
                    if (g.name.Contains("White"))
                    {

                        closestObject = g;
                        oldDistance = dist;
                    }

                }
            }

            oldDistance = 9999;
            return closestObject; 
        }
    }



    string bestmove(string move, int k, string best)
    {
     
        string output = "";
        string test = "";
        var p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "C:/Users/Nathan/Documents/project/dissertation/Unity/DissertationProject/Assets/stockfish_14.1_win_x64_avx2/stockfish_14.1_win_x64_avx2.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();


        StreamWriter myStreamWriter = p.StandardInput;
        StreamReader reader = p.StandardOutput;


        if (first == true)
        {
            myStreamWriter.WriteLine("position startpos moves "+bigmove);
            first = false;
        }
        else
        {
            if (k == 1)
            {
                test = "position fen " + fen + " moves " + bigmove;
           
                myStreamWriter.WriteLine(test);
            }
            else
            {
                test = "position fen " + fen + " moves " + best;
                myStreamWriter.WriteLine(test);

            }
        }

        myStreamWriter.WriteLine("d");
        

        string processString = "go depth 20";
        myStreamWriter.WriteLine(processString);
        Thread.Sleep(1200);
        p.StandardInput.Flush();
        p.StandardInput.Close();

        output = reader.ReadToEnd();


        p.WaitForExit();

       


        
        return output;
    }

}








