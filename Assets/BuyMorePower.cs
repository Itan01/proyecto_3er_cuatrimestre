using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMorePower : MonoBehaviour
{
    [SerializeField] private ItemShop _item;
    private void Start()
    {
        if (_item == null) _item = GetComponentInParent<ItemShop>();
    }
    public void Buy()
    {
        if (_item.Price > Save_Progress_Json.Instance.Data.Money) return;
        AudioManager.Instance.PlaySFX(_item.Clip, 1.0f);
        Save_Progress_Json.Instance.Data.Money -= _item.Price;
        Save_Progress_Json.Instance.Data.GunPowerMultiplier += 0.5f;
        Save_Progress_Json.Instance.SaveData();
    }
}
