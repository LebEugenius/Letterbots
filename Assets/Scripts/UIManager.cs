using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _instruction;
    [SerializeField] private Text _key;
    [SerializeField] private Text _anyKey;
    [SerializeField] private Text _press;

    [SerializeField] private Text _winner;
    [SerializeField] private Text _level;
    [SerializeField] private Text _music;

    private GameController _gameController;

    private InputController _inputController;
    [Inject]
    public void Construct(
        GameController gameController,
        InputController inputController)
    {
        _gameController = gameController;
        _inputController = inputController;
    }

    private void Update()
    {
        switch (_gameController.GameState)
        {
            case GameState.Idle:
                _winner.text = "";
                _anyKey.text = "Any key";
                _instruction.text = "Start";
                _key.text = "";
                _press.text = "Press";
                break;
            case GameState.Game:
                _anyKey.text = "";
                _key.text = _inputController.CurrentKeyCode.ToString();
                _instruction.text = "Attack";
                _winner.text = "";
                _press.text = "Press";
                break;
            case GameState.Prepare:
                _anyKey.text = "";
                _key.text = Mathf.CeilToInt(_gameController.GetTimeBeforeStart()).ToString();
                _winner.text = "";
                _instruction.text = "";
                _press.text = "";
                break;
            case GameState.Lose:
                _winner.text = "<color=red>RED</color> WIN";;
                _anyKey.text = "";
                _press.text = "";
                _key.text = "";
                _instruction.text = "";
                break;
            case GameState.Win:
                _winner.text = "<color=blue>BLUE</color> WIN";
                _anyKey.text = "";
                _press.text = "";
                _key.text = "";
                _instruction.text = "";
                break;
            default:
                _instruction.text = "";
                _key.text = "";
                _anyKey.text = "";
                _press.text = "";
                _winner.text = "";
                break;
        }

        _level.text = "Level " + _gameController.CurrentLevel + " / Record " + _gameController.RecordLevel;
    }

    public void MuteMusic(bool mute)
    {
        _music.text = mute ? "Music Off" : "Music On";
        AudioListener.volume = mute ? 0 : 1;
    }

    public void OnIncorrectKeyPressed()
    {
        StopCoroutine("ColorText");
        var initColor = Color.green;
        _key.color = Color.red;
        StartCoroutine(ColorText(_key, initColor, 0.2f));
    }

    private IEnumerator ColorText(Text text, Color color, float time)
    {
        var oldColor = text.color;
        var delta = 0f;
        do
        {
            delta += Time.deltaTime;
            text.color = Color.LerpUnclamped(oldColor, color, delta / time);
            yield return null;
        } while (delta < time);
    }
}
