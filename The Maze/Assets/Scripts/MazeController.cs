using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeController : MonoBehaviour
{
    public int rows = 2;
    public int columns = 2;
    public GameObject wall;
    public GameObject floor;
    public GameObject corner;
    public GameObject playerCharacter;
    public InputField widthInput;
    public InputField heightInput;
    public Camera ortoCamera;
    float size;
    float thickness;

    private MazeCell[,] grid;
    private int currentRow = 0;
    private int currentColumn = 0;
    private bool scanComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        ////All the floors and walls are created
        //CreateGrid();
        ////Algorithm is run to carve a path from top left to bottom right
        //HuntAndKill();
        heightInput.text = rows.ToString();
        widthInput.text = columns.ToString();
        GenerateGrid();
    }

    void CreateGrid()
    {
        size = floor.transform.localScale.x;
        thickness = floor.transform.localScale.y;
        grid = new MazeCell[rows, columns];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {

                GameObject floorTile = Instantiate(floor, new Vector3(j * size, 0, -i * size), Quaternion.identity);
                floorTile.name = "Floor_" + i + "_" + j;

                GameObject upWall = Instantiate(wall, new Vector3(j * size, (size + thickness) / 2, -i * size + (size - thickness) / 2), Quaternion.identity);
                upWall.name = "UpWall_" + i + "_" + j;

                GameObject downWall = Instantiate(wall, new Vector3(j * size, (size + thickness) / 2, -i * size - (size - thickness) / 2), Quaternion.identity);
                downWall.name = "DownWall_" + i + "_" + j;

                GameObject leftWall = Instantiate(wall, new Vector3(j * size - (size - thickness) / 2, (size + thickness) / 2, -i * size), Quaternion.Euler(0, 90, 0));
                leftWall.name = "LeftWall_" + i + "_" + j;

                GameObject rightWall = Instantiate(wall, new Vector3(j * size + (size - thickness) / 2, (size + thickness) / 2, -i * size), Quaternion.Euler(0, 90, 0));
                rightWall.name = "RightWall_" + i + "_" + j;

                grid[i, j] = new MazeCell();
                grid[i, j].upWall = upWall;
                grid[i, j].downWall = downWall;
                grid[i, j].leftWall = leftWall;
                grid[i, j].rightWall = rightWall;
                grid[i, j].floorTile = floorTile;


                floorTile.transform.parent = transform;
                upWall.transform.parent = transform;
                downWall.transform.parent = transform;
                leftWall.transform.parent = transform;
                rightWall.transform.parent = transform;
                floorTile.transform.parent = transform;

                if(i == 0 && j == 0)
                {
                    Destroy(grid[i, j].leftWall);
                    grid[i, j].leftWall = null;
                    GameObject g = new GameObject();
                    Destroy(g);
                    //g = null;
                    if(!grid[i, j].leftWall)
                    {
                        print("Destroyed");
                    }
                }

                if (i == rows - 1 && j == columns - 1)
                {
                    Destroy(grid[i, j].rightWall);
                }
            }
        }

        playerCharacter.transform.position = new Vector3(GameObject.Find("Floor_0_0").transform.position.x,
            playerCharacter.transform.position.y, GameObject.Find("Floor_0_0").transform.position.z);
    }

    bool AreThereUnvisitedNeighbors()
    {
        //Look up
        if (IsCellUnvisitedAndBetweenBoundaries(currentRow - 1, currentColumn))
        {
            return true;
        }

        //Look down
        if (IsCellUnvisitedAndBetweenBoundaries(currentRow + 1, currentColumn))
        {
            return true;
        }

        //Look left
        if (IsCellUnvisitedAndBetweenBoundaries(currentRow, currentColumn - 1))
        {
            return true;
        }

        //Look right
        if (IsCellUnvisitedAndBetweenBoundaries(currentRow, currentColumn + 1))
        {
            return true;
        }

        //There aren't unvisited neighbors
        return false;
    }

    bool AreThereVisitedNeighbors(int row, int col)
    {
        if(row>0 && grid[row - 1, col].visited)
        {
            return true;
        }

        if (row < rows-1 && grid[row + 1, col].visited)
        {
            return true;
        }

        if (col > 0 && grid[row, col - 1].visited)
        {
            return true;
        }

        if (col < columns - 1 && grid[row, col + 1].visited)
        {
            return true;
        }

        return false;
    }

    //Verifies if the cell is unvisited
    bool IsCellUnvisitedAndBetweenBoundaries(int row, int col)
    {
        if(row>=0 && row<rows && col>=0 && col<columns &&  !grid[row, col].visited)
        {
            return true;
        }

        return false;
    }

    bool IsCellBetweenBoundaries(int row, int col)
    {
        if (row >= 0 && row < rows && col >= 0 && col < columns)
        {
            return true;
        }

        return false;
    }



    void HuntAndKill()
    {
        //Mark the first cell of the random walk as visited
        grid[currentRow, currentColumn].visited = true;

        while (!scanComplete)
        {
            Walk();
            Hunt();
        }

        



        
    }

    void Walk()
    {
        while (AreThereUnvisitedNeighbors())
        {
            int direction = Random.Range(0, 4);

            switch (direction)
            {
                //Check up
                case 0:
                    {
                        if (IsCellUnvisitedAndBetweenBoundaries(currentRow - 1, currentColumn))
                        {
                            if (grid[currentRow, currentColumn].upWall)
                            {
                                Destroy(grid[currentRow, currentColumn].upWall);
                                grid[currentRow, currentColumn].upWall = null;
                            }
                            else
                            {
                                print("this");
                            }

                            currentRow--;
                            grid[currentRow, currentColumn].visited = true;

                            if (grid[currentRow, currentColumn].downWall)
                            {
                                Destroy(grid[currentRow, currentColumn].downWall);
                                grid[currentRow, currentColumn].downWall = null;
                            }

                        }
                        break;
                    }

                //Check down
                case 1:
                    {
                        if (IsCellUnvisitedAndBetweenBoundaries(currentRow + 1, currentColumn))
                        {
                            if (grid[currentRow, currentColumn].downWall)
                            {
                                Destroy(grid[currentRow, currentColumn].downWall);
                                grid[currentRow, currentColumn].downWall = null;
                            }

                            currentRow++;
                            grid[currentRow, currentColumn].visited = true;

                            if (grid[currentRow, currentColumn].upWall)
                            {
                                Destroy(grid[currentRow, currentColumn].upWall);
                                grid[currentRow, currentColumn].upWall = null;
                            }
                        }
                        break;
                    }

                //Check left
                case 2:
                    {
                        if (IsCellUnvisitedAndBetweenBoundaries(currentRow, currentColumn - 1))
                        {
                            if (grid[currentRow, currentColumn].leftWall)
                            {
                                Destroy(grid[currentRow, currentColumn].leftWall);
                                grid[currentRow, currentColumn].leftWall = null;
                            }

                            currentColumn--;
                            grid[currentRow, currentColumn].visited = true;

                            if (grid[currentRow, currentColumn].rightWall)
                            {
                                Destroy(grid[currentRow, currentColumn].rightWall);
                                grid[currentRow, currentColumn].rightWall = null;
                            }
                        }
                        break;
                    }

                //Check right
                case 3:
                    {
                        if (IsCellUnvisitedAndBetweenBoundaries(currentRow, currentColumn + 1))
                        {
                            if (grid[currentRow, currentColumn].rightWall)
                            {
                                Destroy(grid[currentRow, currentColumn].rightWall);
                                grid[currentRow, currentColumn].rightWall = null;
                            }

                            currentColumn++;
                            grid[currentRow, currentColumn].visited = true;

                            if (grid[currentRow, currentColumn].leftWall)
                            {
                                Destroy(grid[currentRow, currentColumn].leftWall);
                                grid[currentRow, currentColumn].leftWall = null;
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

    void DestroyAdjacentWall()
    {
        bool destroyed = false;

        while (!destroyed)
        {
            int direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0:
                    {
                        if(currentRow>0 && grid[currentRow - 1, currentColumn].visited)
                        {
                            if(grid[currentRow - 1, currentColumn].downWall)
                            {
                                Destroy(grid[currentRow - 1, currentColumn].downWall);
                                grid[currentRow - 1, currentColumn].downWall = null;
                            }

                            if (grid[currentRow, currentColumn].upWall)
                            {
                                Destroy(grid[currentRow, currentColumn].upWall);
                                grid[currentRow, currentColumn].upWall = null;
                            }

                            destroyed = true;
                        }
                        break;
                    }
                    
                case 1:
                    {
                        if (currentRow < rows - 1 && grid[currentRow + 1, currentColumn].visited)
                        {
                            if(grid[currentRow + 1, currentColumn].upWall)
                            {
                                Destroy(grid[currentRow + 1, currentColumn].upWall);
                                grid[currentRow + 1, currentColumn].upWall = null;
                            }

                            if (grid[currentRow, currentColumn].downWall)
                            {
                                Destroy(grid[currentRow, currentColumn].downWall);
                                grid[currentRow, currentColumn].downWall = null;
                            }

                            destroyed = true;
                        }
                        break;
                    }
                    
                case 2:
                    {
                        if (currentColumn > 0 && grid[currentRow, currentColumn - 1].visited)
                        {
                            if(grid[currentRow, currentColumn - 1].rightWall)
                            {
                                Destroy(grid[currentRow, currentColumn - 1].rightWall);
                                grid[currentRow, currentColumn - 1].rightWall = null;
                            }

                            if (grid[currentRow, currentColumn].leftWall)
                            {
                                Destroy(grid[currentRow, currentColumn].leftWall);
                                grid[currentRow, currentColumn].leftWall = null;
                            }

                            destroyed = true;
                        }
                        break;
                    }
                    
                case 3:
                    {
                        if (currentColumn < columns - 1 && grid[currentRow, currentColumn + 1].visited)
                        {
                            if(grid[currentRow, currentColumn + 1].leftWall)
                            {
                                Destroy(grid[currentRow, currentColumn + 1].leftWall);
                                grid[currentRow, currentColumn + 1].leftWall = null;
                            }

                            if (grid[currentRow, currentColumn].rightWall)
                            {
                                Destroy(grid[currentRow, currentColumn].rightWall);
                                grid[currentRow, currentColumn].rightWall = null;
                            }

                            destroyed = true;
                        }
                        break;
                    }
                    
                default:
                    {
                        break;
                    }
                    
            }

        }
    }

    void Hunt()
    {

        scanComplete = true;
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if(!grid[i,j].visited && AreThereVisitedNeighbors(i, j))
                {
                    scanComplete = false;
                    currentRow = i;
                    currentColumn = j;
                    grid[currentRow, currentColumn].visited = true;
                    DestroyAdjacentWall();
                    return;

                }
            }
        }
    }

    void RepositionOrtoCamera()
    {
        float sizeOrtoCamera = (Mathf.Max(rows, columns)*size)/2;
        ortoCamera.orthographicSize = sizeOrtoCamera;

        Vector3 ortoCameraPosition = ortoCamera.transform.position;

        float posX = size*columns/2 - size/2;
        float posZ = -size*rows/2 + size/2;

        ortoCameraPosition.x = posX;
        ortoCameraPosition.z = posZ;

        ortoCamera.transform.position = ortoCameraPosition;
        

    }

    void FixCorners()
    {
        
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                //Check upper left corner
                if (!grid[i, j].leftWall && !grid[i, j].upWall && IsCellBetweenBoundaries(i - 1, j) && IsCellBetweenBoundaries(i, j - 1) &&
                    grid[i - 1, j].leftWall && grid[i, j - 1].upWall)
                {
                    Vector3 cornerPosition = grid[i, j].floorTile.transform.position;
                    cornerPosition.x -= size / 2 - thickness/2;
                    cornerPosition.y = (size + thickness) / 2;
                    cornerPosition.z += size / 2 - thickness/2;
                    GameObject cornerFix = Instantiate(corner, cornerPosition, Quaternion.identity);
                    cornerFix.transform.parent = transform;
                }

                //Check upper right corner
                if (!grid[i, j].rightWall && !grid[i, j].upWall && IsCellBetweenBoundaries(i - 1, j) && IsCellBetweenBoundaries(i, j + 1) &&
                    grid[i - 1, j].rightWall && grid[i, j + 1].upWall)
                {
                    Vector3 cornerPosition = grid[i, j].floorTile.transform.position;
                    cornerPosition.x += size / 2 - thickness / 2;
                    cornerPosition.y = (size + thickness) / 2;
                    cornerPosition.z += size / 2 - thickness / 2;
                    GameObject cornerFix = Instantiate(corner, cornerPosition, Quaternion.identity);
                    cornerFix.transform.parent = transform;
                }

                //Check bottom left corner
                if (!grid[i, j].leftWall && !grid[i, j].downWall && IsCellBetweenBoundaries(i + 1, j) && IsCellBetweenBoundaries(i, j - 1) &&
                    grid[i + 1, j].leftWall && grid[i, j - 1].downWall)
                {
                    Vector3 cornerPosition = grid[i, j].floorTile.transform.position;
                    cornerPosition.x -= size / 2 - thickness / 2;
                    cornerPosition.y = (size + thickness) / 2;
                    cornerPosition.z -= size / 2 - thickness / 2;
                    GameObject cornerFix = Instantiate(corner, cornerPosition, Quaternion.identity);
                    cornerFix.transform.parent = transform;
                }

                //Check bottom right corner
                if (!grid[i, j].rightWall && !grid[i, j].downWall && IsCellBetweenBoundaries(i + 1, j) && IsCellBetweenBoundaries(i, j + 1) &&
                    grid[i + 1, j].rightWall && grid[i, j + 1].downWall)
                {
                    Vector3 cornerPosition = grid[i, j].floorTile.transform.position;
                    cornerPosition.x += size / 2 - thickness / 2;
                    cornerPosition.y = (size + thickness) / 2;
                    cornerPosition.z -= size / 2 - thickness / 2;
                    GameObject cornerFix = Instantiate(corner, cornerPosition, Quaternion.identity);
                    cornerFix.transform.parent = transform;
                }



            }
        }


    }

    void GenerateGrid()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        CreateGrid();
        currentRow = 0;
        currentColumn = 0;
        scanComplete = false;
        HuntAndKill();
        FixCorners();
        RepositionOrtoCamera();
    }


    public void Regenerate()
    {
        int rowsI = 2;
        int columnsI = 2;

        if (int.TryParse(heightInput.text, out rowsI))
        {
            rows = Mathf.Max(rowsI, 2);
        }

        if(int.TryParse(widthInput.text, out columnsI))
        {
            columns = Mathf.Max(columnsI, 2);
        }

        GenerateGrid();
    } 

    // Update is called once per frame
    void Update()
    {
        


    }
}
