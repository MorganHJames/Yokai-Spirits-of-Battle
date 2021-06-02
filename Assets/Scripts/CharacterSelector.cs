////////////////////////////////////////////////////////////
// File: CharacterSelector.cs
// Author: Morgan Henry James
// Date Created: 01-04-2020
// Brief: Allows the player to join the game and pick a character to play.
//////////////////////////////////////////////////////////// 

using TMPro;
using UnityEngine;

/// <summary>
/// Allows the player to join the game and pick a character to play.
/// </summary>
public class CharacterSelector : MonoBehaviour
{
	#region Structures
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// An available character.
	/// </summary>
	[System.Serializable]
	public struct AvailableCharacter
	{
		public GameObject previewModel;
		public GameObject characterPrefab;
	}
	#endregion
	#endregion

	#region Variables
	#region Private
	/// <summary>
	/// All of the available characters.
	/// </summary>
	[Tooltip("All of the available characters.")]
	[SerializeField] private AvailableCharacter[] availableCharacters = null;

	/// <summary>
	/// The screen transition script.
	/// </summary>
	[Tooltip("The screen transition script.")]
	[SerializeField] private ScreenTransition screenTransition = null;

	/// <summary>
	/// The current character index.
	/// </summary>
	private int currentCharacterIndex = 0;

	/// <summary>
	/// The number that the controller used to control this character is.
	/// </summary>
	[Tooltip("The number that the controller used to control this character is.")]
	[SerializeField] private int controllerNumber = 1;

	/// <summary>
	/// The Press A to join animator.
	/// </summary>
	[Tooltip("The Press A to join animator.")]
	[SerializeField] private Animator aToJoinAnimator = null;

	/// <summary>
	/// The Press B to leave animator.
	/// </summary>
	[Tooltip("The Press B to leave animator.")]
	[SerializeField] private Animator bToLeaveAnimator = null;

	/// <summary>
	/// The character name animator.
	/// </summary>
	[Tooltip("The character name animator.")]
	[SerializeField] private Animator characterNameAnimator = null;

	/// <summary>
	/// The arrows animator.
	/// </summary>
	[Tooltip("The arrows animator.")]
	[SerializeField] private Animator arrowsAnimator = null;

	/// <summary>
	/// The character spawn location.
	/// </summary>
	[Tooltip("The character spawn location.")]
	[SerializeField] private Transform characterSpawnLocation = null;

	/// <summary>
	/// The character's name.
	/// </summary>
	[Tooltip("The character's name.")]
	[SerializeField] private TextMeshProUGUI characterName = null;

	/// <summary>
	/// The character's ready overlay.
	/// </summary>
	[Tooltip("The character's ready overlay.")]
	[SerializeField] private GameObject readyOverlay = null;

	/// <summary>
	/// The fight overlay.
	/// </summary>
	[Tooltip("The fight overlay.")]
	[SerializeField] private GameObject fightOverlay = null;

	/// <summary>
	/// The preview character.
	/// </summary>
	private GameObject previewCharacter = null;

	/// <summary>
	/// The A button.
	/// </summary>
	private string aButton = "Jump";

	/// <summary>
	/// The B button.
	/// </summary>
	private string bButton = "Light";

	/// <summary>
	/// The horizontal axis for the left stick.
	/// </summary>
	private string leftHorizontalAxis = "LeftHorizontal";

	/// <summary>
	/// The horizontal axis for the right stick.
	/// </summary>
	private string rightHorizontalAxis = "RightVertical";

	/// <summary>
	/// The time remaining until the player can pick a new character.
	/// </summary>
	private float currentWaitTime = 0.5f;

	/// <summary>
	/// The time the user must wait before picking a new character.
	/// </summary>
	private float waitTime = 0.5f;

	/// <summary>
	/// The character info.
	/// </summary>
	private GameManager.CharacterInfo characterInfo = new GameManager.CharacterInfo();

	/// <summary>
	/// The states that the character selector can be in.
	/// </summary>
	private enum States
	{
		NotJoined,
		PickingCharacter,
		ChangingCharacter,
		Ready
	}

