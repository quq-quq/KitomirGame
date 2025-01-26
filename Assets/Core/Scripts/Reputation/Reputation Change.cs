using UnityEngine;
using UnityEngine.UI;

public class ReputationChange : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _startReputation;
    [SerializeField] private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar.value = _startReputation;
    }

    public void ChangeReputation(float value)
    {

        _scrollbar.value += value;
    }
}
