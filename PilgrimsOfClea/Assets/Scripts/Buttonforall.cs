using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonforall : MonoBehaviour
{
    [SerializeField] Button _다하는Button;
    private void Start()
    {
        _다하는Button.onClick.AddListener(E);
    }

    private void A()
    {
        DataManager.PlayerCurrentHP -= 5;
        EventManager.CallOnPlayerHP(DataManager.PlayerCurrentHP, DataBase.PlayerMaxHP[0]);
        EventManager.CallOnPlayerAnim(1);
    }

    private void B()
    {
        EventManager.CallOnSoundID(DataBase.SoundID.DianaSelection, 5);
    }

    private void C()
    {
    }
    private void D()
    {
        
        DataManager.PlusPlayerDeck(0);
        EventManager.CallOnPlayerDeckNum(DataManager.PlayerDeck.Count.ToString());
        //EventManager.CallOnCard(1);

    }

    private void E()
    {
    }
}
