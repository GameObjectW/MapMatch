using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    [SerializeField] private int _row;
    [SerializeField] private int _col;
    [SerializeField] private StandByDiBanItem _diBan;

    public void SetPos(int row, int col)
    {
        _row = row;
        _col = col;
    }

    public Vector2Int GetPos()
    {
        return new Vector2Int(_row, _col);
    }

    /// <summary>
    /// 根据铺设状态来改变自身显示效果
    /// </summary>
    /// <param name="isNull"></param>
    public void UpdateState(int isNull)
    {
        if (_diBan != null)
        {
            GameObject.Destroy(_diBan.gameObject);
        }

        if (isNull == 1)
        {
            _diBan = Instantiate(Resources.Load<GameObject>("Effect/StandbyDiBan"), transform,false).GetComponent<StandByDiBanItem>();
            _diBan.ActiveDiBan(_row, _col);
        }

        //自身状态更新完毕在之后，再将周围八个单元的状态更新一遍
        foreach (var VARIABLE in InitBoard.Ins.GetNeighbour(_row, _col))
        {
            if (VARIABLE != null && VARIABLE._diBan != null)
            {
                VARIABLE._diBan.ActiveDiBan(VARIABLE.GetPos().x, VARIABLE.GetPos().y);
            }
        }
    }
}