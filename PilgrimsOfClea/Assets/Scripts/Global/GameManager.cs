using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool IsClick;
    [SerializeField] private Texture2D _pointerIdle;
    [SerializeField] private Texture2D _pointerCard;
    public bool IsAnim;
    public DataBase.CardState CardState;
    public DataBase.CardState PostCardState;
    private GameObject _onClickObj;


    public GameObject OnClickObj
    {
        get { return _onClickObj; }
    }

    private DataBase.ObjType _onClickType;
    public DataBase.ObjType OnClickType
    {
        get { return _onClickType; }
    }

    private void Start()
    {
        SetCursorIdle();
        IsAnim = false;
        IsClick = false;
        CardState = DataBase.CardState.CardPanel;
        PostCardState = DataBase.CardState.CardPanel;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetCursorIdle()
    {
        Cursor.SetCursor(_pointerIdle, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorCard()
    {
        Cursor.SetCursor(_pointerCard, Vector2.zero, CursorMode.Auto);
    }

    public void SetOnClickObj(GameObject obj = null, DataBase.ObjType type = DataBase.ObjType.None)
    {
        if (obj == null)
        {
            _onClickObj = null;
            _onClickType = type;
        }
        else
        {
            _onClickObj = obj;
            _onClickType = type;
        }
        
    }

    public void EmptyObj()
    {
        SetCursorIdle();
        _onClickObj = null;
        _onClickType = DataBase.ObjType.None;
        IsClick = false;
    }

    


}
