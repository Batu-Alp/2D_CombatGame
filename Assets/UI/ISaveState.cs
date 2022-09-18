using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ISaveState
{
    void Save(int gameNumber);
    void Load(int gameNumber);
}