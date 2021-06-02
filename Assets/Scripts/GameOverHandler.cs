////////////////////////////////////////////////////////////
// File: GameOverHandler.cs
// Author: Morgan Henry James
// Date Created: 14-04-2020
// Brief: Turns the game play canvas off and the game over canvas on when all but one character is dead.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Turns the game play canvas off and the game over canvas on when all but one character is dead.
/// </summary>
public class GameOverHandler : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The game play canvas.
	/// </summary>
	[Tooltip("The game play canvas.")]
	[SerializeField] private GameObject gamePlayCanvas = null;


	/// <summary>
	/// The game over canvas.
	/// </summary>
	[Tooltip("The game over canvas.")]
	[SerializeField] private GameObject gameOverCanvas = null;

	/// <summary>
	/// If the swap invoke has occured.
	/// </summary>
	private bool swapped = false;

	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Checks to see if only one player is left.
	/// </summary>
	private void Update()
	{
		if (GameManager.Instance.deadPlayers == GameManager.Instance.totalPlayers - 1 && !swapped)
		{
			swapped = true;
			Invoke("SwapCanvas", 2.5f);
		}
	}

	/// <summary>
	/// Swaps the canvas.
	/// </summary>
	private void SwapCanvas()
	{
		gamePlayCanvas.SetActive(false);
		gameOverCanvas.SetActive(true);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}