using System.Collections;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;

public class PackageController : MonoBehaviour
{
    [SerializeField] private UiController UiController;
    [SerializeField] private Transform _toStorage;
    [SerializeField] private StuckModule stuckModule;
    [SerializeField] private TriggerPickUpModule triggerModule;

    PlayerController _player;

    private bool CanDoSomeAgain = true;


    private void Start()
    {
        _player = GetComponent<PlayerController>();

        stuckModule.Init();
        triggerModule.FindPacks += stuckModule.AddToCarry;
        triggerModule.FindPacks += AddCountPacks;
    }
    

    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Storage"))
        {
            if (_player._curAmmount > 0 && CanDoSomeAgain)
            {
                _player._curAmmount -= 1;
                var pack = stuckModule._collectedPacks.Last();
                pack.transform.DOJump(_toStorage.position, 5, 1, 0.5f).OnComplete(() => SellPack(pack.gameObject));
                stuckModule._collectedPacks.Remove(pack);
                UiController.ChangePacksScore();
                CanDoSomeAgain = false;
                StartCoroutine(WaitSomeSec(0.05f));
            }
        }
    }
    private void SellPack(GameObject pack)
    {
        Destroy(pack);
        _player._Coins += 15;
        UiController.GetCoins();
    }

    private void AddCountPacks(Package pack)
    {
        _player._curAmmount++;
        UiController.ChangePacksScore();
    }
    IEnumerator WaitSomeSec(float _timeToWait)
    {
        yield return new WaitForSeconds(_timeToWait);
        CanDoSomeAgain = true;
    }
    
}
