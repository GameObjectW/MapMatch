using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class InitBoard : MonoBehaviour
{
    private const int MAXROW = 7;
    private const int MAXCOL = 7;
    private const int CELLSIZE = 78;

    //二维数组映射地图上底板铺设数据
    public int[,] map = new int[MAXROW, MAXCOL];
    //二维数组映射地图上每个单元
    public CellInfo[,] cellMap = new CellInfo[MAXROW, MAXCOL];

    public static InitBoard Ins;

    private void Awake()
    {
        Ins = this;
    }

    /// <summary>
    /// 初始化面板
    /// </summary>
    private void Start()
    {
        GameObject bg1 = Resources.Load<GameObject>("Effect/BG1");
        GameObject bg2 = Resources.Load<GameObject>("Effect/BG2");
        float offset = CELLSIZE * 0.01f;
        int count = 0;
        for (int i = 0; i < MAXROW; i++)
        {
            for (int j = 0; j < MAXCOL; j++)
            {
                map[i, j] = 0;
                GameObject obj = Instantiate(count % 2 == 0 ? bg1 : bg2, new Vector3(j * offset, i * offset, 0), Quaternion.identity, transform);
                cellMap[i, j] = obj.GetComponent<CellInfo>();
                cellMap[i, j].SetPos(i, j);
                count++;
            }
        }
    }

    /// <summary>
    /// 获取指定位置底板的周围8个单元
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public List<CellInfo> GetNeighbour(int row, int col)
    {
        List<CellInfo> neighbourList = new List<CellInfo>();
        if ((row + 1) >= MAXROW || (col - 1) < 0)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row + 1, col - 1]);
        }

        if ((row + 1) >= MAXROW)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row + 1, col]);
        }

        if ((row + 1) >= MAXROW || (col + 1) >= MAXCOL)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row + 1, col + 1]);
        }

        if ((col + 1) >= MAXCOL)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row, col + 1]);
        }

        if ((row - 1) < 0 || (col + 1) >= MAXCOL)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row - 1, col + 1]);
        }

        if ((row - 1) < 0)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row - 1, col]);
        }

        if ((row - 1) < 0 || (col - 1) < 0)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row - 1, col - 1]);
        }

        if ((col - 1) < 0)
        {
            neighbourList.Add(null);
        }
        else
        {
            neighbourList.Add(cellMap[row, col - 1]);
        }

        return neighbourList;
    }

    /// <summary>
    /// 获取指定位置底板周围八个单元的铺设情况（11111111反应周围全部有铺设）
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    public string GetNeighbourState(int row, int col)
    {
        List<CellInfo> neightbourList = GetNeighbour(row, col);
        string neighbours = "";
        foreach (var VARIABLE in neightbourList)
        {
            if (VARIABLE == null || map[VARIABLE.GetPos().x, VARIABLE.GetPos().y] == 0)
            {
                neighbours += 0;
            }
            else
            {
                neighbours += 1;
            }
        }
        return neighbours;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Assert(Camera.main != null, "Camera.main != null");
            Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            CellInfo cellInfo = hit != null ? hit.gameObject.GetComponent<CellInfo>() : null;
            if (cellInfo != null)
            {
                Vector2Int pos = cellInfo.GetPos();
                map[pos.x, pos.y] = (map[pos.x, pos.y] + 1) % 2;                //修改铺设状态，每次点击都是状态取反
                cellInfo.UpdateState(map[pos.x, pos.y]);
            }
        }
    }
}