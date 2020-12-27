using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    // 플레이어가 호출하면 해당 방향으로 움직입니다.
    void Pushed(Vector3 dir);
    
}
