using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Money : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(0, _rotationSpeed, 0);
        MoneyData.Y = transform.rotation.eulerAngles.y;
    }

    void Start() => MoneyData.MoneyName = gameObject.name;

    public string PickedUpKey => $"Money-{gameObject.name}-PickedUp";

    public MoneyData MoneyData { get; private set; } = new MoneyData();

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent<PlayerController>(out var player))
        {
            player.AddMoney();
            gameObject.SetActive(false);
            MoneyData.WasPickedUp = true;
        }
    }

    public void Load(MoneyData moneyData)
    {

        MoneyData = moneyData;
        gameObject.SetActive(!MoneyData.WasPickedUp);
        transform.rotation = Quaternion.Euler(0, MoneyData.Y, 0);
        //int wasPickedUp = PlayerPrefs.GetInt(gameNumber + PickedUpKey);
    }

}
