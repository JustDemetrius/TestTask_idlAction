using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    private int scoreToShow;

    [SerializeField] private PlayerController _player;

    [SerializeField] private RectTransform CoinsScoreBoard;
    [SerializeField] private RectTransform PackScoreBoard;

    [SerializeField] private GameObject CoinPrefab;
    [SerializeField] private TextMeshProUGUI _scoreCoins;
    [SerializeField] private TextMeshProUGUI _scorePacks;

    private void Awake()
    {
        scoreToShow = _player._Coins;
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);
    }
    

    public void GetCoins()
    {
        var coin = Instantiate(CoinPrefab);
        coin.transform.SetParent(CoinsScoreBoard.transform);
        coin.transform.DOMove(CoinsScoreBoard.position, 0.5f).SetEase(Ease.InQuint).OnComplete(() => ChangeScoreCoins(coin.gameObject));
    }


    private void ChangeScoreCoins(GameObject coin)
    {
        Destroy(coin);
        StartCoroutine(AnimateScoreChanging());
    }
    public void ChangePacksScore()
    {
        _scorePacks.text = $"{_player._curAmmount} / {_player._maxAmmount}";
    }

    private void RotateToZero(Transform some)
    {
        some.DOLocalRotate(Vector3.zero, 0.1f);
    }
    
    private IEnumerator AnimateScoreChanging()
    {
        while (scoreToShow < _player._Coins)
        {
            scoreToShow++;
            _scoreCoins.text = $"Coins: {scoreToShow}";
            CoinsScoreBoard.transform.DOLocalRotate(new Vector3(0, 0, Random.Range(-10, 10)), 0.2f).OnComplete(() => RotateToZero(CoinsScoreBoard.transform));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
