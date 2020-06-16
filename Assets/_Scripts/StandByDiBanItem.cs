using System.Text;
using UnityEngine;

public enum Direction
{
    LeftUp,
    RightUp,
    RightDown,
    LeftDown
}

public class StandByDiBanItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trans1;
    public Transform trans2;
    public Transform trans3;
    public Transform trans4;

    private Transform _first;
    private Transform _second;
    private Transform _third;
    private Transform _forth;

    /// <summary>
    /// 更新当前单元的自身显示效果，一个单元会按照4个方向分成4个子单元分别处理
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void ActiveDiBan(int row, int col)
    {
        string state = InitBoard.Ins.GetNeighbourState(row,col);    //获取周围铺设状态
        if (_first != null)
        {
            GameObject.Destroy(_first.gameObject);
        }

        if (_second != null)
        {
            GameObject.Destroy(_second.gameObject);
        }

        if (_third != null)
        {
            GameObject.Destroy(_third.gameObject);
        }

        if (_forth != null)
        {
            GameObject.Destroy(_forth.gameObject);
        }

        StringBuilder stringBuilder = new StringBuilder();

        string index1 = stringBuilder.Append(state[7]).Append(state[0]).Append(state[1]).ToString();
        stringBuilder.Clear();
        string index2 = stringBuilder.Append(state[1]).Append(state[2]).Append(state[3]).ToString();
        stringBuilder.Clear();
        string index3 = stringBuilder.Append(state[3]).Append(state[4]).Append(state[5]).ToString();
        stringBuilder.Clear();
        string index4 = stringBuilder.Append(state[5]).Append(state[6]).Append(state[7]).ToString();
        string PrefabName1 = Cal(index1, Direction.LeftUp);
        string PrefabName2 = Cal(index2, Direction.RightUp);
        string PrefabName3 = Cal(index3, Direction.RightDown);
        string PrefabName4 = Cal(index4, Direction.LeftDown);

        if (PrefabName1 != "")
        {
            _first = GameObject.Instantiate(Resources.Load("Prefabs/" + PrefabName1 + "_DB") as GameObject).transform;
            _first.SetParent(trans1);
            _first.localScale = Vector3.one;
            _first.localPosition = Vector3.zero;
        }

        if (PrefabName2 != "")
        {
            _second = GameObject.Instantiate(Resources.Load("Prefabs/" + PrefabName2 + "_DB") as GameObject).transform;
            _second.SetParent(trans2);
            _second.localScale = Vector3.one;
            _second.localPosition = Vector3.zero;
        }

        if (PrefabName3 != "")
        {
            _third = GameObject.Instantiate(Resources.Load("Prefabs/" + PrefabName3 + "_DB") as GameObject).transform;
            _third.SetParent(trans3);
            _third.localScale = Vector3.one;
            _third.localPosition = Vector3.zero;
        }

        if (PrefabName4 != "")
        {
            _forth = GameObject.Instantiate(Resources.Load("Prefabs/" + PrefabName4 + "_DB") as GameObject).transform;
            _forth.SetParent(trans4);
            _forth.localScale = Vector3.one;
            _forth.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// 根据自身周围铺设情况，选择自己的显示效果，单位为一个单元的1/4
    /// </summary>
    /// <param name="index">邻近单元的铺设状态</param>
    /// <param name="dir">当前处理的是该单元哪个方向的子单元</param>
    /// <returns></returns>
    string Cal(string index, Direction dir)
    {
        if (index == "111")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "1";
                case Direction.RightUp:
                    return "2";
                case Direction.RightDown:
                    return "3";
                case Direction.LeftDown:
                    return "4";
            }

            return "";
        }
        else if (index == "101")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "b1";
                case Direction.RightUp:
                    return "b2";
                case Direction.RightDown:
                    return "b3";
                case Direction.LeftDown:
                    return "b4";
            }
        }
        else if (index == "000")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "a1";
                case Direction.RightUp:
                    return "a2";
                case Direction.RightDown:
                    return "a3";
                case Direction.LeftDown:
                    return "a4";
            }
        }
        else if (index == "100")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "c1";
                case Direction.RightUp:
                    return "c2";
                case Direction.RightDown:
                    return "c3";
                case Direction.LeftDown:
                    return "c4";
            }
        }
        else if (index == "001")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "d1";
                case Direction.RightUp:
                    return "d2";
                case Direction.RightDown:
                    return "d3";
                case Direction.LeftDown:
                    return "d4";
            }
        }
        else if (index == "010")
        {
            switch (dir)
            {
                case Direction.LeftUp:
                    return "e1";
                case Direction.RightUp:
                    return "e2";
                case Direction.RightDown:
                    return "e3";
                case Direction.LeftDown:
                    return "e4";
            }
        }

        return "";
    }
}