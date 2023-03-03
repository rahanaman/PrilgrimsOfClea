using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    CardManager _cardManager = new CardManager();
    [SerializeField]private GameObject _efx;
    private int _index;

    public void SetCard(CardData data)
    {
        _cardManager.SetCardData(data);
    }

    private void OnMouseEnter()
    {
        if (!GameManager.instance.IsAnim&&!GameManager.instance.IsClick && GameManager.instance.CardState == DataBase.CardState.HandCard)
        {
            Vector3 chpos = _cardManager.Pos;
            chpos.y = -400;
            
            transform.localPosition = chpos;
            transform.rotation = Quaternion.identity;
            transform.SetAsLastSibling();
            _efx.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.instance.IsAnim&&!GameManager.instance.IsClick && GameManager.instance.CardState == DataBase.CardState.HandCard)
        {
            transform.localPosition = _cardManager.Pos;
            transform.rotation = _cardManager.Rot;
            _efx.SetActive(false);
            transform.SetSiblingIndex(_index);
            //_cardCon.transform.localScale = _scale;
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.IsAnim&&GameManager.instance.CardState == DataBase.CardState.HandCard)
        {
            if (!GameManager.instance.IsClick)
            {
                GameManager.instance.IsClick = true;
                transform.rotation = Quaternion.identity;
                GameManager.instance.SetOnClickObj(gameObject,_cardManager.CardData.Cardtype);
                GameManager.instance.SetCursorCard();
            }
            else if (GameManager.instance.OnClickObj == gameObject)
            {
                GameManager.instance.IsClick = false;
                transform.localPosition = _cardManager.Pos;
                transform.rotation = _cardManager.Rot;
                _efx.SetActive(false);
                transform.SetSiblingIndex(_index);
                GameManager.instance.SetCursorIdle();
            }
        }

        if(GameManager.instance.CardState == DataBase.CardState.PrizeCard)
        {
            DataManager.PlusPlayerDeck(_cardManager.CardData.CardId);
            EventManager.CallOnClosePrizeCard(true);
        }
        
    }

    public void SetPosRot()
    {
        _cardManager.Pos = transform.localPosition;
        _cardManager.Rot = transform.rotation;
        
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public int GetData()
    {
        return _cardManager.CardData.CardId;
    }

    public int GetCost()
    {
        return _cardManager.GetCost();
    }


    public void FailToUse()
    {
        GameManager.instance.IsClick = false;
        transform.localPosition = _cardManager.Pos;
        transform.rotation = _cardManager.Rot;
        _efx.SetActive(false);
        transform.SetSiblingIndex(_index);
        GameManager.instance.SetCursorIdle();
    }



}
