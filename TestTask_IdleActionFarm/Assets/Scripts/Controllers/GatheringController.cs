using UnityEngine;

public class GatheringController : MonoBehaviour
{
    public GameObject _tool;
    Animator _animator;
    PlayerController _player;
    bool ableToGather = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerController>();
    }
    private void Update()
    {
        _animator.SetBool("CanGather", ableToGather);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GrowingSystem _wheat))
        {
            if (_wheat._cropState == GrowingSystem.CropState.Sliced || _wheat._cropState == GrowingSystem.CropState.Final)
            {
                _tool.SetActive(true);
                ableToGather = true;
            }
            else
            {
                _tool.SetActive(false);
                ableToGather = false;
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlantToGather")
        {
            _tool.SetActive(false);
            ableToGather = false;
        }
    }

}
