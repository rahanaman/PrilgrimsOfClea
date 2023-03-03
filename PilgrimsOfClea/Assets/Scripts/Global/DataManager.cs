using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static bool _isStart;
    public static bool IsStart
    {
        get { return _isStart; }
    }

    private static int _playerCurrentHP;
    public static int PlayerCurrentHP
    {
        get { return _playerCurrentHP; }
        set { _playerCurrentHP = value; }
    }

    private static int _playerID;
    public static int PlayerID
    {
        get { return _playerID; }
        set { _playerID = value; }
    }

    private static List<int> _playerDeck = new List<int>();
    public static List<int> PlayerDeck
    {
        get { return _playerDeck; }
    }

    private static int _gameTurn;
    public static int GameTurn
    {
        get { return _gameTurn; }
    }

    private static int _battleNum;
    public static int BattleNum
    {
        get { return _battleNum; }
    }

    private static DataBase.State _currentState;

    public static DataBase.State CurrentState
    {
        get { return _currentState; }
    }

    private static List<int> _currentStateData = new List<int>();

    public static List<int> CurrentStateData
    {
        get { return _currentStateData; }
    }

    private static int _mana;
    public static int Mana
    {
        get { return _mana; }
    }


    public static void SaveCurrentState(DataBase.State state, List<int> data)
    {
        _currentState = state;
        _currentStateData = data;
    }
    
    public static void SetSelctionData(int value)
    {
        PlayerID = value - 1; //CGID - 1 = PlayerID
        Init();
    }

    public static void PlusPlayerDeck(int value)
    {
        PlayerDeck.Add(value);
        PlayerDeck.Sort();
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
    }

    private static void Init()
    {
        DataManager.PlayerCurrentHP = DataBase.PlayerMaxHP[DataManager.PlayerID];
        _playerDeck = new List<int>();
        _currentState = DataBase.State.Selection;
        _gameTurn = 0;
        _battleNum = 0;
        _isStart = true;
        _mana = 100;
        List<int> data = new List<int>() { 2, 0 };
        SaveCurrentState(DataBase.State.Selection, data);
    }

    public static void AddTurn()
    {
        _gameTurn++;
    }

    public static void LoadDebug()
    {
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(0);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
        DataManager.PlusPlayerDeck(1);
    }

    public static void PlusMana(int value)
    {
        _mana += value;
        EventManager.CallOnMana(DataManager.Mana);
    }

    public static void MinusMana(int value)
    {
        _mana -= value;
        EventManager.CallOnMana(DataManager.Mana);
    }



}
