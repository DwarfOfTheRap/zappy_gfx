using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeSliderUpdater : MonoBehaviour {

    public void UpdateSliderOnServerChange(int value)
    {
        gameObject.GetComponent<Slider>().value = value;
        GameManagerScript.instance.timeManager.ChangeTimeSpeed(value);
    }

    public void UpdateServerOnEvent(float value)
    {
        ServerQuery query = new ServerQuery();

        query.SendTimeUnitChangeQuery((int)value);
    }
}
