using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerAngle;

	public float moveSpeed;
	public float spacing;

	public Vector3 mousePosition;
	private Vector3 direction;
	private Vector3 pos;

	private bool goingUp;
	private bool goingDown;
	private bool goingLeft;
	private bool goingRight;

	private bool swingingWeapon;

	private int swingWeaponCount;
	private int swingWeaponDirection;

	public GameObject currentPlayerWeapon;

	// Use this for initialization
	void Start () {
		//Set the starting angle
		playerAngle = 0;
		moveSpeed = 2f;
		spacing = .1f;
		pos = transform.position;
		goingUp = false;
		goingDown = false;
		goingLeft = false;
		goingRight = false;
		swingingWeapon = false;
		swingWeaponCount = 0;
		swingWeaponDirection = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//Get the new mouse position
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Calculate the angle between the player and mouse position
		direction = mousePosition - transform.position;
		playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerAngle));

		pos = transform.position;
		//Move the player based on keyboard input
		if(Input.GetKeyDown(KeyCode.W)){
			goingUp = true;
		}
		if(Input.GetKeyDown(KeyCode.S)){
			goingDown = true;
		}
		if(Input.GetKeyDown(KeyCode.A)){
			goingLeft = true;
		}
		if(Input.GetKeyDown(KeyCode.D)){
			goingRight = true;
		}
		if(Input.GetKeyUp(KeyCode.W)){
			goingUp = false;
		}
		if(Input.GetKeyUp(KeyCode.S)){
			goingDown = false;
		}
		if(Input.GetKeyUp(KeyCode.A)){
			goingLeft = false;
		}
		if(Input.GetKeyUp(KeyCode.D)){
			goingRight = false;
		}

		if(Input.GetMouseButtonDown(0) && !swingingWeapon){
			swingingWeapon = true;
			swingWeaponCount = 0;
			swingWeaponDirection = 1;
			GameObject prefab = Resources.Load("Weapon") as GameObject;
			currentPlayerWeapon = Instantiate(prefab);
			currentPlayerWeapon.transform.position = transform.position;
			currentPlayerWeapon.transform.position += transform.right;

			currentPlayerWeapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerAngle+45));
		}
		if(Input.GetMouseButtonUp(0)){
			//swingingWeapon = false;
		}

		if (goingUp){
			pos.y += spacing;
		}
		if (goingDown){
			pos.y -= spacing;
		}
		if (goingLeft){
			pos.x -= spacing;
		}
		if (goingRight){
			pos.x += spacing;
		}

		if (swingingWeapon){
			currentPlayerWeapon.transform.position = transform.position;
			currentPlayerWeapon.transform.position += currentPlayerWeapon.transform.right;
			currentPlayerWeapon.transform.RotateAround(transform.position, new Vector3(0, 0, swingWeaponDirection),-200*Time.deltaTime);
			swingWeaponCount++;

			if (swingWeaponCount == 25){
				swingWeaponDirection = -1;
			}
			if (swingWeaponCount == 50){
				swingingWeapon = false;
				Destroy(currentPlayerWeapon, 0); //Destroy after 0 seconds
			}
		}

		transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
	}
}
