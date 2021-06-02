////////////////////////////////////////////////////////////
// File: CharacterController.cs
// Author: Morgan Henry James
// Date Created: 32/01/2020
// Brief: The base character controller for a player.
//////////////////////////////////////////////////////////// 

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The base character controller for a player.
/// </summary>
public class FighterController : MonoBehaviour
{
	#region Variables
	#region Private
	#region Controls
	/// <summary>
	/// The horizontal axis for the left stick.
	/// </summary>
	private string leftHorizontalAxis = "LeftHorizontal";

	/// <summary>
	/// The vertical axis for the left stick.
	/// </summary>
	private string leftVerticalAxis = "LeftVertical";

	/// <summary>
	/// The horizontal axis for the right stick.
	/// </summary>
	private string rightHorizontalAxis = "RightHorizontal";

	/// <summary>
	/// The vertical axis for the right stick.
	/// </summary>
	private string rightVerticalAxis = "RightVertical";

	/// <summary>
	/// The jump button.
	/// </summary>
	private string jumpButton = "Jump";

	/// <summary>
	/// The light attack button.
	/// </summary>
	private string lightAttackButton = "Light";

	/// <summary>
	/// The block button.
	/// </summary>
	private string blockButton = "Block";

	/// <summary>
	/// The heavy attack button.
	/// </summary>
	private string heavyAttackButton = "Heavy";

	/// <summary>
	/// The grab button.
	/// </summary>
	private string grabButton = "Grab";

	/// <summary>
	/// The right stick button.
	/// </summary>
	private string rightStick = "RightStick";

	/// <summary>
	/// The special button.
	/// </summary>
	private string specialButton = "Special";

	/// <summary>
	/// The start button.
	/// </summary>
	private string startButton = "Start";

	/// <summary>
	/// The back button.
	/// </summary>
	private string backButton = "Back";

	/// <summary>
	/// The right trigger axis.
	/// </summary>
	private string rightTriggerAxis = "RightTrigger";

	/// <summary>
	/// The left trigger axis.
	/// </summary>
	private string leftTriggerAxis = "LeftTrigger";
	#endregion

	/// <summary>
	/// The characters grab location.
	/// </summary>
	[Tooltip("The characters grab location.")]
	[SerializeField] private GameObject grabLocation = null;

	/// <summary>
	/// The characters rigidBody.
	/// </summary>
	[Tooltip("The characters rigidBody.")]
	[SerializeField] private new Rigidbody rigidbody = null;

	/// <summary>
	/// The player indicator.
	/// </summary>
	[Tooltip("The player indicator.")]
	[SerializeField] private Image playerIndicator = null;

	/// <summary>
	/// The player silhouettes. 
	/// </summary>
	[Tooltip("The player silhouettes.")]
	[SerializeField] private GameObject[] silhouettes = null;

	/// <summary>
	/// How fast the character can move.
	/// </summary>
	[Tooltip("How fast the character can move.")]
	[SerializeField] private float speed = 25.0f;

	/// <summary>
	/// How high the character can jump.
	/// </summary>
	[Tooltip("How high the character can jump.")]
	[SerializeField] private float jumpHeight = 1000.0f;

	/// <summary>
	/// The throw power.
	/// </summary>
	[Tooltip("The throw power.")]
	[SerializeField] private float throwPower = 10.0f;

	/// <summary>
	/// The power needed to break a grab.
	/// </summary>
	[Tooltip("The power needed to break a grab.")]
	[SerializeField] private float grabThreshold = 100.0f;

	/// <summary>
	/// The max block amount.
	/// </summary>
	[Tooltip("The max block amount.")]
	[SerializeField] private float maxBlock = 100.0f;

	/// <summary>
	/// The block transform.
	/// </summary>
	[Tooltip("The block transform.")]
	[SerializeField] private Transform blockObject = null;

	/// <summary>
	/// The block mesh renderer.
	/// </summary>
	[Tooltip("The block mesh renderer.")]
	[SerializeField] private MeshRenderer blockMeshRenderer = null;

	/// <summary>
	/// The grab threshold remaining.
	/// </summary>
	private float currentGrabThreshold = 0.0f;

	/// <summary>
	/// The location of the last footstep sound.
	/// </summary>
	private Vector3 locationOfLastFootstep;

