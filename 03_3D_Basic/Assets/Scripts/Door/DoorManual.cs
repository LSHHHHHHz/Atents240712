using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorManual : DoorBase, IInteractable
{
    /// <summary>
    /// 재사용에 필요한 쿨타임
    /// </summary>
    public float coolTime = 1.0f;

    /// <summary>
    /// 현재 남아있는 쿨타임
    /// </summary>
    float remainsCoolTime = 0.0f;

    /// <summary>
    /// 문이 열려있는지를 표시하는 변수(true면 열려있다. false면 닫혀있다)
    /// </summary>
    bool isOpen = false;

    /// <summary>
    /// 근처에 플레이어가 접근하면 상호작용용 단축키를 알려주는 3D 텍스트
    /// </summary>
    TextMeshPro text;

    /// <summary>
    /// 현재 이 오브젝트를 사용가능한지 판단하기 위한 프로퍼티(인터페이스에 있는 프로퍼티 구현)
    /// </summary>
    public bool CanUse => remainsCoolTime < 0.0f;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshPro>(true);
    }

    void Update()
    {
        remainsCoolTime -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false); 
        }
    }

    /// <summary>
    /// 문을 사용하는 함수(인터페이스에 있는 함수 구현)
    /// </summary>
    public void Use()
    {
        if (CanUse)
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }

            remainsCoolTime = coolTime;
        }
    }

    /// <summary>
    /// 문이 열렸음을 표시하는 기능
    /// </summary>
    protected override void OnOpen()
    {
        isOpen = true;
    }

    /// <summary>
    /// 문이 닫혔음을 표시하는 기능
    /// </summary>
    protected override void OnClose()
    {
        isOpen = false;
    }
}
