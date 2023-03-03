using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSceneManager
{
    public List<int> SetStateData(DataBase.State state) // 상황에 맞춰서 연산
    {
        switch (state)
        {
            case DataBase.State.Selection:
                return new List<int> { 2, 0 };
            case DataBase.State.Battle:
                return new List<int> { 0 }; //더미용 연산
        }
        return new List<int> { 0 };
    }


}
