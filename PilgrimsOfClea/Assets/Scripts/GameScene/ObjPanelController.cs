using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPanelController : MonoBehaviour
{
    private void OnMouseDown()
    {
        
        if (!GameManager.instance.IsAnim&&GameManager.instance.CardState == DataBase.CardState.HandCard && GameManager.instance.IsClick && (int)GameManager.instance.OnClickType % 2 == 1) // ��Ÿ������ ��쿡��
        {
            EventManager.CallOnUseObj();
        }
    }
}
