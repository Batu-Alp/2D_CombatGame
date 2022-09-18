using UnityEngine;
using TMPro;

public class UIMoneyDisplay : MonoBehaviour
{
    private PlayerController _player;
    private TMP_Text _text;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.SetText("$" + _player.Money);
    }
}
