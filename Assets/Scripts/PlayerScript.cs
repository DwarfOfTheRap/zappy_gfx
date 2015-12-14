using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerScript : MonoBehaviour {
	public int			index;
	public float		speed = 1.0f;
	public bool 		isIncantating { get; private set; }
	private Vector2		positionIndex;
	private Vector3		destination;

	void Start()
	{
		destination = this.transform.position;
	}
	public void Incantate()
	{
		isIncantating = true;
		GetComponent<Animator>().SetBool ("Incantation", true);
	}

	public void SetPosition(int x, int y)
	{
		if (this.positionIndex.x != x || this.positionIndex.y != y)
		{
			destination = GameManagerScript.instance.grid.GetSquare (x, y).transform.position;
		}
		this.positionIndex.x = x;
		this.positionIndex.y = y;
	}

	int GetDirection()
	{
		if (this.transform.position == destination)
			return 0;
		Vector3 heading = destination - transform.position;
		Vector3 direction = heading / heading.magnitude;
		float absx = Mathf.Abs (direction.x);
		float absy = Mathf.Abs (direction.y);
		Debug.Log (direction);

		if (absx > absy && direction.x <= 0)
			return 4;
		if (absx > absy && direction.x > 0)
			return 2;
		if (absx <= absy && direction.y <= 0)
			return 3;
		if (absx <= absy && direction.y > 0)
			return 1;
		return 0;
	}

	void GoToDestination()
	{
		int orientation = GetDirection ();
		GetComponent<Animator>().SetBool ("Walking", (this.transform.position != destination));
		if (orientation != 0)
			GetComponent<Animator>().SetInteger("Orientation", orientation);
		this.transform.position = Vector3.MoveTowards (this.transform.position, destination, Time.deltaTime * speed);
	}
	
	void Update()
	{
		GoToDestination ();
	}
}
