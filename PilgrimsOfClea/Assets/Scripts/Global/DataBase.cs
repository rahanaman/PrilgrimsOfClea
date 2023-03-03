using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataBase
{

    public static int[] PlayerMaxHP = new int[]{ 10, 101, 102 };

    public enum SoundID
    {
        MainSceneBgm,
        ApolloSelection,
        ScyllaSelection,
        DianaSelection,
        UISound1
    };

    public enum EnemyPattern
    {
        Attack, // 0
        Defence, //1
        AttackAndDefence //2,3
    };

    public enum ObjType //짝수는 타겟팅
    {
        None,
        Card,
        TargetingCard
        
    };
    public enum State
    {
        None,
        Selection,
        Battle,
        Store
    };
    public enum CardState
    {
        CardPanel,
        HandCard,
        Store,
        PrizeCard
    };


    private static Sprite[] _startSceneBackgroundCG = new Sprite[] { };
    public static Sprite[] StartSceneBackgroundCG
    {
        get { return _startSceneBackgroundCG; }
        set { _startSceneBackgroundCG = value; }
    }
    // 아폴로 0 스킬라 1 디아나 2

    private static Sprite[] _cardIcon = new Sprite[] { };
    public static Sprite[] CardIcon
    {
        get { return _cardIcon; }
        set { _cardIcon = value; }
    }

    private static Sprite[] _enemySprite = new Sprite[] { };
    public static Sprite[] EnemySprite
    {
        get { return _enemySprite; }
        set { _enemySprite = value; }
    }

    private static Sprite[] _selectionSprite = new Sprite[] { };
    public static Sprite[] SelectionSprite
    {
        get { return _selectionSprite; }
        set { _selectionSprite = value; }
    }

    public static List<CardData> CardList = new List<CardData>();


    public static List<EnemyData> EnemyIndexList = new List<EnemyData>();
    public static List<List<int>> EnemyEncounter = new List<List<int>>(); // 몬스터 조합 
    public static List<List<Vector2>> EnemyPosition = new List<List<Vector2>>();


    public static List<List<State>> SelectionList = new List<List<State>>();

    
    public static List<int> SetStateData(DataBase.State state) // 상황에 맞춰서 연산
    {
        switch (state)
        {
            case DataBase.State.Selection:
                return new List<int> { 2,0 };
            case DataBase.State.Battle:
                return new List<int> { 0 }; //더미용 연산
        }
        return new List<int> { 0 };
    }



}
