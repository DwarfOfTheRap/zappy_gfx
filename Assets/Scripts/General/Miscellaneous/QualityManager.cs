using UnityEngine;
using System.Collections;

public class QualityManager {
	// Event
	public delegate void QualityEventHandler(QualityEventArg arg);
	public static event QualityEventHandler OnQualityChange;

	int					_quality;

	public QualityManager()
	{
		InitResolutionSettings();
	}

	void InitResolutionSettings()
	{
		if (PlayerPrefs.HasKey ("FullScreen"))
			Screen.fullScreen = PlayerPrefs.GetInt ("FullScreen") != 0;
		if (PlayerPrefs.HasKey ("Resolution"))
		{
			foreach (var res in Screen.resolutions)
			{
				if (res.width == PlayerPrefs.GetInt ("Resolution"))
					Screen.SetResolution (res.width, res.height, Screen.fullScreen);
			}
		}
		if (PlayerPrefs.HasKey ("Quality"))
			ChangeQuality (PlayerPrefs.GetInt ("Quality"));
	}

	public static int GetQualityLevel()
	{
		return QualitySettings.GetQualityLevel ();
	}

	public void ChangeQuality(int qualityLevel)
	{
		_quality = qualityLevel;
		QualitySettings.SetQualityLevel (qualityLevel);
		if (OnQualityChange != null)
			OnQualityChange(new QualityEventArg { qualityLevel = qualityLevel });
	}

	public void Update()
	{
		if (_quality != QualitySettings.GetQualityLevel ())
			ChangeQuality (QualitySettings.GetQualityLevel ());
	}
}

public class QualityEventArg : System.EventArgs
{
	public int			qualityLevel { get; set; }
}