	/// <summary>
	/// The current state of character selection.
	/// </summary>
	private States currentState = States.NotJoined;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets up the buttons.
	/// </summary>
	private void Start()
	{
		aButton += controllerNumber;
		bButton += controllerNumber;
		leftHorizontalAxis += controllerNumber;
		rightHorizontalAxis += controllerNumber;

		GameManager.Instance.deadPlayers = 0;
		GameManager.Instance.totalPlayers = 0;

		// If connect ready contains this remove.
		if (GameManager.Instance.connectedPlayersReady.Contains(controllerNumber))
		{
			GameManager.Instance.connectedPlayersReady.Remove(controllerNumber);
		}

		for (int i = 0; i < GameManager.Instance.characterInfos.Count; i++)
		{
			if(GameManager.Instance.characterInfos[i].ControllerNumber == controllerNumber)
			{
				for (int k = 0; k < availableCharacters.Length; k++)
				{
					if (GameManager.Instance.characterInfos[i].CharacterPrefab == availableCharacters[k].characterPrefab)
					{
						currentCharacterIndex = k;

						// A to join outro.
						aToJoinAnimator.Play("AToJoinOutro");

						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Accept);

						// Spawn random character.
						previewCharacter = Instantiate(availableCharacters[currentCharacterIndex].previewModel);
						previewCharacter.transform.parent = characterSpawnLocation;
						previewCharacter.transform.localPosition = Vector3.zero;
						previewCharacter.transform.localRotation = Quaternion.identity;
						previewCharacter.transform.localPosition = new Vector3(previewCharacter.transform.localPosition.x, previewCharacter.transform.localPosition.y - 1.75f, previewCharacter.transform.localPosition.z);

						// Fill in name.
						characterName.text = availableCharacters[currentCharacterIndex].characterPrefab.GetComponent<FighterController>().characterName;

						// Play arrow intro.
						arrowsAnimator.Play("ArrowsIntro");

						// Play name intro.
						characterNameAnimator.Play("CharacterNameIntro");

						// Play b to leave intro.
						bToLeaveAnimator.Play("BToLeaveIntro");
						currentWaitTime = waitTime;
						currentState = States.PickingCharacter;
					}
				}
			}
		}

