using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenePosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> positionSpawn = new List<Transform>();

    void Start()
    {
        player.position = new Vector3(positionSpawn[NumberSceneLoad.numberPositionLoad].position.x, positionSpawn[NumberSceneLoad.numberPositionLoad].position.y, player.position.z);
    }
}
