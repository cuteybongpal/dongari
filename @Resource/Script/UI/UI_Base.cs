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
    //자식요소의 컴포넌트를 가져온다.
    protected T FindChild<T>(string name) where T : Component
    {
        //dict dictionarty의 키값의 이름이 있을 시 그 컴포넌트에서 GetComponent해줘서 반환해준다.
        if (dict.ContainsKey(name))
        {
            return dict[name].GetComponent<T>();
        }
        //자식 요소에서 T라는 컴포넌트를 가진 모든 게임오브젝트의 컴포넌트를 가져온다.
        T[] components = GetComponentsInChildren<T>();
        foreach (T component in components)
        {
            //컴포넌트의 게임오브젝트의 이름이 같을 시 반환해준다. => 이름이 다를시 nullreference 오류가 남
            if (component.gameObject.name ==name)
                return component;
        }
        return null;
    }
}
