using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerInputETriger : MonoBehaviour
{
    public event Action ClickEPlayer;
    private readonly HashSet<Collider> activeTriggers = new HashSet<Collider>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ClickEPlayer?.Invoke();
            ClickStart();
        }
    }
    private void ClickStart()
    {
        if (activeTriggers.Count == 0) return;

        Collider closestTrigger = null;
        float closestDistance = Mathf.Infinity;

        foreach (var trigger in activeTriggers)
        {
            float distance = Vector2.Distance(transform.position, trigger.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTrigger = trigger;
            }
        }
        if (closestTrigger != null)
            closestTrigger.GetComponent<PointTrigerObject>().StartEvent();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PointTrigerObject>() != null)
            activeTriggers.Add(collision);
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<PointTrigerObject>() != null)
            activeTriggers.Remove(collision);
    }
}
