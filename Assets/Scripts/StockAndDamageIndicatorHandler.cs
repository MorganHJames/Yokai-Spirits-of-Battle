////////////////////////////////////////////////////////////
// File: StockAndDamageIndicatorHandler.cs
// Author: Morgan Henry James
// Date Created: 31-03-2020
// Brief: Controls the UI to show the damage to the player and their remaining lives.
//////////////////////////////////////////////////////////// 

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI to show the damage to the player and their remaining lives.
/// </summary>
public class StockAndDamageIndicatorHandler : MonoBehaviour
{
	#region Structures
	#region Public
	[System.Serializable]
	public struct CharacterImagry
	{
		public GameObject prefab;
		public Sprite stockSprite;
		public Sprite chracterIcon;
	}
	#endregion
	#region Private

	#endregion
	#endregion

	#region Variables
	#region Private
	/// <summary>
	/// The damage indicator for the fighter controller.
	/// </summary>
	[Tooltip("The damage indicator for the fighter controller.")]
	[SerializeField] private TextMeshProUGUI damageIndicator = null;

	/// <summary>
	/// The background square image.
	/// </summary>
	[Tooltip("The background square image.")]
	[SerializeField] private Image backgroundSquare = null;

	/// <summary>
	/// The image for stock 1.
	/// </summary>
	[Tooltip("The image for stock 1.")]
	[SerializeField] private Image stock1 = null;

	/// <summary>
	/// The image for stock 2.
	/// </summary>
	[Tooltip("The image for stock 2.")]
	[SerializeField] private Image stock2 = null;

	/// <summary>
	/// The image for stock 3.
	/// </summary>
	[Tooltip("The image for stock 3.")]
	[SerializeField] private Image stock3 = null;

	/// <summary>
	/// The image for character icon.
	/// </summary>
	[Tooltip("The image for character icon.")]
	[SerializeField] private Image chracterIcon = null;

	/// <summary>
	/// All of the character prefabs and there associated imagery.
	/// </summary>
	[Tooltip("All of the character prefabs and there associated imagery.")]
	[SerializeField] private CharacterImagry[] characterImagries = null;
	#endregion
	#region Public
	/// <summary>
	/// The fighter controller that this script handles the UI for.
	/// </summary>
	[Tooltip("The fighter controller that this script handles the UI for.")]
	[HideInInspector] public FighterController fighterController = null;

	/// <summary>
	/// The prefab that the character uses.
	/// </summary>
	[Tooltip("The prefab that the character uses.")]
	[HideInInspector] public GameObject characterPrefab = null;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets up the stock images.
	/// Sets up the background square color.
	/// Sets up the character icon.
	/// </summary>
	private void Start()
	{
		if (fighterController)
		{
			switch (fighterController.controllerNumber)
			{
				case 1:
					backgroundSquare.color = Color.blue;
					break;
				case 2:
					backgroundSquare.color = Color.yellow;
					break;
				case 3:
					backgroundSquare.color = Color.red;
					break;
				case 4:
					backgroundSquare.color = Color.green;
					break;
				case 5:
					backgroundSquare.color = Color.white;
					break;
				default:
					break;
			}

			for (int i = 0; i < characterImagries.Length; i++)
			{
				if (characterImagries[i].prefab == characterPrefab)
				{
					chracterIcon.sprite = characterImagries[i].chracterIcon;
					stock1.sprite = characterImagries[i].stockSprite;
					stock2.sprite = characterImagries[i].stockSprite;
					stock3.sprite = characterImagries[i].stockSprite;
					break;
				}
			}
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Sets the stock and the damage percentage to the correct amount.
	/// </summary>
	private void Update()
	{
		if (fighterController.isDead)
		{
			damageIndicator.text = "";
			stock3.gameObject.SetActive(false);
			stock2.gameObject.SetActive(false);
			stock1.gameObject.SetActive(false);
		}
		else
		{
			damageIndicator.text = (int)fighterController.damageTaken + "%";
			damageIndicator.color = Color.Lerp(Color.white, Color.red, fighterController.damageTaken / 100f);

			switch (fighterController.lives)
			{
				case 3:
					stock3.gameObject.SetActive(true);
					stock2.gameObject.SetActive(true);
					stock1.gameObject.SetActive(true);
					break;
				case 2:
					stock3.gameObject.SetActive(false);
					stock2.gameObject.SetActive(true);
					stock1.gameObject.SetActive(true);
					break;
				case 1:
					stock3.gameObject.SetActive(false);
					stock2.gameObject.SetActive(false);
					stock1.gameObject.SetActive(true);
					break;
				case 0:
					stock3.gameObject.SetActive(false);
					stock2.gameObject.SetActive(false);
					stock1.gameObject.SetActive(false);
					break;
				default:
					break;
			}
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}