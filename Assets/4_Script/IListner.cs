using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE{
    Like,
    // 다이어로고 이벤트
    StartDia, NextDia, StopDia,
    // 화면 시작 이벤트
    BlackScreen
}
public interface ILinerEvnet
{
    public void OnEvnet(EVENT_TYPE type, Component sender,object param1 = null, object param2 = null);
}