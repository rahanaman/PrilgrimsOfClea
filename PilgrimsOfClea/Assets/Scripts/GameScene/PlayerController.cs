using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerManager _playerManager = new PlayerManager();

    public void Start()
    {
        _playerManager.ResetDefence();
    }
    public void GetDamage(int damage)
    {

        _playerManager.GetDamage(damage);
        if (!IsAlive())
        {
            EventManager.CallOnCheckBattle(gameObject);
            //죽는 애니메이션 필요
        }
        EventManager.CallOnPlayerDefence(_playerManager.Defence);
        EventManager.CallOnPlayerHP(DataManager.PlayerCurrentHP, DataBase.PlayerMaxHP[DataManager.PlayerID]);
        EventManager.CallOnPlayerAnim(1);
    }

    public void GetDefence(int defence)
    {
        _playerManager.GetDefence(defence);
        EventManager.CallOnPlayerDefence(_playerManager.Defence);

    }
    public bool IsAlive()
    {
        if(DataManager.PlayerCurrentHP <= 0) { return false; }
        else return true;
    }

    public void SetNewTurn()
    {
        _playerManager.ResetDefence();
        EventManager.CallOnPlayerDefence(_playerManager.Defence);
    }
       
    
}
