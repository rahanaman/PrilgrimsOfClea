using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int _defence = 0;
    public int Defence
    {
        get { return _defence; }
    }


    private int GetCalculatedDamage(int damage)
    {
        return damage;
    }

    private int GetCalculatedDefence(int defence)
    {
        return defence;
    }


    public void ResetDefence()
    {
        _defence = 0;
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
        
         DataManager.PlayerCurrentHP -= calculatedDamage;
        
    }

    public void GetDefence(int defence)
    {
        int calaulatedDefence = GetCalculatedDefence(defence);
        _defence += calaulatedDefence;
    }

        
}
