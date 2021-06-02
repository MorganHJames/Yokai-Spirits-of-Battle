////////////////////////////////////////////////////////////
// File: GameManager.cs
// Author: Morgan Henry James
// Date Created: 29-03-2020
// Brief: Controls the interaction between scenes.
//////////////////////////////////////////////////////////// 

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the interaction between scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
	#region Structures
	#region Public
	/// <summary>
	/// The character info structure.
	/// </summary>
	public class CharacterInfo
	{
		public int ControllerNumber;
		public GameObject CharacterPrefab;
		public FighterController fighterController;
	}
	#endregion
	#region Private
	#endregion
	#endregion

	#region Variables
	#region Private
	/// <summary>
	/// The private instance of the game manager.
	/// </summary>
	private static GameManager _instance;
	#endregion
	#region Public
	/// <summary>
	/// The sandbag controller.
	/// </summary>
	[HideInInspector] public FighterController sandBagController = null;

	/// <summary>
	/// The public instance of the game manager.
	/// </summary>
	public static GameManager Instance { get { return _instance; } }

	/// <summary>
	/// All the players.
	/// </summary>
	[HideInInspector] public List<CharacterInfo> characterInfos = new List<CharacterInfo>();

	/// <summary>
	/// All the players connected.
	/// </summary>
	[HideInInspector] public List<int> connectedPlayers = new List<int>();

	/// <summary>
	/// All the players connected that are ready.
	/// </summary>
	[HideInInspector] public List<int> connectedPlayersReady = new List<int>();

	/// <summary>
	/// The players ready to end the game.
	/// </summary>
	[HideInInspector] public int readyToEndPlayers = 0;

	/// <summary>
	/// The dead player count.
	/// </summary>
	[HideInInspector] public int deadPlayers = 0;

	/// <summary>
	/// The player count.
	/// </summary>
	[HideInInspector] public int totalPlayers = 0;

	/// <summary>
	/// The player reloaded in.
	/// </summary>
	[HideInInspector] public int reloadedPlayers = 0;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// The singleton set up.
	/// </summary>
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}