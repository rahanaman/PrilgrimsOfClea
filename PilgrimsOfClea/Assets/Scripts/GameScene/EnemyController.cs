using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyManager _enemyManager = new EnemyManager();
    [SerializeField] GameObject _efx;
    [SerializeField] GameObject _defence;
    private int atk;
    private int def;
    private EnemyView _enemyView;

    public void SetEnemy(EnemyData data)
    {
        _enemyManager.SetEnemyData(data);
        _enemyView = GetComponent<EnemyView>();
        _enemyView.SetEnemyDefence(_enemyManager.Defence);
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.CardState == DataBase.CardState.HandCard)
        {
            _efx.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.instance.CardState == DataBase.CardState.HandCard)
        {
            _efx.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.IsAnim&&GameManager.instance.CardState == DataBase.CardState.HandCard && GameManager.instance.IsClick)
        {
            ///카드일 경우 여기에 코스트 조건 추가해야함
            EventManager.CallOnUseObj(gameObject);
        }
    }
    public float GetEnemyPattern(int TurnNum, GameObject player)
    {
        DataBase.EnemyPattern pattern = _enemyManager.GetEnemyPattern(TurnNum);
        switch ((int)pattern) // 공격 패턴  
        {
            case 0:
                atk = DataBase.EnemyIndexList[_enemyManager.EnemyId].EnemyPatternData[0];
                player.GetComponent<PlayerController>().GetDamage(atk);
                break;
            case 1:
                def = DataBase.EnemyIndexList[_enemyManager.EnemyId].EnemyPatternData[1];
                GetDefence(def);
                break;
            case 2:
                atk = DataBase.EnemyIndexList[_enemyManager.EnemyId].EnemyPatternData[2];
                player.GetComponent<PlayerController>().GetDamage(atk);
                def = DataBase.EnemyIndexList[_enemyManager.EnemyId].EnemyPatternData[3];
                GetDefence(def);
                break;
        }
        return 2.0f; // 공격애니메이션에 맞게 딜레이
    }

    public void GetDamage(int damage)
    {
        _enemyManager.GetDamage(damage);
        if (!_enemyManager.IsAlive())
        {
            //죽는 애니메이션
            EventManager.CallOnCheckBattle(gameObject);
            Destroy(gameObject);
        }
        _enemyView.SetEnemyDefence(_enemyManager.Defence);
        _enemyView.SetEnemyHp(_enemyManager.CurrentHP, _enemyManager.MaxHP);
        _enemyView.SetEnemyAnim(1);
        
    }

    public void GetDefence(int defence)
    {
        _enemyManager.GetDefence(defence);
        _enemyView.SetEnemyDefence(_enemyManager.Defence);
    }

    public void SetNewTurn()
    {
        _enemyManager.ResetDefence();
        _enemyView.SetEnemyDefence(_enemyManager.Defence);
    }

    



}
