using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    // Unity Methods
    private void Awake() 
    {
        // Clear static data
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }
}
