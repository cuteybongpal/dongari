using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE{
    Like,
    // ���̾�ΰ� �̺�Ʈ
    StartDia, NextDia, StopDia,
    // ȭ�� ���� �̺�Ʈ
    BlackScreen
}
public interface ILinerEvnet
{
    public void OnEvnet(EVENT_TYPE type, Component sender,object param1 = null, object param2 = null);
}