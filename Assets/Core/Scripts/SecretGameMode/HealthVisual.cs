using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour
{
    [SerializeField] private Image[] _healthPointList;

    private void Start()
    {
        SecretGameModePlayer.Instance.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, EventArgs e)
    {
        if (SecretGameModePlayer.Instance.Health >= 0)
        {
            _healthPointList[SecretGameModePlayer.Instance.Health].gameObject.SetActive(false);
        }
    }
}
