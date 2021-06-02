////////////////////////////////////////////////////////////
// File: MapSelector.cs
// Author: Morgan Henry James
// Date Created: 09-04-2020
// Brief: Allows for the selection of a map to fight on.
//////////////////////////////////////////////////////////// 

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows for the selection of a map to fight on.
/// </summary>
public class MapSelector : MonoBehaviour
{
	#region Structures
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// A structure for defining map information.
	/// </summary>
	[System.Serializable]
	public struct MapInfo
	{
		public Sprite image;
		public string mapName;
		public string sceneName;
	}
	#endregion
	#endregion

	#region Variables
	#region Private
	/// <summary>
	/// The screen transition script.
	/// </summary>
	[Tooltip("The screen transition script.")]
	[SerializeField] private ScreenTransition screenTransition = null;

	/// <summary>
	/// All of the maps information.
	/// </summary>
	[Tooltip("All of the maps information.")]
	[SerializeField] private MapInfo[] mapInfos = null;

	/// <summary>
	/// The map name.
	/// </summary>
	[Tooltip("The map name.")]
	[SerializeField] private TextMeshProUGUI mapName = null;

	/// <summary>
	/// The map image.
	/// </summary>
	[Tooltip("The map image.")]
	[SerializeField] private Image mapImage = null;

	/// <summary>
	/// The current map info index.
	/// </summary>
	private int currentMapInfoIndex = 0;

	/// <summary>
	/// The time remaining until a player can pick a new map.
	/// </summary>
	private float currentWaitTime = 0.0f;

	/// <summary>
	/// The time the user must wait before picking a new map.
	/// </summary>
	private float waitTime = 0.5f;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Picks a random map and sets it as the currently selected map.
	/// </summary>
	private void Awake()
	{
		currentMapInfoIndex = Random.Range(0, mapInfos.Length);
		PopulateCanvas(mapInfos[currentMapInfoIndex]);
	}

	/// <summary>
	/// Fills in the canvas with information from the map.
	/// </summary>
	/// <param name="mapInfo">The map info to populate the canvas with.</param>
	private void PopulateCanvas(MapInfo mapInfo)
	{
		mapName.text = mapInfo.mapName;
		mapImage.sprite = mapInfo.image;
		currentWaitTime = waitTime;
	}

	/// <summary>
	/// Gets the users input and changes the map selection if left or right is pressed.
	/// If A is pressed load said map.
	/// </summary>
	private void Update()
	{
		float leftHorizontal1 = Input.GetAxis("LeftHorizontal1");
		float leftHorizontal2 = Input.GetAxis("LeftHorizontal2");
		float leftHorizontal3 = Input.GetAxis("LeftHorizontal3");
		float leftHorizontal4 = Input.GetAxis("LeftHorizontal4");

		currentWaitTime -= Time.deltaTime;
		currentWaitTime = Mathf.Clamp01(currentWaitTime);

		// Confirm map.
		if (currentWaitTime <= 0.0f && (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2") || Input.GetButtonDown("Jump3") || Input.GetButtonDown("Jump4")))
		{
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Fight);
			LoadMap(mapInfos[currentMapInfoIndex]);
		}
		// Go down an index.
		else if (currentWaitTime <= 0.0f && (leftHorizontal1 <= -0.9f || leftHorizontal2 <= -0.9f || leftHorizontal3 <= -0.9f || leftHorizontal4 <= -0.9f))
		{
			currentMapInfoIndex -= 1;

			if (currentMapInfoIndex < 0)
			{
				currentMapInfoIndex = mapInfos.Length - 1;
			}

			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Swap);

			PopulateCanvas(mapInfos[currentMapInfoIndex]);
		}
		// Go up an index.
		else if (currentWaitTime <= 0.0f && (leftHorizontal1 >= 0.9f || leftHorizontal2 >= 0.9f || leftHorizontal3 >= 0.9f || leftHorizontal4 >= 0.9f))
		{
			currentMapInfoIndex++;

			if (currentMapInfoIndex >= mapInfos.Length)
			{
				currentMapInfoIndex = 0;
			}

			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Swap);
			PopulateCanvas(mapInfos[currentMapInfoIndex]);
		}
	}

	/// <summary>
	/// Loads the scene that contains the specified map.
	/// </summary>
	/// <param name="mapInfo">The map to load.</param>
	private void LoadMap(MapInfo mapInfo)
	{
		screenTransition.ScreenTransistion(1.0f, mapInfo.sceneName);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}