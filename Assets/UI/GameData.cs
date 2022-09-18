using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class GameData {
    public int Money;
    public Vector3 PlayerPosition;
    public List<MoneyData> MoneyDatas = new List<MoneyData>();
}
