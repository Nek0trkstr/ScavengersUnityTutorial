using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count 
    {
        public int m_Min;
        public int m_Max;

        public Count (int i_Min, int i_Max)
        {
            m_Min = i_Min;
            m_Max = i_Max;
        }
    }

    public int m_Columns = 8;
    public int m_Rows = 8;
    public Count m_WallCount = new Count(5, 8);
    public Count m_FoodCount = new Count(1, 5);
    public GameObject m_Exit;
    public GameObject[] m_FloorTiles;
    public GameObject[] m_WallTiles;
    public GameObject[] m_FoodTiles;
    public GameObject[] m_EnemyTiles;
    public GameObject[] m_OuterWallsTiles;

    private Transform m_BoardHolder;
    private List<Vector3> m_GridPosition = new List<Vector3>();

    private void InitializeList()
    {
        m_GridPosition.Clear();
        for(int x = 1; x < m_Columns - 1; x++)
        {
            for( int y = 1; y < m_Rows - 1; y++)
            {
                m_GridPosition.Add(new Vector3(x, y, 0f));
            }
        }
    }

    private void BoardSetup()
    {
        m_BoardHolder = new GameObject("Board").transform;
        for (int x = -1; x < m_Columns + 1; x++)
        {
            for (int y = -1; y < m_Rows +1; y++)
            {
                GameObject toInstantiate = m_FloorTiles[Random.Range(0, m_FloorTiles.Length)];
                if (x == -1 || x == m_Columns || y == -1 || y == m_Rows)
                {
                    toInstantiate = m_OuterWallsTiles[Random.Range(0, m_OuterWallsTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(m_BoardHolder);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, m_GridPosition.Count);
        Vector3 randomPosition = m_GridPosition[randomIndex];
        m_GridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] i_TileArray, int i_Minimum, int i_Maximum)
    {
        int objectCount = Random.Range(i_Minimum, i_Maximum);
        for(int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject tileChoice = i_TileArray[Random.Range(0, i_TileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int i_Level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(m_WallTiles, m_WallCount.m_Min, m_WallCount.m_Max);
        LayoutObjectAtRandom(m_FoodTiles, m_FoodCount.m_Min, m_FoodCount.m_Max);
        int enemyCount = (int)Math.Log(i_Level, 2f);
        LayoutObjectAtRandom(m_EnemyTiles, enemyCount, enemyCount);
        Instantiate(m_Exit, new Vector3(m_Columns - 1, m_Rows - 1, 0f), Quaternion.identity);
    }
}
