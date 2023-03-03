using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButtonView : MonoBehaviour
{
    [SerializeField] Image _image;

    public void SetImage(int data)
    {
        _image.sprite = DataBase.SelectionSprite[data];
    }
}
