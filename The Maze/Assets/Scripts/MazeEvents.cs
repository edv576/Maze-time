using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeEvents : MonoBehaviour
{

    GameObject maze;
    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("Maze");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToGeneration()
    {
        maze.GetComponent<MazeController>().DestroyMaze();
        Destroy(maze);

        SceneManager.LoadScene(1);



    }


}