		GameManager.Instance.reloadedPlayers++;
		if (GameManager.Instance.reloadedPlayers == 4)
		{
			GameManager.Instance.characterInfos.Clear();
			GameManager.Instance.reloadedPlayers = 0;
		}
	}

	/// <summary>
	/// Handles the character selection and player joining.
	/// </summary>
	private void Update()
	{
		switch (currentState)
		{
			case States.NotJoined:
				currentWaitTime -= Time.deltaTime;
				currentWaitTime = Mathf.Clamp01(currentWaitTime);
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(aButton))
				{
					// A to join outro.
					aToJoinAnimator.Play("AToJoinOutro");

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Accept);

					// Spawn random character.
					currentCharacterIndex = Random.Range(0, availableCharacters.Length);
					previewCharacter = Instantiate(availableCharacters[currentCharacterIndex].previewModel);
					previewCharacter.transform.parent = characterSpawnLocation;
					previewCharacter.transform.localPosition = Vector3.zero;
					previewCharacter.transform.localRotation = Quaternion.identity;
					previewCharacter.transform.localPosition = new Vector3(previewCharacter.transform.localPosition.x, previewCharacter.transform.localPosition.y - 1.75f, previewCharacter.transform.localPosition.z);

					// Fill in name.
					characterName.text = availableCharacters[currentCharacterIndex].characterPrefab.GetComponent<FighterController>().characterName;

					// Play arrow intro.
					arrowsAnimator.Play("ArrowsIntro");

					// Play name intro.
					characterNameAnimator.Play("CharacterNameIntro");

					// Play b to leave intro.
					bToLeaveAnimator.Play("BToLeaveIntro");
					currentWaitTime = waitTime;
					GameManager.Instance.connectedPlayers.Add(controllerNumber);
					currentState = States.PickingCharacter;
				}
				break;
			case States.PickingCharacter:
				float leftHorizontal = Input.GetAxis(leftHorizontalAxis);
				float rightHorizontal = Input.GetAxis(rightHorizontalAxis);

				// Pick the character an index down.
				if (leftHorizontal <= -0.9f)
				{
					// Despawn current character.
					previewCharacter.GetComponent<PreviewScaling>().ScaleDown();

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Swap);

					// Spawn character an index down.
					currentCharacterIndex -= 1;

					if (currentCharacterIndex < 0)
					{
						currentCharacterIndex = availableCharacters.Length - 1;
					}

					previewCharacter = Instantiate(availableCharacters[currentCharacterIndex].previewModel);
					previewCharacter.transform.parent = characterSpawnLocation;
					previewCharacter.transform.localPosition = Vector3.zero;
					previewCharacter.transform.localRotation = Quaternion.identity;
					previewCharacter.transform.localPosition = new Vector3(previewCharacter.transform.localPosition.x, previewCharacter.transform.localPosition.y - 1.75f, previewCharacter.transform.localPosition.z);

					// Fill in name.
					characterName.text = availableCharacters[currentCharacterIndex].characterPrefab.GetComponent<FighterController>().characterName;

					currentWaitTime = waitTime;
					currentState = States.ChangingCharacter;
				}
				// Pick the character an index up.
				else if (leftHorizontal >= 0.9f)
				{
					// Despawn current character.
					previewCharacter.GetComponent<PreviewScaling>().ScaleDown();

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Swap);

					// Spawn character an index up.
					currentCharacterIndex++;

					if (currentCharacterIndex >= availableCharacters.Length)
					{
						currentCharacterIndex = 0;
					}

					previewCharacter = Instantiate(availableCharacters[currentCharacterIndex].previewModel);
					previewCharacter.transform.parent = characterSpawnLocation;
					previewCharacter.transform.localPosition = Vector3.zero;
					previewCharacter.transform.localRotation = Quaternion.identity;
					previewCharacter.transform.localPosition = new Vector3(previewCharacter.transform.localPosition.x, previewCharacter.transform.localPosition.y - 1.75f, previewCharacter.transform.localPosition.z);

					// Fill in name.
					characterName.text = availableCharacters[currentCharacterIndex].characterPrefab.GetComponent<FighterController>().characterName;

					currentWaitTime = waitTime;
					currentState = States.ChangingCharacter;
				}

				// Rotate the character left.
				if (rightHorizontal <= -0.9f)
				{
					previewCharacter.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * 250.0f);
				}
				// Rotate the character right.
				else if (rightHorizontal >= 0.9f)
				{
					previewCharacter.transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * 250.0f);
				}

				// Confirm character.
				currentWaitTime -= Time.deltaTime;
				currentWaitTime = Mathf.Clamp01(currentWaitTime);
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(aButton))
				{
					// If all active players are ready show the start banner.
					GameManager.Instance.connectedPlayersReady.Add(controllerNumber);

					if (GameManager.Instance.connectedPlayersReady.Count == GameManager.Instance.connectedPlayers.Count)
					{
						fightOverlay.SetActive(true);

						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.AllReady);
					}
					else
					{
						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Accept);
					}

					// Play arrow outro.
					arrowsAnimator.Play("ArrowsOutro");

					// Play b to leave outro.
					bToLeaveAnimator.Play("BToLeaveOutro");

					readyOverlay.SetActive(true);
					currentWaitTime = waitTime;

					characterInfo = new GameManager.CharacterInfo();
					characterInfo.CharacterPrefab = availableCharacters[currentCharacterIndex].characterPrefab;
					characterInfo.ControllerNumber = controllerNumber;
					GameManager.Instance.characterInfos.Add(characterInfo);

					currentState = States.Ready;
				}

				// Back out.
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(bButton))
				{
					// A to join intro.
					aToJoinAnimator.Play("AToJoinIntro");

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Reject);

					// Despawn current character.
					previewCharacter.GetComponent<PreviewScaling>().ScaleDown();

					// Play name outro.
					characterNameAnimator.Play("CharacterNameOutro");

					// Play arrow outro.
					arrowsAnimator.Play("ArrowsOutro");

					// Play b to leave outro.
					bToLeaveAnimator.Play("BToLeaveOutro");

					currentWaitTime = waitTime;
					GameManager.Instance.connectedPlayers.Remove(controllerNumber);
					currentState = States.NotJoined;
				}
				break;
			case States.ChangingCharacter:
				currentWaitTime -= Time.deltaTime;
				currentWaitTime = Mathf.Clamp01(currentWaitTime);

				if (currentWaitTime <= 0.0f)
				{
					currentState = States.PickingCharacter;
				}
				break;
			case States.Ready:
				currentWaitTime -= Time.deltaTime;
				currentWaitTime = Mathf.Clamp01(currentWaitTime);

				// If all ready and a pressed go to map select.
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(aButton) && GameManager.Instance.connectedPlayersReady.Count == GameManager.Instance.connectedPlayers.Count)
				{
					screenTransition.ScreenTransistion(1.0f, "MapSelect");

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Start);
				}

				// Back out.
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(bButton))
				{
					GameManager.Instance.characterInfos.Remove(characterInfo);

					// If all active players are ready show the start banner.
					GameManager.Instance.connectedPlayersReady.Remove(controllerNumber);

					if (GameManager.Instance.connectedPlayersReady.Count != GameManager.Instance.connectedPlayers.Count)
					{
						fightOverlay.SetActive(false);
					}

					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Reject);

					// Play arrow intro.
					arrowsAnimator.Play("ArrowsIntro");

					// Play b to leave intro.
					bToLeaveAnimator.Play("BToLeaveIntro");

					readyOverlay.SetActive(false);
					currentWaitTime = waitTime;
					currentState = States.PickingCharacter;
				}
				break;
			default:
				break;
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}