	/// <summary>
	/// True when requesting to jump.
	/// </summary>
	private bool jumpRequest = false;

	/// <summary>
	/// The gravity scale of the character.
	/// </summary>
	private float gravityScale = 1.0f;

	/// <summary>
	/// The value for gravity.
	/// </summary>
	private float globalGravity = -9.81f;

	/// <summary>
	/// Gravity multiplier when falling.
	/// </summary>
	private float fallMultiplier = 2.5f;

	/// <summary>
	/// Gravity multiplier on quick release.
	/// </summary>
	private float lowJumpMultiplier = 2.0f;

	/// <summary>
	/// The invincibility time of respawning.
	/// </summary>
	private float respawnInvincibilityTime = 2.5f;

	/// <summary>
	/// The invincibility time of respawning.
	/// </summary>
	private float currentRespawnInvincibilityTime = 2.5f;

	/// <summary>
	/// If the player can jump or not.
	/// </summary>
	private bool canJump = true;

	/// <summary>
	/// If the character can perform an aerial attack or not.
	/// </summary>
	private bool hasAerialed = true;

	/// <summary>
	/// If the character is airborne or not.
	/// </summary>
	private bool airborne = false;

	/// <summary>
	/// The ground collider for the character.
	/// </summary>
	[Tooltip("The ground collider for the character.")]
	[SerializeField] private BoxCollider groundCollider = null;

	/// <summary>
	/// The animator for the character.
	/// </summary>
	private Animator animator = null;
	#endregion
	#region Public
	/// <summary>
	/// The characters audio script.
	/// </summary>
	[Tooltip("The characters audio script.")]
	public CharacterAudio characterAudio = null;

	/// <summary>
	/// The characters name.
	/// </summary>
	[Tooltip("The characters name.")]
	public string characterName = "Default Dan";

	/// <summary>
	/// The speed the character can go whilst grabbing.
	/// </summary>
	[Tooltip("The speed the character can go whilst grabbing.")]
	[SerializeField] public float speedWhilstGrabbing = 5.0f;

	/// <summary>
	/// The distance between footsteps.
	/// </summary>
	[Tooltip("The distance between footsteps.")]
	[SerializeField] public float footstepDistance = 1.5f;

	/// <summary>
	/// The current speed the character should go.
	/// </summary>
	[HideInInspector] public float currentSpeed = 25.0f;

	/// <summary>
	/// The current block amount of the character.
	/// </summary>
	[HideInInspector] public float currentBlock = 0.0f;

	/// <summary>
	/// The number that the controller used to control this character is.
	/// </summary>
	[Tooltip("The number that the controller used to control this character is.")]
	public int controllerNumber = 1;

	/// <summary>
	/// The fighter grabbed by this fighter.
	/// </summary>
	[HideInInspector] public FighterController grabbedFighter = null;

	/// <summary>
	/// The fighter grabbing this fighter.
	/// </summary>
	[HideInInspector] public FighterController fighterGrabbingThisFighter = null;

	/// <summary>
	/// The fighter rank.
	/// </summary>
	[HideInInspector] public int rank = 1;

	/// <summary>
	/// The current state that the character is in.
	/// </summary>
	[HideInInspector] public CharacterStates currentCharacterState = CharacterStates.Default;

	/// <summary>
	/// The rigid body of the character.
	/// </summary>
	[HideInInspector] public Rigidbody rb = null;

	/// <summary>
	/// The amount of damage the character has taken.
	/// </summary>
	[HideInInspector] public float damageTaken = 0.0f;

	/// <summary>
	/// True when the fighter is dead.
	/// </summary>
	[HideInInspector] public bool isDead = false;

	/// <summary>
	/// The amount of lives a character has.
	/// </summary>
	[HideInInspector] public int lives = 3;
	#endregion
	#endregion

