using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanelController : MonoBehaviour
{//
    [SerializeField] GameObject _battlePanel;
    [SerializeField] GameObject _handCardPanel;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _cardPanel;
    [SerializeField] Button _turnEndButton;
    [SerializeField] Button _unusedDeckButton;
    [SerializeField] Button _usedDeckButton;
    [SerializeField] GameObject _사망Panel;
    [SerializeField] GameObject _승리Panel;
    private List<GameObject> _cards = new List<GameObject>();
    BattlePanelManager _battlePanelManager = new BattlePanelManager();
    private GameSceneManager _gameSceneManager = new GameSceneManager();
    private bool _isPlayerTurnEnd;
    IEnumerator _routine;


    private void OnEnable()
    {
        _cards.Clear();
        _turnEndButton.onClick.AddListener(OnClickTurnEndButton);
        EventManager.UseObj += UseObj;
        EventManager.SetCheckBattle += CheckBattle;
        EventManager.SetEndBattle += EndPanel;
        _routine = BattleRoutine();
        StartCoroutine(_routine);
    }

    private void OnDisable()
    {
        EventManager.UseObj -= UseObj;
        EventManager.SetCheckBattle -= CheckBattle;
        EventManager.SetEndBattle -= EndPanel;
    }
    private IEnumerator BattleRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        WaitUntil playerphase = new WaitUntil(() => _isPlayerTurnEnd);
        Init(); // 배틀루틴 초기화
        var encounterList = DataBase.EnemyEncounter[DataManager.CurrentStateData[0]];
        var n = encounterList.Count;
        for (int i = 0; i < n; i++)
        {
            var enemy = gameObject.GetComponent<BattlePanelView>().AddEnemy(encounterList[i], DataBase.EnemyPosition[n][i]);
            _battlePanelManager.AddEnemy(enemy);

            
        }
        //적 세팅
        yield return new WaitForEndOfFrame(); // 있어야 다른 함수들 콜백 걸리는 거 오류 안 나도ㅓ라...
        while (true)
        {
            _battlePanelManager.SetCost();
            InitPlayerTurn();            
            int num = _battlePanelManager.GetDrawCardNum();
            for (int i = 0; i < num; i++)
            {
                _battlePanelManager.DrawCard();
                yield return delay;
            }
            yield return playerphase;
            _isPlayerTurnEnd = false;
            
            InitEnemyPhase();
            int enemynum = _battlePanelManager.GetEnemyNum();
            for (int i = 0; i < enemynum; i++)
            {
                var time = _battlePanelManager.EnemyPhase(i, _player);
                yield return new WaitForSeconds(time);
            }
            _battlePanelManager.EndEnemyPhase();

        }

        


    }

    private void OnClickTurnEndButton()
    {
        _isPlayerTurnEnd = true;
    }

    private void Init()
    {
        _battlePanelManager.Init();
        int n = DataManager.PlayerDeck.Count;
        for (int i = 0; i < n; i++)
        {
            GameObject card = Instantiate(DataLoader.CardPref[DataManager.PlayerDeck[i]], _handCardPanel.transform);
            card.GetComponent<CardController>().SetCard(DataBase.CardList[DataManager.PlayerDeck[i]]);
            _cards.Add(card);
            _battlePanelManager.InitDeck(card);
        }
    }

    private void InitPlayerTurn()
    {
        _player.GetComponent<PlayerController>().SetNewTurn();
    }

    private void InitEnemyPhase()
    {
        _battlePanelManager.InitEnemyPhase();
    }

    private void OnClickUnusedDeckButton()
    {
        _cardPanel.SetActive(true);
        GameManager.instance.PostCardState = GameManager.instance.CardState;
        GameManager.instance.CardState = DataBase.CardState.CardPanel;
        List<int> list = _battlePanelManager.GetUnusedDeck();
        EventManager.CallOnCardList(list);
    }

    private void UseObj(GameObject enemy = null)
    {
        switch (GameManager.instance.OnClickType)
        {

            case DataBase.ObjType.TargetingCard:
                _battlePanelManager.UseTargetingCard(GameManager.instance.OnClickObj, _player, enemy);


                break;
            case DataBase.ObjType.Card:

                _battlePanelManager.UseCard(GameManager.instance.OnClickObj, _player);

                break;

        }
    }

    private void End()
    {
        GameManager.instance.CardState = DataBase.CardState.CardPanel;
        ResetCard();
        if (!_player.GetComponent<PlayerController>().IsAlive()) // 플레이어 죽음
        {
            _사망Panel.SetActive(true);
        }
        else
        {
            _승리Panel.SetActive(true);
            _승리Panel.GetComponent<승리PanelController>().SetPrize(_battlePanelManager.GetPrize());
        }
    }

    private void ResetCard()
    {
        int n = _cards.Count;
        for(int i =0; i< n; i++)
        {
            Destroy(_cards[i]);
        }
    }
    private void CheckBattle(GameObject enemy)
    {
        if (enemy == _player)
        {
            StopCoroutine(_routine);
            End();
        }
        else
        {

            _battlePanelManager.RemoveEnemy(enemy);
            if (_battlePanelManager.IsBattleEnd())
            {
                StopCoroutine(_routine);
                End();
            }
        }

    }

    public void EndPanel()
    {
        _player.GetComponent<PlayerController>().SetNewTurn(); //방어도 리셋을 위해
        List<int> stateData = _gameSceneManager.SetStateData(DataBase.State.Selection);
        DataManager.SaveCurrentState(DataBase.State.Selection, stateData);
        DataManager.AddTurn();
        EventManager.CallOnFade();
        gameObject.SetActive(false);
    }


}
