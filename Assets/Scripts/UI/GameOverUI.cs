using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameObject[] _childs;
    private int _childCount;

    // 식 본문 (Expression Body)
    public void Activate()
    {
        for (int i = 0; i < _childCount; i++)
        {
            _childs[i].SetActive(true);
        }
    }

    void Awake()
    {
        _childCount = transform.childCount;
        _childs = new GameObject[_childCount]; // 자식 오브젝트를 관리한다.

        for (int i = 0; i < _childCount; i++)
        {
            _childs[i] = transform.GetChild(i).gameObject;
        }
    }

    void OnEnable()
    {
        GameManager.Instance.OnGameOver.AddListener(Activate);
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameOver.RemoveListener(Activate);
    }
}