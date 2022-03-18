using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Package : MonoBehaviour
{
    public bool isPickable = false;
    public bool isPicked = false;

    private void Start()
    {
        StartCoroutine(WaitAndPick());
    }
    
    public void JumpToPlayer(Vector3 _dest)
    {
        transform.DOLocalJump(_dest, 2, 1, 0.5f).SetEase(Ease.Linear);
        isPicked = true;
    }
    public void UnableToCarry()
    {
        StartCoroutine(JumpOnPlace());
    }
    private IEnumerator WaitAndPick()
    {
        yield return new WaitForSeconds(1f);
        isPickable = true;
    }
    private IEnumerator JumpOnPlace()
    {
        transform.DOLocalMoveY(3, 0.2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.2f);
        transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.InSine);
    }
}
