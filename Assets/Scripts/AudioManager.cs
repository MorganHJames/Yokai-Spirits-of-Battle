////////////////////////////////////////////////////////////
// File: AudioManager.cs
// Author: Morgan Henry James
// Date Created: 12-05-2020
// Brief: Controls the audio within the game.
//////////////////////////////////////////////////////////// 

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the audio within the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The music audio source.
	/// </summary>
	[Tooltip("The music audio source.")]
	[SerializeField] private AudioSource musicSource = null;

	/// <summary>
	/// The SFX audio sources.
	/// </summary>
	[Tooltip("The SFX audio sources.")]
	[SerializeField] private AudioSource[] sfxSources = null;

	/// <summary>
	/// All the SFX audio clips.
	/// </summary>
	[Tooltip("All the SFX audio clips.")]
	[SerializeField] private AudioClip[] sfxClips = null;

	/// <summary>
	/// The animator.
	/// </summary>
	[Tooltip("The animator.")]
	[SerializeField] private Animator animator = null;

	/// <summary>
	/// The lowest a SFX clip will be randomly pitched.
	/// </summary>
	private float lowPitchRange = .95f;

	/// <summary>
	/// The highest a SFX clip will be randomly pitched.
	/// </summary>
	private float highPitchRange = 1.05f;
	#endregion
	#region Public
	/// <summary>
	/// The next backing track.
	/// </summary>
	[HideInInspector]
	public AudioClip nextTrack = null;

	/// <summary>
	/// The instance of the script that allows other scripts to call functions from the audio manager.
	/// </summary>
	public static AudioManager instance = null;
	#endregion
	#endregion

	#region Enumerators
	#region Public
	public enum SFXClips
	{
		Accept = 0,
		Reject,
		AllReady,
		Start,
		Swap,
		Fight,
		KnockOut
	}
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the instance of the audio manager.
	/// </summary>
	private void Awake()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
		{
			//if not, set it to this.
			instance = this;
		}
		//If instance already exists:
		else if (instance != this)
		{
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	/// <summary>
	/// Delegates the scene loaded function.
	/// </summary>
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	/// <summary>
	/// On finished loading a level play an animation to transition the music.
	/// </summary>
	/// <param name="scene">The scene that was loaded.</param>
	/// <param name="mode">The mode the scene loaded in.</param>
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		animator.Play("MusicChange");
	}

	/// <summary>
	/// Used to play one shot sounds after a delay.
	/// </summary>
	/// <param name="clip">The sound clip to play.</param>
	/// <param name="delay">How long to wait before playing the sound.</param>
	/// <returns></returns>
	private IEnumerator WaitThenPlaySFX(int musicClipIndex, float delay)
	{
		yield return new WaitForSeconds(delay);
		PlayOneShot(musicClipIndex);
	}
	#endregion
	#region Public
	/// <summary>
	/// Plays the next song.
	/// </summary>
	public void PlayNextSong()
	{
		musicSource.clip = nextTrack;
		musicSource.Play();
	}

	/// <summary>
	/// Used to play one shot sounds.
	/// Called like so : AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Button);
	/// </summary>
	/// <param name="clip">The sound clip to play.</param>
	/// <param name="delay">How long to wait before playing the sound.</param>
	public void PlayOneShot(int musicClipIndex, float delay = 0f)
	{
		if (delay > 0f)
		{
			StartCoroutine(WaitThenPlaySFX(musicClipIndex, delay));
		}
		else
		{
			int i;

			for (i = 0; i < sfxSources.Length; i++)
			{
				if (!sfxSources[i].isPlaying)
				{
					break;
				}
			}

			//Set the clip of our sfxSource audio source to the clip passed in as a parameter.
			sfxSources[i].clip = sfxClips[musicClipIndex];

			//Set the pitch of the audio source to the randomly chosen pitch.
			sfxSources[i].pitch = Random.Range(lowPitchRange, highPitchRange);

			//Play the clip.
			sfxSources[i].Play();
		}
	}
	#endregion
	#endregion
}