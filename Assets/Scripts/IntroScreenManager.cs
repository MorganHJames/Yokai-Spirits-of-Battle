////////////////////////////////////////////////////////////
// File: IntroScreenManager.cs
// Author: Morgan Henry James
// Date Created: 01-04-2020
// Brief: Loads the character select screen if the A button is pressed.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Loads the character select screen if the A button is pressed.
/// </summary>
public class IntroScreenManager : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The screen transition script.
	/// </summary>
	[Tooltip("The screen transition script.")]
	[SerializeField] private ScreenTransition screenTransition = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Checks if the 'A' Button is pressed and if so goes to the character select screen.
	/// </summary>
	private void Update()
	{
		if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2") || Input.GetButtonDown("Jump3") || Input.GetButtonDown("Jump4"))
		{
			AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Start);
			screenTransition.ScreenTransistion(1.0f, "CharacterSelect");
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}