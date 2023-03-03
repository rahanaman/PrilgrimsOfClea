using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{ 


    private CardData _cardData;
    public CardData CardData
    {
        get { return _cardData; }
    }


    public Vector3 Pos;

    public Quaternion Rot;

    public void SetCardData(CardData data)
    {
        _cardData = data;
    }

    public int GetCost()
    {
        return _cardData.CardCost;
    }
}