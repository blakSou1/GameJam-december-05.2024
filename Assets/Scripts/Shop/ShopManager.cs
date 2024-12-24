using TMPro;
using UnityEngine;
using System;
using Player;

namespace Shop
{
    public class ShopManager : Coins
    {
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private bool destroyCoin = true;

        private void Start()
        {
            shopPanel = this.gameObject;
            shopPanel.SetActive(false);
            
            GetComponent<PointTrigerObject>().trigerE += TriggerPanel;

            if (destroyCoin)
                coins = 0;
        }

        private void TriggerPanel()
        {
            if (shopPanel.activeInHierarchy)
            {
                PlayerMovements.isLocked = false;
                shopPanel.SetActive(false);
            }
            else
            {
                PlayerMovements.isLocked = true;
                shopPanel.SetActive(true);
            }
        }
        public void Item1(int amount)
        {
            if (coins >= amount)
            {
                Debug.Log("Продано!");

                coins -= amount;
                Dynamit.isWorked = 1;
                PlayerPrefs.SetInt("isWork", Dynamit.isWorked);
            }
            
        }

        private void OnDisable()
        {
            GetComponent<PointTrigerObject>().trigerE -= TriggerPanel;
        }
    }
}

