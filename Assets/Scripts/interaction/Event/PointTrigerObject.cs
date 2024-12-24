using System;
using UnityEngine;

public class PointTrigerObject : MonoBehaviour
{
    public event Action trigerE;

    public void StartEvent()
    {
        trigerE?.Invoke();
    }
}
