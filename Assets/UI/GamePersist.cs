using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GamePersist : MonoBehaviour
{
    GameData _gameData = new GameData();
    PlayerController _player;

    //void Awake() => _player = FindObjectOfType<HeroKnight>();

    void Awake() => _player = FindObjectOfType<PlayerController>();

    public void Load(int gameNumber)
    {

        using (StreamReader streamReader = new StreamReader($"SaveGame{gameNumber}.json"))
        {
            var json = streamReader.ReadToEnd();

            //string json = PlayerPrefs("GameData" + gameNumber);
            _gameData = JsonUtility.FromJson<GameData>(json);

            foreach (var money in FindObjectsOfType<Money>(includeInactive: true))
            {
                var moneyData = _gameData.MoneyDatas.FirstOrDefault(t => t.MoneyName == money.name);
                money.Load(moneyData);
            }

            _player.transform.position = _gameData.PlayerPosition;
            _player.Money = _gameData.Money;
        }
    }

    public void Save(int gameNumber)
    {

        _gameData.MoneyDatas.Clear();

        foreach (var money in FindObjectsOfType<Money>(includeInactive: true))
        {
            _gameData.MoneyDatas.Add(money.MoneyData);
        }

        _gameData.Money = _player.Money;
        _gameData.PlayerPosition = _player.transform.position;


        var json = JsonUtility.ToJson(_gameData);

        using (StreamWriter streamWriter = new StreamWriter($"SaveGame{gameNumber}.json"))
        {
            streamWriter.Write(json);
        }

        //PlayerPrefs.SetString("GameData" + gameNumber, json);

    }
}


