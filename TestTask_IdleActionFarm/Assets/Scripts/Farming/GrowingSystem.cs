using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GrowingSystem : MonoBehaviour
{
    [SerializeField] private GameObject SeedState;
    [SerializeField] private GameObject HalfSliced;
    [SerializeField] private GameObject FinalState;
    [SerializeField] private GameObject SomePackage;
    [SerializeField] private ParticleSystem HittedPlant;


    [Header("Growth Timer and CropState")]
    [SerializeField] private float _growth = 0f;
    [SerializeField] private float _timeToGrow = 10f;

    public CropState _cropState;
    bool canSlice = true;

    private void Awake()
    {
        SwitchCropState(CropState.Final);
    }
    void Update()
    {
        Grow();
    }

    private void Grow()
    {
        if (_growth < _timeToGrow)
        {
            _growth += Time.deltaTime;
        }
        else
        {
            if (_cropState != CropState.Final)
            {
                SwitchCropState(CropState.Final);
            }
        }
    }
    public enum CropState
    {
        Seed, Sliced, Final
    }
    private void SwitchCropState(CropState _stateToSwitch)
    {
        SeedState.SetActive(false);
        HalfSliced.SetActive(false);
        FinalState.SetActive(false);
        switch (_stateToSwitch)
        {
            case CropState.Seed:
                SeedState.SetActive(true);
                SeedState.transform.DOShakeScale(1f).SetEase(Ease.OutBounce);
                break;
            case CropState.Sliced:
                HalfSliced.SetActive(true);
                HalfSliced.transform.DOShakeScale(1f).SetEase(Ease.OutBounce);
                break;
            case CropState.Final:
                FinalState.SetActive(true);
                FinalState.transform.DOShakeScale(1f).SetEase(Ease.OutBounce);
                break;
        }

        _cropState = _stateToSwitch;
    }
    
    public void SliceWheat()
    {
        if (_cropState == CropState.Final)
        {
            SwitchCropState(CropState.Sliced);
            _growth = 0;
            PackageWheat();
            HittedPlant.Play();
        }
        else if (_cropState == CropState.Sliced)
        {
            SwitchCropState(CropState.Seed);
            _growth = 0;
            PackageWheat();
            HittedPlant.Play();
        }
    }
    private void PackageWheat()
    {
        var pack = Instantiate(SomePackage, this.transform.position, Quaternion.identity);
        pack.transform.parent = transform;
        pack.transform.DOLocalJump(new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), 5, 1, 0.5f).SetEase(Ease.OutExpo);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tool")
        {
            if (canSlice)
            {
                SliceWheat();
                canSlice = false;
                StopCoroutine(WaitToSliceAgain());
                StartCoroutine(WaitToSliceAgain());
            }
        }
    }
    IEnumerator WaitToSliceAgain()
    {
        yield return new WaitForSeconds(0.5f);
        canSlice = true;
    }

}
