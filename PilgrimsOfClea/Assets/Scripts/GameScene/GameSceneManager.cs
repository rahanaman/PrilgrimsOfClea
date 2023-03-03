using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSceneManager
{
    public List<int> SetStateData(DataBase.State state) // ��Ȳ�� ���缭 ����
    {
        switch (state)
        {
            case DataBase.State.Selection:
                return new List<int> { 2, 0 };
            case DataBase.State.Battle:
                return new List<int> { 0 }; //���̿� ����
        }
        return new List<int> { 0 };
    }


}
