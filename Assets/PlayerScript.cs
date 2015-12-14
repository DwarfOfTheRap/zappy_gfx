using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerScript : MonoBehaviour {
	public int	index;
	public bool isIncantating { get; private set; }


	public void Incantate()
	{
		isIncantating = true;
		GetComponent<Animator>().SetBool ("Incantation", true);
	}
	
	void Update()
	{
	}
}
