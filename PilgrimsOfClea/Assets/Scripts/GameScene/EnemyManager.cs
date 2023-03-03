using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager 
{
    
    private int _maxHP;
    public int MaxHP
    {
        get { return _maxHP; }
    }
    private int _currentHP;
    public int CurrentHP
    {
        get { return _currentHP;}
    }
    private int _enemyId;
    private List<DataBase.EnemyPattern> _enemyPatterns;
    int _enemyPatternNum = 0;
    private int _defence;
    public int Defence
    {
        get { return _defence; }
    }
    public int EnemyId
    {
        get { return _enemyId; }
    }

    private EnemyData _enemyData;
    public EnemyData EnemyData
    {
        get { return _enemyData; }
    }
    public void SetEnemyData(EnemyData data)
    {
        _currentHP = data.EnemyHP;
        _maxHP = data.EnemyHP;
        _enemyId = data.EnemyId;
        _enemyPatterns = data.EnemyPatternList.ToList();
        _enemyPatternNum = _enemyPatterns.Count;
        _enemyData = data;
        ResetDefence();
    }
    public DataBase.EnemyPattern GetEnemyPattern(int TurnNum)
    {

        
        int num = 0;

        if(TurnNum > 0)
        {

            num = TurnNum % _enemyPatternNum;
        }
            


        return _enemyPatterns[num];

    }

    private int GetCalculatedDamage(int damage)
    {
        return damage;
    }

    private int GetCalculatedDefence(int defence)
    {
        return defence;
    }
    public void GetDefence(int defence)
    {
        int calaulatedDefence = GetCalculatedDefence(defence);
        _defence += calaulatedDefence;
    }
    public void GetDamage(int damage)
    {
        int calculatedDamage = GetCalculatedDamage(damage);
        if (_defence > 0)
        {
            if (_defence > calculatedDamage)
            {
                _defence -= calculatedDamage;
                calculatedDamage = 0;
            }
            else
            {
                calculatedDamage -= _defence;
                ResetDefence();
            }
        }

        _currentHP -= calculatedDamage;
    }

    public void ResetDefence()
    {
        _defence = 0;
    }

    public bool IsAlive()
    {
        if (_currentHP <= 0) { return false; }
        return true;
    }
}
