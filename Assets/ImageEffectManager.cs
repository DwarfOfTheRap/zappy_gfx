using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ImageEffectManager : MonoBehaviour {
	GameObject directionalLight;

	void Start()
	{
		this.directionalLight = GameObject.Find("Directional light");
	}

	void ManageQualitySettings()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		this.directionalLight.SetActive (qualityLevel > 0);
		GetComponentInChildren<SkyboxCamera>().enabled = qualityLevel > 0;
		GetComponent<Bloom>().enabled = qualityLevel > 2;
		GetComponent<VignetteAndChromaticAberration>().enabled = qualityLevel > 3;
		GetComponent<Antialiasing>().enabled = qualityLevel > 3;
		GetComponent<CameraMotionBlur>().enabled = qualityLevel > 4;
	}
	void Update()
	{
		ManageQualitySettings ();
	}
}
