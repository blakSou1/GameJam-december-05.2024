using UnityEngine;
using GamePush;

public class DeadReclam : MonoBehaviour
{
    private GameObject player;
    Vector3 pos;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        pos = player.transform.position;
    }
    public void Reclama()
    {
        if (GP_Init.isReady)
        {
            GP_Ads.ShowRewarded();
        }
    }
    private void OnRewarded(string dead)
    {
        player.GetComponent<HitPlayer>().Health(100);
        player.transform.position = pos;
    }

    private void OnEnable()
    {
        GP_Ads.OnRewardedReward += OnRewarded;
    }
    private void OnDisable()
    {
        GP_Ads.OnRewardedReward -= OnRewarded;
    }
}
