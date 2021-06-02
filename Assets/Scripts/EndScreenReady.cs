////////////////////////////////////////////////////////////
// File: EndScreenReady.cs
// Author: Morgan Henry James
// Date Created: 14-04-2020
// Brief: Controls how the characters ready up after completing a match.
//////////////////////////////////////////////////////////// 

using TMPro;
using UnityEngine;

/// <summary>
/// Controls how the characters ready up after completing a match.
/// </summary>
public class EndScreenReady : MonoBehaviour
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
	/// The character name animator.
	/// </summary>
	[Tooltip("The character name animator.")]
	[SerializeField] private Animator characterNameAnimator = null;

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
	/// The character's rank.
	/// </summary>
	[Tooltip("The character's rank.")]
	[SerializeField] private TextMeshProUGUI rank = null;

	/// <summary>
	/// The character's ready overlay.
	/// </summary>
	[Tooltip("The character's ready overlay.")]
	[SerializeField] private GameObject readyOverlay = null;

	/// <summary>
	/// The Press A to join animator.
	/// </summary>
	[Tooltip("The Press A to join animator.")]
	[SerializeField] private Animator aToJoinAnimator = null;

	/// <summary>
	/// The fighter controller that this script handles the UI for.
	/// </summary>
	[Tooltip("The fighter controller that this script handles the UI for.")]
	[HideInInspector] public FighterController fighterController = null;

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
	/// The states that the readyer selector can be in.
	/// </summary>
	private enum States
	{
		NotReady,
		Ready
	}

	/// <summary>
	/// The current state of character selection.
	/// </summary>
	private States currentState = States.NotReady;
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
		rightHorizontalAxis += controllerNumber;
		GameManager.Instance.readyToEndPlayers = 0;

		for (int i = 0; i < GameManager.Instance.characterInfos.Count; i++)
		{
			if (GameManager.Instance.characterInfos[i].ControllerNumber == controllerNumber)
			{
				for (int k = 0; k < availableCharacters.Length; k++)
				{
					if (GameManager.Instance.characterInfos[i].CharacterPrefab == availableCharacters[k].characterPrefab)
					{
						currentCharacterIndex = k;

						// A to join outro.
						aToJoinAnimator.gameObject.SetActive(true);
						aToJoinAnimator.Play("AToJoinIntro");

						// Spawn random character.
						previewCharacter = Instantiate(availableCharacters[currentCharacterIndex].previewModel);
						previewCharacter.transform.parent = characterSpawnLocation;
						previewCharacter.transform.localPosition = Vector3.zero;
						previewCharacter.transform.localRotation = Quaternion.identity;
						previewCharacter.transform.localPosition = new Vector3(previewCharacter.transform.localPosition.x, previewCharacter.transform.localPosition.y - 1.0f, previewCharacter.transform.localPosition.z);

						// Fill in rank.
						switch (fighterController.rank)
						{
							case 1:
								rank.text = "1st";
								break;
							case 2:
								rank.text = "2nd";
								break;
							case 3:
								rank.text = "3rd";
								break;
							case 4:
								rank.text = "4th";
								break;
							default:
								break;
						}

						// Fill in name.
						characterName.text = availableCharacters[currentCharacterIndex].characterPrefab.GetComponent<FighterController>().characterName;

						// Play name intro.
						characterNameAnimator.Play("CharacterNameIntro");
					}
				}
			}
		}
	}

	/// <summary>
	/// Handles the character selection and player joining.
	/// </summary>
	private void Update()
	{
		float rightHorizontal = Input.GetAxis(rightHorizontalAxis);
		switch (currentState)
		{
			case States.NotReady:
				currentWaitTime -= Time.deltaTime;
				currentWaitTime = Mathf.Clamp01(currentWaitTime);
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(aButton))
				{
					// A to join outro.
					aToJoinAnimator.Play("AToJoinOutro");

					readyOverlay.SetActive(true);

					currentWaitTime = waitTime;

					currentState = States.Ready;

					GameManager.Instance.readyToEndPlayers++;

					// If all ready and a pressed go to map select.
					if (Input.GetButtonDown(aButton) && GameManager.Instance.readyToEndPlayers == GameManager.Instance.connectedPlayers.Count)
					{
						screenTransition.ScreenTransistion(1.0f, "CharacterSelect");
						GameManager.Instance.readyToEndPlayers = 0;

						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.AllReady);
					}
					else
					{
						AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Accept);
					}
					break;
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
				break;
			case States.Ready:
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

				// Back out.
				if (currentWaitTime <= 0.0f && Input.GetButtonDown(bButton))
				{
					AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Reject);

					// A to join intro.
					aToJoinAnimator.Play("AToJoinIntro");

					currentWaitTime = waitTime;

					GameManager.Instance.readyToEndPlayers--;
					currentState = States.NotReady;
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