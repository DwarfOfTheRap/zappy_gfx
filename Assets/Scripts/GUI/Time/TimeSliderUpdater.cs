using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class TimeSliderUpdater : MonoBehaviour, IPointerUpHandler {

	void Start()
	{
		var slider = gameObject.GetComponent<Slider>();
		GameManagerScript.instance.timeManager.SetSlider(slider);
	}

    void UpdateServerOnEvent(float value)
    {
		GameManagerScript.instance.timeManager.ChangeTimeSpeedClient (value);
    }

	public void OnPointerUp (PointerEventData eventData)
	{
		UpdateServerOnEvent(gameObject.GetComponent<Slider>().value);
	}
}
