////////////////////////////////////////////////////////////
// File: CharacterAudio.cs
// Author: Morgan Henry James
// Date Created: 12-05-2020
// Brief: Controls the characters audio.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Controls the characters audio.
/// </summary>
public class CharacterAudio : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The SFX audio sources for the character.
	/// </summary>
	[Tooltip("The SFX audio sources for the character.")]
	[SerializeField] private AudioSource[] sfxSources = null;

	/// <summary>
	/// The lowest a SFX clip will be randomly pitched.
	/// </summary>
	private float lowPitchRange = .95f;

	/// <summary>
	/// The highest a SFX clip will be randomly pitched.
	/// </summary>
	private float highPitchRange = 1.05f;

	/// <summary>
	/// All the hurt audio clips.
	/// </summary>
	[Tooltip("All the hurt audio clips.")]
	[SerializeField] private AudioClip[] hurtClips = null;

	/// <summary>
	/// The characters footstep audio clip.
	/// </summary>
	[Tooltip("The characters footstep audio clip.")]
	[SerializeField] private AudioClip footstep = null;

	/// <summary>
	/// The characters jump audio clip.
	/// </summary>
	[Tooltip("The characters jump audio clip.")]
	[SerializeField] private AudioClip jump = null;

	/// <summary>
	/// The characters land audio clip.
	/// </summary>
	[Tooltip("The characters land audio clip.")]
	[SerializeField] private AudioClip land = null;

	/// <summary>
	/// The characters grabbed audio clip.
	/// </summary>
	[Tooltip("The characters grabbed audio clip.")]
	[SerializeField] private AudioClip grabbed = null;

	/// <summary>
	/// The characters clashed audio clip.
	/// </summary>
	[Tooltip("The characters clashed audio clip.")]
	[SerializeField] private AudioClip clashed = null;

	/// <summary>
	/// The characters struggle audio clip.
	/// </summary>
	[Tooltip("The characters struggle audio clip.")]
	[SerializeField] private AudioClip struggle = null;

	/// <summary>
	/// The characters respawn audio clip.
	/// </summary>
	[Tooltip("The characters respawn audio clip.")]
	[SerializeField] private AudioClip respawn = null;

	/// <summary>
	/// The characters grab audio clip.
	/// </summary>
	[Tooltip("The characters grab audio clip.")]
	[SerializeField] private AudioClip grab = null;

	/// <summary>
	/// The characters blockedAttack audio clip.
	/// </summary>
	[Tooltip("The characters blockedAttack audio clip.")]
	[SerializeField] private AudioClip blockedAttack = null;

	/// <summary>
	/// The characters thrown audio clip.
	/// </summary>
	[Tooltip("The characters thrown audio clip.")]
	[SerializeField] private AudioClip thrown = null;

	/// <summary>
	/// The characters throwing audio clip.
	/// </summary>
	[Tooltip("The characters throwing audio clip.")]
	[SerializeField] private AudioClip throwing = null;

	/// <summary>
	/// The characters shield audio clip.
	/// </summary>
	[Tooltip("The characters shield audio clip.")]
	[SerializeField] private AudioClip shield = null;

	/// <summary>
	/// The characters shieldBreak audio clip.
	/// </summary>
	[Tooltip("The characters shieldBreak audio clip.")]
	[SerializeField] private AudioClip shieldBreak = null;

	[Header("Attacks")]
	/// <summary>
	/// The characters lightAttack audio clip.
	/// </summary>
	[Tooltip("The characters lightAttack audio clip.")]
	[SerializeField] private AudioClip lightAttack = null;

	/// <summary>
	/// The characters movingLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters lightAttack audio clip.")]
	[SerializeField] private AudioClip movingLightAttack = null;

	/// <summary>
	/// The characters specialLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialLightAttack audio clip.")]
	[SerializeField] private AudioClip specialLightAttack = null;

	/// <summary>
	/// The characters specialMovingLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialMovingLightAttack audio clip.")]
	[SerializeField] private AudioClip specialMovingLightAttack = null;

	/// <summary>
	/// The characters heavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters heavyAttack audio clip.")]
	[SerializeField] private AudioClip heavyAttack = null;

	/// <summary>
	/// The characters movingHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters movingHeavyAttack audio clip.")]
	[SerializeField] private AudioClip movingHeavyAttack = null;

	/// <summary>
	/// The characters specialHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialHeavyAttack audio clip.")]
	[SerializeField] private AudioClip specialHeavyAttack = null;

	/// <summary>
	/// The characters specialMovingHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialMovingHeavyAttack audio clip.")]
	[SerializeField] private AudioClip specialMovingHeavyAttack = null;

	/// <summary>
	/// The characters aerialAttack audio clip.
	/// </summary>
	[Tooltip("The characters aerialAttack audio clip.")]
	[SerializeField] private AudioClip aerialAttack = null;

	/// <summary>
	/// The characters aerialLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters aerialLightAttack audio clip.")]
	[SerializeField] private AudioClip aerialLightAttack = null;

	/// <summary>
	/// The characters aerialMovingLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters aerialMovingLightAttack audio clip.")]
	[SerializeField] private AudioClip aerialMovingLightAttack = null;

	/// <summary>
	/// The characters specialAerialLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialAerialLightAttack audio clip.")]
	[SerializeField] private AudioClip specialAerialLightAttack = null;

	/// <summary>
	/// The characters specialAerialMovingLightAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialAerialMovingLightAttack audio clip.")]
	[SerializeField] private AudioClip specialAerialMovingLightAttack = null;

	/// <summary>
	/// The characters aerialHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters aerialHeavyAttack audio clip.")]
	[SerializeField] private AudioClip aerialHeavyAttack = null;

	/// <summary>
	/// The characters aerialMovingHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters aerialMovingHeavyAttack audio clip.")]
	[SerializeField] private AudioClip aerialMovingHeavyAttack = null;

	/// <summary>
	/// The characters specialAerialHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialAerialHeavyAttack audio clip.")]
	[SerializeField] private AudioClip specialAerialHeavyAttack = null;

	/// <summary>
	/// The characters specialAerialMovingHeavyAttack audio clip.
	/// </summary>
	[Tooltip("The characters specialAerialMovingHeavyAttack audio clip.")]
	[SerializeField] private AudioClip specialAerialMovingHeavyAttack = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Used to play one shot sounds.
	/// </summary>
	/// <param name="audioClipToPlay">The audio clip to play.</param>
	private void PlayOneShot(AudioClip audioClipToPlay)
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
		sfxSources[i].clip = audioClipToPlay;

		//Set the pitch of the audio source to the randomly chosen pitch.
		sfxSources[i].pitch = Random.Range(lowPitchRange, highPitchRange);

		//Play the clip.
		sfxSources[i].Play();
	}
	#endregion
	#region Public
	/// <summary>
	/// Plays a random hurt sound.
	/// </summary>
	public void HurtSound()
	{
		PlayOneShot(hurtClips[Random.Range(0, hurtClips.Length)]);
	}

	/// <summary>
	/// Plays the jump audio clip.
	/// </summary>
	public void JumpSound()
	{
		PlayOneShot(jump);
	}

	/// <summary>
	/// Plays the grabbed audio clip.
	/// </summary>
	public void GrabbedSound()
	{
		PlayOneShot(grabbed);
	}

	/// <summary>
	/// Plays the struggle audio clip.
	/// </summary>
	public void StruggleSound()
	{
		PlayOneShot(struggle);
	}

	/// <summary>
	/// Plays the respawn audio clip.
	/// </summary>
	public void RespawnSound()
	{
		PlayOneShot(respawn);
	}

	/// <summary>
	/// Plays the grab audio clip.
	/// </summary>
	public void GrabSound()
	{
		PlayOneShot(grab);
	}

	/// <summary>
	/// Plays the thrown audio clip.
	/// </summary>
	public void ThrownSound()
	{
		PlayOneShot(thrown);
	}

	/// <summary>
	/// Plays the throwing audio clip.
	/// </summary>
	public void ThrowingSound()
	{
		PlayOneShot(throwing);
	}

	/// <summary>
	/// Plays the land audio clip.
	/// </summary>
	public void LandSound()
	{
		PlayOneShot(land);
	}

	/// <summary>
	/// Plays the footstep audio clip.
	/// </summary>
	public void FootstepSound()
	{
		PlayOneShot(footstep);
	}

	/// <summary>
	/// Plays the shield audio clip.
	/// </summary>
	public void ShieldSound()
	{
		PlayOneShot(shield);
	}

	/// <summary>
	/// Plays the blockedAttack audio clip.
	/// </summary>
	public void BlockedAttackSound()
	{
		PlayOneShot(blockedAttack);
	}

	/// <summary>
	/// Plays the shieldBreak audio clip.
	/// </summary>
	public void ShieldBreakSound()
	{
		PlayOneShot(shieldBreak);
	}

	/// <summary>
	/// Plays the clashed audio clip.
	/// </summary>
	public void ClashedSound()
	{
		PlayOneShot(clashed);
	}

	/// <summary>
	/// Plays the lightAttack audio clip.
	/// </summary>
	public void LightAttackSound()
	{
		PlayOneShot(lightAttack);
	}

	/// <summary>
	/// Plays the movingLightAttack audio clip.
	/// </summary>
	public void MovingLightAttackSound()
	{
		PlayOneShot(movingLightAttack);
	}

	/// <summary>
	/// Plays the specialLightAttack audio clip.
	/// </summary>
	public void SpecialLightAttackSound()
	{
		PlayOneShot(specialLightAttack);
	}

	/// <summary>
	/// Plays the specialMovingLightAttack audio clip.
	/// </summary>
	public void SpecialMovingLightAttackSound()
	{
		PlayOneShot(specialMovingLightAttack);
	}

	/// <summary>
	/// Plays the heavyAttack audio clip.
	/// </summary>
	public void HeavyAttackSound()
	{
		PlayOneShot(heavyAttack);
	}

	/// <summary>
	/// Plays the movingHeavyAttack audio clip.
	/// </summary>
	public void MovingHeavyAttackSound()
	{
		PlayOneShot(movingHeavyAttack);
	}

	/// <summary>
	/// Plays the specialHeavyAttack audio clip.
	/// </summary>
	public void SpecialHeavyAttackSound()
	{
		PlayOneShot(specialHeavyAttack);
	}

	/// <summary>
	/// Plays the specialMovingHeavyAttack audio clip.
	/// </summary>
	public void SpecialMovingHeavyAttackSound()
	{
		PlayOneShot(specialMovingHeavyAttack);
	}

	/// <summary>
	/// Plays the aerialAttack audio clip.
	/// </summary>
	public void AerialAttackSound()
	{
		PlayOneShot(aerialAttack);
	}

	/// <summary>
	/// Plays the aerialLightAttack audio clip.
	/// </summary>
	public void AerialLightAttackSound()
	{
		PlayOneShot(aerialLightAttack);
	}

	/// <summary>
	/// Plays the aerialMovingLightAttack audio clip.
	/// </summary>
	public void AerialMovingLightAttackSound()
	{
		PlayOneShot(aerialMovingLightAttack);
	}

	/// <summary>
	/// Plays the specialAerialLightAttack audio clip.
	/// </summary>
	public void SpecialAerialLightAttackSound()
	{
		PlayOneShot(specialAerialLightAttack);
	}

	/// <summary>
	/// Plays the specialAerialMovingLightAttack audio clip.
	/// </summary>
	public void SpecialAerialMovingLightAttackSound()
	{
		PlayOneShot(specialAerialMovingLightAttack);
	}

	/// <summary>
	/// Plays the aerialHeavyAttack audio clip.
	/// </summary>
	public void AerialHeavyAttackSound()
	{
		PlayOneShot(aerialHeavyAttack);
	}

	/// <summary>
	/// Plays the aerialMovingHeavyAttack audio clip.
	/// </summary>
	public void AerialMovingHeavyAttackSound()
	{
		PlayOneShot(aerialMovingHeavyAttack);
	}

	/// <summary>
	/// Plays the specialAerialHeavyAttack audio clip.
	/// </summary>
	public void SpecialAerialHeavyAttackSound()
	{
		PlayOneShot(specialAerialHeavyAttack);
	}

	/// <summary>
	/// Plays the specialAerialMovingHeavyAttack audio clip.
	/// </summary>
	public void SpecialAerialMovingHeavyAttackSound()
	{
		PlayOneShot(specialAerialMovingHeavyAttack);
	}
	#endregion
	#endregion
}