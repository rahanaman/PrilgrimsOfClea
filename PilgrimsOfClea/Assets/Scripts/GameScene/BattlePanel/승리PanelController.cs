using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class 승리PanelController : MonoBehaviour
{
    // 0 마나 1 카드 2 유물
    // 마나 - 마나 숫자, 카드 - 카드 등급, 유물 - 유물 id
    [SerializeField] Button _Button;
    [SerializeField] GameObject _content;
    [SerializeField] GameObject _cardPrizePanel;
    [SerializeField] GameObject _prizeButton;
    GameObject _cardButton;
    
    List<GameObject> _prizeList;
    void OnEnable()
    {
        _Button.onClick.AddListener(OnClickButton);
        EventManager.ClosePrizeCard += ClosePrizeCard;
    }

    private void OnDisable()
    {
        _Button.onClick.RemoveAllListeners();
        EventManager.ClosePrizeCard -= ClosePrizeCard;
    }

    private void OnClickButton()
    {
        EventManager.CallOnEndBattle();
        gameObject.SetActive(false);
    }

    public void SetPrize(List<Vector2Int> prize)
    {
        _prizeList = new List<GameObject>();
        int num = prize.Count;
        _content.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 120 * num + 40);
        
        for(int i = 0; i < num; i++)
        {
            int number = i;
            GameObject prizebutton = Instantiate(_prizeButton, _content.transform);
            prizebutton.SetActive(true);
            _prizeList.Add(prizebutton);
            prizebutton.GetComponent<PrizeButtonController>().SetText(prize[number].x.ToString() + ' '+ prize[number].y.ToString());
            prizebutton.GetComponent<Button>().onClick.AddListener(() => OnClickPrize(number, prizebutton));
        }
        SetPrizePlace();

        void OnClickPrize(int number, GameObject button)
        {
            var p = prize[number];
            switch (p.x)
            {
                case 0: //마나
                    DataManager.PlusMana(p.y);
                    DestroyPrizeButton(button);
                    SetPrizePlace();
                    break;
                case 1: //카드
                    _cardButton=button;
                    GameManager.instance.CardState = DataBase.CardState.PrizeCard;
                    int num = DataManager.PlayerID * 48 + p.y * 12; // 플레이어 id, 등급에 따른 id 범위
                    List<int> card = new List<int>();
                    for(int i = 0; i < 12; i++)
                    {
                        card.Add(i+num);
                    }
                    card.OrderBy(g => Guid.NewGuid());
                    card = card.Take(3).ToList();
                    _cardPrizePanel.SetActive(true);
                    _cardPrizePanel.GetComponent<CardPrizePanelController>().SetPrizePanel(card);
                    break;
            }
            
            
        }
    }

    
    private void DestroyPrizeButton(GameObject button)
    {
        _prizeList.Remove(button);
        Destroy(button);
    }


    private void SetPrizePlace()
    {
        int num = _prizeList.Count;
        Vector2 position = new Vector2(300, -80);
        for (int i = 0; i < num; i++)
        {
            
            _prizeList[i].transform.localPosition = position;
            position.y -= 120;
        }
    }
    
    private void ClosePrizeCard(bool IsClick)
    {
        if (IsClick)
        {
            GameManager.instance.CardState = DataBase.CardState.CardPanel;
            DestroyPrizeButton(_cardButton);
            SetPrizePlace();
            _cardPrizePanel.SetActive(false);
        }
        else
        {
            _cardButton = null;
            _cardPrizePanel.SetActive(false);
            GameManager.instance.CardState = DataBase.CardState.CardPanel;
        }
    }


}


