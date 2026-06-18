using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ShowEndPanel();
    }
}
