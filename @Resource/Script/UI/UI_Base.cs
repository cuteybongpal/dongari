using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    Dictionary<string, Object> dict = new Dictionary<string, Object>();
    void Start()
    {

    }
    //�ڽĿ���� ������Ʈ�� �����´�.
    protected T FindChild<T>(string name) where T : Component
    {
        //dict dictionarty�� Ű���� �̸��� ���� �� �� ������Ʈ���� GetComponent���༭ ��ȯ���ش�.
        if (dict.ContainsKey(name))
        {
            return dict[name].GetComponent<T>();
        }
        //�ڽ� ��ҿ��� T��� ������Ʈ�� ���� ��� ���ӿ�����Ʈ�� ������Ʈ�� �����´�.
        T[] components = GetComponentsInChildren<T>();
        foreach (T component in components)
        {
            //������Ʈ�� ���ӿ�����Ʈ�� �̸��� ���� �� ��ȯ���ش�. => �̸��� �ٸ��� nullreference ������ ��
            if (component.gameObject.name ==name)
                return component;
        }
        return null;
    }
}
