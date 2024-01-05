using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;



public class TileGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject chest;
    public GameObject enemy;
    public GameObject enemy2;
    public Tilemap towerTiles;
    public Tile tileStairs;
    public Tile[] tilesFloor;
    public Tile[] tilesWall;
    private int widthCell = 21;
    private int heightCell = 20;
    private int width = 90;
    private int height = 60;
    private int fillerW = 10;
    private int fillerH = 5;
    private int nRX = 4;
    private int nRY = 3;
    private int numRooms;
    private int[,] map;
    public struct room {
        public room(int x, int y, int s)
        {
            rX = x;
            rY = y;
            rS = s;
        }
        public int rX { get; set; }
        public int rY { get; set; }
        public int rS { get; set; }
    };

    private room[] rooms;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("created player");
            player = Instantiate(player, new Vector3Int(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else player = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Goal") == null)
        {
            goal = Instantiate(goal, new Vector3Int(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else goal = GameObject.FindGameObjectWithTag("Goal");
        numRooms = nRY * nRX;
        //new map entirely full
        map = new int[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                map[x, y] = 1;
        var rand = new System.Random();

        //creem les sales. De moment farem un nombre fixe (12 en bloc 3x4) per facilitar la feina
        rooms = new room[numRooms];
        int c = 0;
        int cS = 0;
        for (int y = 0; y < nRY; y++)
        {
            for (int x = 0; x < nRX; x++)
            {
                int rX = rand.Next(7) + 6;
                int rY = rand.Next(7) + 6;
                int rS = rand.Next(3);
                rS = rS * 2;
                rooms[c].rX = rX + widthCell * x;
                rooms[c].rY = rY + heightCell * y;
                rooms[c].rS = rS;
                if (rS > 0) cS++;
                for (int x2 = rX + widthCell * x - rS; x2 <= rX + widthCell * x + rS; x2++)
                    for (int y2 = rY + heightCell * y - rS; y2 <= rY + heightCell * y + rS; y2++)
                        map[x2, y2] = 0;
                c++;
            }
        }
        if (cS < 2)
        {
            rooms[0].rS = 2;
            rooms[5].rS = 2;
        }

        //comencem els camins fins als límits de bloc

        c = 0;
        for (int y = 0; y < nRY; y++)
        {
            for (int x = 0; x < nRX; x++)
            {
                int dX;
                int dY;
                int tempX = rooms[c].rX;
                int tempY = rooms[c].rY;

                dX = widthCell * (x + 1) - tempX;
                dY = heightCell * (y + 1) - tempY;
                if (y != nRY - 1)
                    for (int i = tempY; i <= tempY + dY; i++)
                        map[tempX, i] = 0;
                if (x != nRX - 1)
                    for (int i = tempX; i <= tempX + dX; i++)
                        map[i, tempY] = 0;

                //std::cout << dX+tempX << "  "<< dY+tempY << "\n";


                dX = -widthCell * x + tempX;
                dY = -heightCell * y + tempY;


                //std::cout << tempX-dX << "  " << tempY-dY << "\n";

                //Obtenim els límits de "bloc" on es creuaran els camins
                //21 20 00 00    42 20 21 00    63 20 42 00    84 20 63 00
                //21 40 00 20    42 40 21 20    63 40 21 20    84 40 63 20
                //21 60 00 40    42 60 21 40    63 60 42 40    84 60 63 40

                if (y != 0)
                {

                    for (int i = tempY; i >= tempY - dY; i--)
                        map[tempX, i] = 0;
                }
                if (x != 0)
                {
                    for (int i = tempX; i >= tempX - dX; i--)
                        map[i, tempY] = 0;
                }
                c++;
            }
        }


        //Connectem els camins horitzontals
        for (int j = 0; j < heightCell * nRY; j += heightCell)
        {
            for (int i = 0; i < widthCell * nRX; i += widthCell)
            {
                int counter = 0;
                room[] tent = new room[3];
                tent[0].rX = 0;
                tent[0].rY = 0;
                tent[1].rX = 0;
                tent[1].rY = 0;
                for (int y = j; y < j + heightCell; y++)
                {
                    if (map[i, y] == 0 && counter < 2)
                    {
                        tent[counter].rX = i;
                        tent[counter].rY = y;
                        counter++;
                    }
                }
                if (counter == 2)
                {
                    for (int y = tent[0].rY; y < tent[1].rY; y++)
                    {
                        map[i, y] = 0;
                    }
                }

            }
        }
        //Connectem els camins verticals
        for (int j = 0; j < heightCell * nRY; j += heightCell)
        {
            for (int i = 0; i < widthCell * nRX; i += widthCell)
            {
                int counter = 0;
                room[] tent = new room[3];
                for (int x = i; x < i + widthCell; x++)
                {
                    if (map[x, j] == 0 && counter < 2)
                    {
                        tent[counter].rX = x;
                        tent[counter].rY = j;
                        counter++;
                    }
                }
                if (counter == 2)
                {
                    for (int x = tent[0].rX; x < tent[1].rX; x++)
                    {
                        map[x, j] = 0;
                    }
                }

            }
        }

        //posem les tiles al map

        Vector3Int currentCell = towerTiles.WorldToCell(transform.position);
        towerTiles.ClearAllTiles();
        bool flag = false;
        int start = 0;
        while (!flag)
        {
            start = rand.Next(numRooms);
            if (rooms[start].rS > 0) flag = true;
        }
        flag = false;
        int finish = 0;
        while (!flag)
        {
            finish = rand.Next(numRooms);
            if ((start != finish) &&(rooms[finish].rS > 0)) flag = true;
        }
        
        for (int y = -fillerH; y < height + fillerH; y++)
        {
            for (int x = -fillerW; x < width + fillerW; x++)
            {
                int nTil = rand.Next(tilesWall.Length);
                towerTiles.SetTile(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z), tilesWall[nTil]);
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x,y] == 0)
                {
                    if (x == rooms[finish].rX && y == rooms[finish].rY)
                    {
                        towerTiles.SetTile(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z), tileStairs);
                        goal.transform.position = new Vector3(0.25f, 0.25f, -1f) + towerTiles.CellToWorld(new Vector3Int(currentCell.x + x, currentCell.y + y, 0));
                    }
                    else
                    {
                        int nTil = rand.Next(tilesFloor.Length);
                        towerTiles.SetTile(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z), tilesFloor[nTil]);


                        int chestC = rand.Next(200);
                        if (chestC == 0) {
                            GameObject tempCh = Instantiate(chest, new Vector3(0.25f, 0.25f, -1f) + towerTiles.CellToWorld(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z)), Quaternion.identity) as GameObject;
                        }
                        else if (chestC == 1)
                        {
                            GameObject tempEn = Instantiate(enemy, new Vector3(0.25f, 0.25f, -1f) + towerTiles.CellToWorld(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z)), Quaternion.identity) as GameObject;
                        }
                        else if (chestC == 2)
                        {
                            GameObject tempEn = Instantiate(enemy2, new Vector3(0.25f, 0.25f, -1f) + towerTiles.CellToWorld(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z)), Quaternion.identity) as GameObject;
                        }
                    };
                    if(x==rooms[start].rX && y==rooms[start].rY)
                    {
                        player.transform.position = new Vector3(0.25f,0.25f,-1f)+towerTiles.CellToWorld(new Vector3Int(currentCell.x + x, currentCell.y + y, 0));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
