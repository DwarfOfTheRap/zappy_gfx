using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class TimeSliderUpdater : MonoBehaviour, IPointerUpHandler, IBeginDragHandler, IEndDragHandler {
	public bool			drag;
	void Start()
	{
		var slider = gameObject.GetComponent<Slider>();
		GameManagerScript.instance.timeManager.SetSlider(slider);
	}

    void UpdateServerOnEvent(float value)
    {
		GameManagerScript.instance.timeManager.ChangeTimeSpeedSlider (value);
    }

	public void OnPointerUp (PointerEventData eventData)
	{
		UpdateServerOnEvent(gameObject.GetComponent<Slider>().value);
	}


	public void OnBeginDrag (PointerEventData eventData)
	{
		this.drag = true;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		this.drag = false;
	}

}
