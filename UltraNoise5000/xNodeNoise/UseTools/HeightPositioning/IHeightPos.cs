using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeightPos
{

    bool HeightCheck(Vector3 pos);

    
    float GetHeightPos (Vector3 pos);

}