	#region Structures
	/// <summary>
	/// All of the sates the character can be in.
	/// </summary>
	public enum CharacterStates
	{
		Default,
		ChargingOrRecover,
		ChargingOrRecoverNoMovement,
		Damaging,
		DamagingNoMovement,
		Interrupted,
		Clashed,
		Grabbing,
		Grabbed,
		Blocking,
		Dead,
		Respawning
	}
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the rigid body of the character.
	/// Sets the ground collider.
	/// Adds the controller number to the input strings.
	/// Sets the animator.
	/// </summary>
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		leftHorizontalAxis += controllerNumber;
		leftVerticalAxis += controllerNumber;
		rightHorizontalAxis += controllerNumber;
		rightVerticalAxis += controllerNumber;
		jumpButton += controllerNumber;
		lightAttackButton += controllerNumber;
		blockButton += controllerNumber;
		rightStick += controllerNumber;
		heavyAttackButton += controllerNumber;
		grabButton += controllerNumber;
		specialButton += controllerNumber;
		rightTriggerAxis += controllerNumber;
		leftTriggerAxis += controllerNumber;
		startButton += controllerNumber;
		backButton += controllerNumber;
		currentSpeed = speed;
		currentBlock = maxBlock;

		switch (controllerNumber)
		{
			case 1:
				playerIndicator.color = Color.blue;
				break;
			case 2:
				playerIndicator.color = Color.yellow;
				break;
			case 3:
				playerIndicator.color = Color.red;
				break;
			case 4:
				playerIndicator.color = Color.green;
				break;
			case 5:
				playerIndicator.color = Color.white;
				break;
			default:
				break;
		}

