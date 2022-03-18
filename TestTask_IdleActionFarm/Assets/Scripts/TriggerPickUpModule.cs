using UnityEngine;
using System;
using DG.Tweening;

public class TriggerPickUpModule : MonoBehaviour
{
    public Action<Package> FindPacks;
    PlayerController _player;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Package pack))
        {
            if (_player._curAmmount + 1 <= _player._maxAmmount && pack.isPickable)
            {
                FindPacks?.Invoke(pack);
                other.enabled = false;
                other.transform.DOScale(1f, 1);
            }
            else
            {
                pack.UnableToCarry();
            }
                
        }
    }
}
