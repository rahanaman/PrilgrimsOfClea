using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanelView : MonoBehaviour
{

    [SerializeField] GameObject _battlePanel;

    private void OnEnable()
    {
        EventManager.SetHandCardList += SetHandCard;
    }

    private void OnDisable()
    {
        EventManager.SetHandCardList -= SetHandCard;
    }
    public GameObject AddEnemy(int data, Vector3 position) // 적 소환, 뷰에 배치
    {
        var enemy = Instantiate(DataLoader.EnemyPref[data], position, Quaternion.identity, _battlePanel.transform);
        enemy.GetComponent<EnemyController>().SetEnemy(DataBase.EnemyIndexList[data]);
        enemy.SetActive(true);
        return enemy;
    }



    private void SetHandCard(List<GameObject> handDeck)
    {
        
        Vector2 pos = new Vector2();
        Vector3 rot = new Vector3();
        Vector3 scale = new Vector3();
        int n = handDeck.Count;
        int x = 110 - 5 * n; 
        pos.x = (x * n) - 25;
        pos.y = -425 - (15 * n);
        rot.z = -2 * (n - 1);
        scale.x = 1.02f - 0.02f * n;
        scale.y = 1.02f - 0.02f * n;
        scale.z = 1.02f - 0.02f * n;
        for (int i = 0; i < n; i++)
        {
            handDeck[i].transform.localPosition = pos;
            handDeck[i].transform.rotation = Quaternion.Euler(rot);
            handDeck[i].transform.localScale = scale;
            handDeck[i].GetComponent<CardController>().SetPosRot();
            pos.x -= 2 * x;
            rot.z += 4;
            if (i < (n - 1) / 2)
            {
                pos.y += 30;
            }
            if (i >= (n / 2) && i != (n - 1))
            {
                pos.y -= 30;
            }
            handDeck[i].GetComponent<CardController>().SetIndex(n - i - 1);
        }

    }
}