		animator = GetComponent<Animator>();
		locationOfLastFootstep = transform.localPosition;
		airborne = true;
	}

	/// <summary>
	/// Called when the player hits a kill wall.
	/// <param name="other">The object it collides with.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("KillWall") && currentCharacterState != CharacterStates.Dead)
		{
			Die();
		}
		else if (rb.velocity.magnitude > 25.0f && other.CompareTag("KillWallSpeedNorth") && currentCharacterState != CharacterStates.Dead)
		{
			GameObject silhouette = Instantiate(silhouettes[UnityEngine.Random.Range(0, silhouettes.Length)], null);
			silhouette.transform.position = transform.position;
			silhouette.transform.rotation = Quaternion.Euler(0.0f, 135.0f, UnityEngine.Random.Range(0.0f, 360.0f));
			Destroy(silhouette, 7.0f);
			Die();
		}
		else if (rb.velocity.magnitude > 25.0f && other.CompareTag("KillWallSpeedSouth") && currentCharacterState != CharacterStates.Dead)
		{
			GameObject silhouette = Instantiate(silhouettes[UnityEngine.Random.Range(0, silhouettes.Length)], null);
			silhouette.transform.position = transform.position;
			silhouette.transform.rotation = Quaternion.Euler(0.0f, 315.0f, UnityEngine.Random.Range(0.0f, 360.0f));
			Destroy(silhouette, 7.0f);
			Die();
		}
		else if (rb.velocity.magnitude > 25.0f && other.CompareTag("KillWallSpeedEast") && currentCharacterState != CharacterStates.Dead)
		{
			GameObject silhouette = Instantiate(silhouettes[UnityEngine.Random.Range(0, silhouettes.Length)], null);
			silhouette.transform.position = transform.position;
			silhouette.transform.rotation = Quaternion.Euler(0.0f, 45.0f, UnityEngine.Random.Range(0.0f, 360.0f));
			Destroy(silhouette, 7.0f);
			Die();
		}
		else if (rb.velocity.magnitude > 25.0f && other.CompareTag("KillWallSpeedWest") && currentCharacterState != CharacterStates.Dead)
		{
			GameObject silhouette = Instantiate(silhouettes[UnityEngine.Random.Range(0, silhouettes.Length)], null);
			silhouette.transform.position = transform.position;
			silhouette.transform.rotation = Quaternion.Euler(0.0f, 225.0f, UnityEngine.Random.Range(0.0f, 360.0f));
			Destroy(silhouette, 7.0f);
			Die();
		}
	}

	/// <summary>
	/// Called when the fighter dies.
	/// </summary>
	private void Die()
	{
		currentCharacterState = CharacterStates.Dead;
		lives--;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		if (lives > 0)
		{
			// Reset.
			damageTaken = 0.0f;
			currentBlock = maxBlock;
			currentRespawnInvincibilityTime = respawnInvincibilityTime;

			// Respawn.
			transform.localPosition = Vector3.zero;
			currentCharacterState = CharacterStates.Respawning;
			animator.Play("Respawning");
			locationOfLastFootstep = transform.localPosition;
			characterAudio.RespawnSound();
		}
		else
		{
			isDead = true;
			GameManager.Instance.deadPlayers++;
		}
	}

	/// <summary>
	/// Updates the character.
	/// </summary>
	private void Update()
	{
		if (GameManager.Instance.deadPlayers == GameManager.Instance.totalPlayers - 1)
		{
			currentCharacterState = CharacterStates.Dead;
		}

		animator.SetFloat("VerticalSpeed", rb.velocity.y);
		animator.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.z) + Mathf.Abs(rb.velocity.x));

		if (Input.GetButtonDown(startButton))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		if (Input.GetButtonDown(backButton))
		{
			int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

			if (sceneToLoad >= 7)
			{
				sceneToLoad = 3;
			}

			SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}

		switch (currentCharacterState)
		{
			case CharacterStates.Default:
				ApplyAttacks();
				currentSpeed = speed;
				ApplyMovement();
				HideBlock();
				break;
			case CharacterStates.ChargingOrRecover:
				ApplyMovement();
				HideBlock();
				break;
			case CharacterStates.ChargingOrRecoverNoMovement:
				HideBlock();
				break;
			case CharacterStates.Damaging:
				ApplyMovement();
				HideBlock();
				break;
			case CharacterStates.DamagingNoMovement:
				HideBlock();
				break;
			case CharacterStates.Interrupted:
				animator.Play("Interrupted");
				HideBlock();
				break;
			case CharacterStates.Clashed:
				animator.Play("Clashed");
				HideBlock();
				break;
			case CharacterStates.Grabbing:
				animator.Play("Grabbing");
				ApplyMovement(true);
				ApplyGrab();
				HideBlock();
				break;
			case CharacterStates.Grabbed:
				animator.Play("Struggling");
				ApplyStruggle();
				HideBlock();
				break;
			case CharacterStates.Blocking:
				animator.Play("Blocking");
				ApplyBlock();
				break;
			case CharacterStates.Dead:
				animator.Play("Dead");
				break;
			case CharacterStates.Respawning:
				currentRespawnInvincibilityTime -= Time.deltaTime;
				if (currentRespawnInvincibilityTime < 0.0f)
				{
					currentCharacterState = CharacterStates.Default;
					animator.Play("Idle");
				}
				ApplyAttacks();
				ApplyMovement();
				HideBlock();
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Makes the block hidden.
	/// Recharges the block.
	/// Clamps the block.
	/// </summary>
	private void HideBlock()
	{
		// Hide the block object.
		blockObject.localScale = Vector3.Lerp(blockObject.localScale, Vector3.zero, 25.0f * Time.deltaTime);

		// Recharge block.
		currentBlock += 10.0f * Time.deltaTime;
		currentBlock = Mathf.Clamp(currentBlock, 0.0f, maxBlock);
	}

	/// <summary>
	/// Remaps one float range to another.
	/// </summary>
	/// <param name="value">The input value.</param>
	/// <param name="from1">The starting value of the current range.</param>
	/// <param name="to1">The end value of the current range.</param>
	/// <param name="from2">The starting value of the new range.</param>
	/// <param name="to2">The end value of the new range.</param>
	/// <returns>The value remapped to a new range.</returns>
	public float Remap(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	/// <summary>
	/// Shows the block object.
	/// Decreases the current block amount slowly.
	/// Checks to see if the block is broken.
	/// Checks to see if the user still wants to block or not.
	/// </summary>
	private void ApplyBlock()
	{
		if (currentBlock <= 0.0f)
		{
			currentCharacterState = CharacterStates.Interrupted;
			characterAudio.ShieldBreakSound();
		}
		else if (Input.GetButtonUp(blockButton))
		{
			currentCharacterState = CharacterStates.Default;
			animator.Play("Idle");
		}
		else
		{
			// Show the block object.
			float currentScale = Remap(currentBlock, 0.0f, maxBlock, 1.0f, 3.0f);
			blockObject.localScale = Vector3.Lerp(blockObject.localScale, new Vector3(currentScale, currentScale, currentScale), 25.0f * Time.deltaTime);

			// Change the color of the block object.
			currentScale = Remap(currentBlock, 0.0f, maxBlock, 0.0f, 1.0f);
			blockMeshRenderer.material.color = Color.Lerp(Color.red, Color.green, currentScale);
			blockMeshRenderer.material.color = new Color(blockMeshRenderer.material.color.r, blockMeshRenderer.material.color.g, blockMeshRenderer.material.color.b, 0.5f);

			// Decrease block.
			currentBlock -= 10.0f * Time.deltaTime;
			currentBlock = Mathf.Clamp(currentBlock, 0.0f, maxBlock);
		}
	}

	/// <summary>
	/// Applies the jump and gravity.
	/// </summary>
	private void FixedUpdate()
	{
		if (jumpRequest)
		{
			rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
			jumpRequest = false;
		}

		if (rb.velocity.y < 0.0f)
		{
			gravityScale = fallMultiplier;
		}
		else if (rb.velocity.y > 0.0f && !Input.GetButton(jumpButton))
		{
			gravityScale = lowJumpMultiplier;
		}
		else
		{
			gravityScale = 1.0f;
		}

		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		rb.AddForce(gravity, ForceMode.Acceleration);
	}

	/// <summary>
	/// Allows the players input to effect the character.
	/// </summary>
	/// <param name="noJump">True when you don't want the character to be able to jump.</param>
	private void ApplyMovement(bool noJump = false)
	{
		float leftHorizontal = Input.GetAxis(leftHorizontalAxis);
		float leftVertical = Input.GetAxis(leftVerticalAxis);
		Move(leftHorizontal, leftVertical);
		Turn(leftHorizontal, leftVertical);

		// If distance travelled is greater or equal to the footstep threshhold and is grounded play a fotstep noise and reset the distance travelled.
		if (!airborne && Mathf.Abs(Vector3.Distance(locationOfLastFootstep, transform.localPosition)) >= footstepDistance)
		{
			characterAudio.FootstepSound();
			locationOfLastFootstep = transform.localPosition;
		}

		if (!noJump)
		{
			Jump();
		}
	}

	/// <summary>
	/// Applies the grab maneuver.
	/// </summary>
	private void ApplyGrab()
	{
		if (currentGrabThreshold <= 0.0f)
		{
			SetState(FighterController.CharacterStates.Interrupted);
			grabbedFighter.SetState(FighterController.CharacterStates.Default);
			grabbedFighter.animator.Play("Idle");
			grabbedFighter.fighterGrabbingThisFighter = null;
			grabbedFighter = null;
			currentSpeed = speed;
		}
		else
		{
			grabbedFighter.transform.position = grabLocation.transform.position;

			float horizontal = Input.GetAxis(rightHorizontalAxis);
			float vertical = Input.GetAxis(rightVerticalAxis);
			float magnitude = Mathf.Clamp01(new Vector2(horizontal, vertical).magnitude);

			if (magnitude >= 0.95f)
			{
				animator.Play("Throw");
				grabbedFighter.SetState(FighterController.CharacterStates.Default);
				grabbedFighter.animator.Play("Idle");
				grabbedFighter.fighterGrabbingThisFighter = null;
				grabbedFighter.ApplyThrow(horizontal, vertical);
				grabbedFighter.characterAudio.ThrownSound();
				grabbedFighter = null;
				currentSpeed = speed;
				characterAudio.ThrowingSound();
				SetState(CharacterStates.ChargingOrRecoverNoMovement);
			}
			else if (Input.GetButtonDown(rightStick))
			{
				animator.Play("Throw");
				grabbedFighter.SetState(FighterController.CharacterStates.Default);
				grabbedFighter.animator.Play("Idle");
				grabbedFighter.fighterGrabbingThisFighter = null;
				grabbedFighter.ApplyThrow(0.0f, 0.0f);
				grabbedFighter.characterAudio.ThrownSound();
				grabbedFighter = null;
				currentSpeed = speed;
				characterAudio.ThrowingSound();
				SetState(CharacterStates.ChargingOrRecoverNoMovement);
			}
		}
	}

	/// <summary>
	/// Applies the struggle.
	/// </summary>
	private void ApplyStruggle()
	{
		if (Input.GetButtonDown(jumpButton) || Input.GetButtonDown(lightAttackButton) || Input.GetButtonDown(heavyAttackButton) || Input.GetButtonDown(blockButton))
		{
			fighterGrabbingThisFighter.currentGrabThreshold -= throwPower + fighterGrabbingThisFighter.damageTaken;
			characterAudio.StruggleSound();
		}
	}

	/// <summary>
	/// Allows the players input to effect the characters attacks.
	/// </summary>
	private void ApplyAttacks()
	{
		float leftHorizontal = Input.GetAxis(leftHorizontalAxis);
		float leftVertical = Input.GetAxis(leftVerticalAxis);
		float rightTrigger = Input.GetAxisRaw(rightTriggerAxis);
		float leftTrigger = Input.GetAxisRaw(leftTriggerAxis);

		#region Light Attacks
		// Special aerial moving light.
		if (!hasAerialed && !canJump && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && airborne && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(lightAttackButton))
		{
			SpecialAerialMovingLightAttack();
		}
		// Special aerial light.
		else if (!hasAerialed && !canJump && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && airborne && Input.GetButtonDown(lightAttackButton))
		{
			SpecialAerialLightAttack();
		}
		// Aerial moving light.
		else if (!hasAerialed && !canJump && airborne && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(lightAttackButton))
		{
			AerialMovingLightAttack();
		}
		// Aerial light.
		else if (!hasAerialed && !canJump && airborne && Input.GetButtonDown(lightAttackButton))
		{
			AerialLightAttack();
		}
		// Special moving light.
		else if (!hasAerialed && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(lightAttackButton))
		{
			SpecialMovingLightAttack();
		}
		// Special light.
		else if (!hasAerialed && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && Input.GetButtonDown(lightAttackButton))
		{
			SpecialLightAttack();
		}
		// Moving light.
		else if (!hasAerialed && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(lightAttackButton))
		{
			MovingLightAttack();
		}
		// Light.
		else if (!hasAerialed && Input.GetButtonDown(lightAttackButton))
		{
			LightAttack();
		}
		#endregion

		#region Heavy Attacks
		// Special aerial moving heavy.
		if (!hasAerialed && !canJump && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && airborne && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(heavyAttackButton))
		{
			SpecialAerialMovingHeavyAttack();
		}
		// Special aerial heavy.
		else if (!hasAerialed && !canJump && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && airborne && Input.GetButtonDown(heavyAttackButton))
		{
			SpecialAerialHeavyAttack();
		}
		// Aerial moving heavy.
		else if (!hasAerialed && !canJump && airborne && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(heavyAttackButton))
		{
			AerialMovingHeavyAttack();
		}
		// Aerial heavy.
		else if (!hasAerialed && !canJump && airborne && Input.GetButtonDown(heavyAttackButton))
		{
			AerialHeavyAttack();
		}
		// Special moving heavy.
		else if (!hasAerialed && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(heavyAttackButton))
		{
			SpecialMovingHeavyAttack();
		}
		// Special heavy.
		else if (!hasAerialed && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && Input.GetButtonDown(heavyAttackButton))
		{
			SpecialHeavyAttack();
		}
		// Moving heavy.
		else if (!hasAerialed && (leftHorizontal != 0 || leftVertical != 0) && Input.GetButtonDown(heavyAttackButton))
		{
			MovingHeavyAttack();
		}
		// Heavy.
		else if (!hasAerialed && Input.GetButtonDown(heavyAttackButton))
		{
			HeavyAttack();
		}
		#endregion

		// Aerial.
		if (!hasAerialed && (Input.GetButton(specialButton) || rightTrigger != 0.0f) && Input.GetButtonDown(jumpButton))
		{
			Aerial();
		}

		// Grab.
		if (!hasAerialed && Input.GetButtonDown(grabButton))
		{
			Grab();
		}

		// Block.
		if (!hasAerialed && Input.GetButtonDown(blockButton))
		{
			Block();
		}
	}

	/// <summary>
	/// Moves the character depending on their players input.
	/// </summary>
	/// <param name="horizontal">The direction of horizontal movement.</param>
	/// <param name="vertical">The direction of vertical movement.</param>
	private void Move(float horizontal, float vertical)
	{
		rb.AddForce(Vector3.back * vertical * currentSpeed, ForceMode.Force);
		rb.AddForce(Vector3.right * horizontal * currentSpeed, ForceMode.Force);
	}

	/// <summary>
	/// Applies a jump if applicable.
	/// </summary>
	private void Jump()
	{
		if (Input.GetButtonDown(jumpButton) && canJump)
		{
			groundCollider.enabled = false;
			Invoke("TurnBackOnGroundCollider", 0.25f);
			jumpRequest = true;
			canJump = false;
			airborne = true;
			animator.Play("Jump");
			animator.SetBool("Grounded", false);
			characterAudio.JumpSound();
		}
	}

	/// <summary>
	/// turns the ground collider back on.
	/// </summary>
	private void TurnBackOnGroundCollider()
	{
		groundCollider.enabled = true;
	}

	/// <summary>
	/// Turns the character depending on their players input.
	/// </summary>
	/// <param name="horizontal">The direction of horizontal looking.</param>
	/// <param name="vertical">The direction of vertical looking.</param>
	private void Turn(float horizontal, float vertical)
	{
		Vector3 targetDirection = new Vector3(horizontal, 0.0f, -vertical);

		if (targetDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			transform.rotation = targetRotation;
		}
	}

	/// <summary>
	/// Adds a forward force to the character.
	/// Used in animations.
	/// </summary>
	/// <param name="amount">The amount of force to apply.</param>
	private void AddForceForward(float amount)
	{
		rb.AddForce(transform.forward * amount, ForceMode.Impulse);
	}

	/// <summary>
	/// Adds a upward force to the character.
	/// Used in animations.
	/// </summary>
	/// <param name="amount">The amount of force to apply.</param>
	private void AddForceUp(float amount)
	{
		rb.AddForce(transform.up * amount, ForceMode.Impulse);
	}

	/// <summary>
	/// Adds a downward force to the character.
	/// Used in animations.
	/// </summary>
	/// <param name="amount">The amount of force to apply.</param>
	private void AddForceDown(float amount)
	{
		rb.AddForce(-transform.up * amount, ForceMode.Impulse);
	}

	/// <summary>
	/// Adds a backwards force to the character.
	/// Used in animations.
	/// </summary>
	/// <param name="amount">The amount of force to apply.</param>
	private void AddForceBack(float amount)
	{
		rb.AddForce(-transform.forward * amount, ForceMode.Impulse);
	}

	#region Attacks
	#region Light
	/// <summary>
	/// Makes the character perform a special aerial moving light attack.
	/// </summary>
	private void SpecialAerialMovingLightAttack()
	{
		animator.Play("SpecialAerialMovingLight");
	}

	/// <summary>
	/// Makes the character perform a special aerial light attack.
	/// </summary>
	private void SpecialAerialLightAttack()
	{
		animator.Play("SpecialAerialLight");
	}

	/// <summary>
	/// Makes the character perform a aerial moving light attack.
	/// </summary>
	private void AerialMovingLightAttack()
	{
		animator.Play("AerialMovingLight");
	}

	/// <summary>
	/// Makes the character perform a aerial light attack.
	/// </summary>
	private void AerialLightAttack()
	{
		animator.Play("AerialLight");
	}

	/// <summary>
	/// Makes the character perform a special moving light attack.
	/// </summary>
	private void SpecialMovingLightAttack()
	{
		animator.Play("SpecialMovingLight");
	}

	/// <summary>
	/// Makes the character perform a special light attack.
	/// </summary>
	private void SpecialLightAttack()
	{
		animator.Play("SpecialLight");
	}

	/// <summary>
	/// Makes the character perform a moving light attack.
	/// </summary>
	private void MovingLightAttack()
	{
		animator.Play("MovingLight");
	}

	/// <summary>
	/// Makes the character perform a light attack.
	/// </summary>
	private void LightAttack()
	{
		animator.Play("Light");
	}
	#endregion
	#region Heavy
	/// <summary>
	/// Makes the character perform a special aerial moving heavy attack.
	/// </summary>
	private void SpecialAerialMovingHeavyAttack()
	{
		animator.Play("SpecialAerialMovingHeavy");
	}

	/// <summary>
	/// Makes the character perform a special aerial heavy attack.
	/// </summary>
	private void SpecialAerialHeavyAttack()
	{
		animator.Play("SpecialAerialHeavy");
	}

	/// <summary>
	/// Makes the character perform a aerial moving heavy attack.
	/// </summary>
	private void AerialMovingHeavyAttack()
	{
		animator.Play("AerialMovingHeavy");
	}

	/// <summary>
	/// Makes the character perform a aerial heavy attack.
	/// </summary>
	private void AerialHeavyAttack()
	{
		animator.Play("AerialHeavy");
	}

	/// <summary>
	/// Makes the character perform a special moving heavy attack.
	/// </summary>
	private void SpecialMovingHeavyAttack()
	{
		animator.Play("SpecialMovingHeavy");
	}

	/// <summary>
	/// Makes the character perform a special heavy attack.
	/// </summary>
	private void SpecialHeavyAttack()
	{
		animator.Play("SpecialHeavy");
	}

	/// <summary>
	/// Makes the character perform a moving heavy attack.
	/// </summary>
	private void MovingHeavyAttack()
	{
		animator.Play("MovingHeavy");
	}

	/// <summary>
	/// Makes the character perform a heavy attack.
	/// </summary>
	private void HeavyAttack()
	{
		animator.Play("Heavy");
	}
	#endregion

	/// <summary>
	/// Makes the character perform a aerial attack.
	/// </summary>
	private void Aerial()
	{
		groundCollider.enabled = false;
		Invoke("TurnBackOnGroundCollider", 0.25f);
		hasAerialed = true;
		canJump = false;
		animator.Play("Aerial");
	}

	/// <summary>
	/// Makes the character perform a block.
	/// </summary>
	private void Block()
	{
		if (currentCharacterState != CharacterStates.Blocking)
		{
			currentCharacterState = CharacterStates.Blocking;
			characterAudio.ShieldSound();
		}
	}

	/// <summary>
	/// Makes the character perform a grab.
	/// </summary>
	private void Grab()
	{
		currentGrabThreshold = grabThreshold;
		animator.Play("Grab");
	}
	#endregion
	#endregion
	#region Public
	/// <summary>
	/// Used in animation events to set the state of the character.
	/// </summary>
	/// <param name="characterStates">The state to set the character to.</param>
	public void SetState(CharacterStates characterStates)
	{
		currentCharacterState = characterStates;
	}

	/// <summary>
	/// The ground check for the character.
	/// </summary>
	/// <param name="other">The object it collides with.</param>
	public void TriggerStay(Collider other)
	{
		if (other.CompareTag("Ground"))
		{
			canJump = true;
			airborne = false;
			hasAerialed = false;
			animator.SetBool("Grounded", true);
		}
	}

	/// <summary>
	/// The initial ground check for the character.
	/// </summary>
	/// <param name="other">The object it collides with.</param>
	public void TriggerEnter(Collider other)
	{
		if (other.CompareTag("Ground"))
		{
			locationOfLastFootstep = transform.localPosition;
			characterAudio.LandSound();
		}
	}

	/// <summary>
	/// Applies the throw in a particular direction.
	/// </summary>
	/// <param name="vertical">The vertical amount.</param>
	/// <param name="horizontal">The horizontal amount.</param>
	public void ApplyThrow(float vertical, float horizontal)
	{
		if (vertical == 0.0f && horizontal == 0.0f)
		{
			if (damageTaken == 0.0f)
			{
				rb.AddForce(Vector3.up * throwPower * 1.0f, ForceMode.Impulse);
			}
			else
			{
				rb.AddForce(Vector3.up * throwPower * damageTaken, ForceMode.Impulse);
			}
		}
		else
		{
			if (damageTaken == 0.0f)
			{
				rb.AddForce(Vector3.back * vertical * throwPower * 1.0f, ForceMode.Impulse);
				rb.AddForce(Vector3.right * horizontal * throwPower * 1.0f, ForceMode.Impulse);
			}
			else
			{
				rb.AddForce(Vector3.back * vertical * throwPower * damageTaken, ForceMode.Impulse);
				rb.AddForce(Vector3.right * horizontal * throwPower * damageTaken, ForceMode.Impulse);
			}
		}
	}
	#endregion
	#endregion
}