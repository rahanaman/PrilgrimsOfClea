using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    public int CardId;
    public int CardCost;
    public string CardName;
    public string CardDesc;
    public bool CardIsSub;
    public string CardSubDesc;
    public List<Vector2Int> CardPatternData = new List<Vector2Int>();//ȿ�� id, ���� ��
    public int Damage;
    public int Defense;
    public DataBase.ObjType Cardtype;
    
}
