using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattlePanelManager 
{
    private List<GameObject> _enemys = new List<GameObject>();
    private int _maxCost;
    private int _currentCost;
    private int _turnNum;
    private List<GameObject> _unusedDeck = new List<GameObject>();
    private List<GameObject> _handDeck = new List<GameObject>();
    private List<GameObject> _usedDeck = new List<GameObject>();
    private List<GameObject> _excludedDeck = new List<GameObject>();
    private int _handCardLimit = 8;

    public void Init() // ��Ʋ ��ƾ �ʱ�ȭ
    {
        GameManager.instance.IsAnim = false;
        GameManager.instance.IsClick = false;
        GameManager.instance.CardState = DataBase.CardState.HandCard;
        _unusedDeck.Clear();
        _handDeck.Clear();
        _usedDeck.Clear();
        _excludedDeck.Clear();
        _enemys.Clear();
        _turnNum = 0;
    }

    public void InitDeck(GameObject card)
    {
        _unusedDeck.Add(card);
    }

    public void InitEnemyPhase()
    {
        GameManager.instance.IsAnim = true;
        int n = _enemys.Count;
        for(int i = 0; i < n; i++)
        {
            _enemys[i].GetComponent<EnemyController>().SetNewTurn();
        }
    }
    public void AddEnemy(GameObject enemy)
    {
        _enemys.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemys.Remove(enemy);
    }

    public bool IsBattleEnd()
    {
        if (_enemys.Count == 0) return true;
        else return false;
    }

    public void SetCost()
    {
        _maxCost = GetCost();
        _currentCost = _maxCost;
        EventManager.SetCost(_currentCost, _maxCost);
    }

    private int GetCost() // �̹� �� �ڽ�Ʈ ����
    {
        return 3;
    }
    private void UseCost(int value)
    {
        _currentCost -= value;
        EventManager.SetCost(_currentCost, _maxCost);
    }

    public void Reset()
    {
        _unusedDeck = _usedDeck.ToList();
        _usedDeck.Clear();
        _unusedDeck = _unusedDeck.OrderBy(x => x.GetComponent<CardController>().GetData()).ToList();
    }


    public void DrawCard() // ī��̱�
    {
        var n = _unusedDeck.Count;
        if (n <= 0)
        {
            if (_usedDeck.Count <= 0)
            {
                // unusedcard�� 0 ���ϸ� �� �̴´ٴ� �޼��� �ʿ�!
            }
            else
            {
                Reset();
            }

        }
        if (_handDeck.Count > _handCardLimit)
        {
            //���� �ѵ� ������ ���⿡ ������ ��

        }
        else
        {
            var i = Random.Range(0, n);
            var card = _unusedDeck[i];
            card.SetActive(true);
            card.transform.SetAsFirstSibling();
            _handDeck.Add(card); 
            _unusedDeck.Remove(card);
            EventManager.CallOnHandCardList(_handDeck);
            
            
        }


    }

    public int GetDrawCardNum()
    {
        if (_turnNum == 0)
        {
            return 3;
        }
        return 1;
    }

    public int GetEnemyNum()
    {
        return _enemys.Count;
    }

    public float EnemyPhase(int data, GameObject player)
    {
        float time = _enemys[data].GetComponent<EnemyController>().GetEnemyPattern(_turnNum, player);
        return time;
    }

    public void EndEnemyPhase()
    {
        GameManager.instance.IsAnim = false;
        _turnNum++;
    }

    public List<int> GetUnusedDeck()
    {
        List<int> list = new List<int>();
        foreach (var i in _unusedDeck)
        {
            list.Add(i.GetComponent<CardController>().GetData());
        }
        return list;
    }

    public void UseTargetingCard(GameObject card, GameObject player, GameObject enemy)
    {
        int id = card.GetComponent<CardController>().GetData();
        int cost = card.GetComponent<CardController>().GetCost();
        if (_currentCost < cost)
        {
            card.GetComponent<CardController>().FailToUse();
            Debug.Log("�ڽ�Ʈ�� �����մϴ�");
            return;
        }
        UseCost(cost);
        switch (id)
        {
            case 0:
                enemy.GetComponent<EnemyController>().GetDamage(DataBase.CardList[id].Damage);
                break;
        }
        UseHandCard(card);
        GameManager.instance.EmptyObj();
    }

    public void UseCard(GameObject card, GameObject player)
    {
        int id = card.GetComponent<CardController>().GetData();
        int cost = card.GetComponent<CardController>().GetCost();
        if (_currentCost < cost)
        {
            card.GetComponent<CardController>().FailToUse();
            return;
        }
        UseCost(cost);
        switch (id)
        {
            case 1:
                player.GetComponent<PlayerController>().GetDefence(DataBase.CardList[id].Defense);
                break;
        }
        UseHandCard(card);
        EventManager.CallOnHandCardList(_handDeck);
        GameManager.instance.EmptyObj();

    }

    private void UseHandCard(GameObject card)
    {
        _handDeck.Remove(card);
        _usedDeck.Add(card);
        card.SetActive(false);
        EventManager.CallOnHandCardList(_handDeck);
    }

    public List<Vector2Int> GetPrize()
    {
        List<Vector2Int> prize = new List<Vector2Int>();
        prize.Add(new Vector2Int(0, 50));
        prize.Add(new Vector2Int(0, 20));
        prize.Add(new Vector2Int(1, 0));
        prize.Add(new Vector2Int(0, 30));
        return prize;
    }

}
