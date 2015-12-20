using UnityEngine;
using System.Collections;

public interface IAnimatorController {
	void SetBool(string name, bool value);
	void SetFloat(string name, float value);
	void SetInteger(string name, int value);
	void SetTrigger(string name);
}
