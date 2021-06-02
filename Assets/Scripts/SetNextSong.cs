////////////////////////////////////////////////////////////
// File: SetNextSong.cs
// Author: Morgan Henry James
// Date Created: 12-05-2020
// Brief: Sets the background music for the scene.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Sets the background music for the scene.
/// </summary>
public class SetNextSong : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The background music for the scene.
	/// </summary>
	[Tooltip("The background music for the scene.")]
	[SerializeField] private AudioClip backGroundSong = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the audio manger to play the next track.
	/// </summary>
	private void Start()
	{
		AudioManager.instance.nextTrack = backGroundSong;
	}
	#endregion
	#region Public

	#endregion
	#endregion
}