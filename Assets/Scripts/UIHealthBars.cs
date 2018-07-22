using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIHealthBars : MonoBehaviour
{
    [Inject] private SkirmishController _skirmish;
    [SerializeField] private Image _blueHPBar;
    [SerializeField] private Image _redHPBar;

	private void Update ()
	{
	    _blueHPBar.fillAmount = _skirmish.GetBluePlayerHealth() / _skirmish.GetStartHealth();
	    _redHPBar.fillAmount = _skirmish.GetRedPlayerHealth() / _skirmish.GetStartHealth();
	}
}
