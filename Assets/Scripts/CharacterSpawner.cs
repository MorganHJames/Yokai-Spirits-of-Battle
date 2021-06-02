////////////////////////////////////////////////////////////
// File: CharacterSpawner.cs
// Author: Morgan Henry James
// Date Created: 29-03-2020
// Brief: Spawns the character on game start.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Spawns the character on game start.
/// </summary>
public class CharacterSpawner : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The number of the player to spawn at this location.
	/// </summary>
	[Tooltip("The number of the player to spawn at this location.")]
	[SerializeField] private int playerNumber = 0;

	/// <summary>
	/// The off screen arrow handler that should point to the character that this script spawns.
	/// </summary>
	[Tooltip("The off screen arrow handler that should point to the character that this script spawns.")]
	[SerializeField] private OffScreenArrowHandler offScreenArrowHandler = null;

	/// <summary>
	/// The stock and damage indicator handler for this character.
	/// </summary>
	[Tooltip("The stock and damage indicator handler for this character.")]
	[SerializeField] private StockAndDamageIndicatorHandler stockAndDamageIndicator = null;

	/// <summary>
	/// The end screen handler for this character.
	/// </summary>
	[Tooltip("The end screen handler for this character.")]
	[SerializeField] private EndScreenReady endScreenReady = null;

	/// <summary>
	/// The sand bag character.
	/// </summary>
	[Tooltip("The sand bag character.")]
	[SerializeField] private GameObject sandBagCharacter = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Spawns the characters.
	/// </summary>
	private void Awake()
	{
		for (int i = 0; i < GameManager.Instance.characterInfos.Count; i++)
		{
			GameManager.CharacterInfo characterInfo = GameManager.Instance.characterInfos[i];

			if (characterInfo.ControllerNumber == playerNumber)
			{
				GameObject spawnedCharacter = Instantiate(characterInfo.CharacterPrefab, this.transform);
				spawnedCharacter.transform.localPosition = Vector3.zero;
				FighterController fighterController = spawnedCharacter.GetComponent<FighterController>();
				fighterController.controllerNumber = characterInfo.ControllerNumber;
				characterInfo.fighterController = fighterController;
				offScreenArrowHandler.fighterController = fighterController;
				stockAndDamageIndicator.characterPrefab = characterInfo.CharacterPrefab;
				stockAndDamageIndicator.fighterController = fighterController;
				endScreenReady.fighterController = fighterController;
				GameManager.Instance.totalPlayers++;
			}
		}

		if (GameManager.Instance.characterInfos.Count == 1)
		{
			int npcController = GameManager.Instance.characterInfos[0].ControllerNumber + 1;
			if (npcController >= 5)
			{
				npcController = 1;
			}

			if (playerNumber == npcController)
			{
				GameObject spawnedCharacter = Instantiate(sandBagCharacter, this.transform);
				spawnedCharacter.transform.localPosition = Vector3.zero;
				FighterController fighterController = spawnedCharacter.GetComponent<FighterController>();
				fighterController.controllerNumber = 5;
				GameManager.Instance.sandBagController = fighterController;
				offScreenArrowHandler.fighterController = fighterController;
				stockAndDamageIndicator.characterPrefab = sandBagCharacter;
				stockAndDamageIndicator.fighterController = fighterController;
				GameManager.Instance.totalPlayers++;
			}
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}