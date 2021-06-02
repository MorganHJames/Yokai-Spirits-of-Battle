////////////////////////////////////////////////////////////
// File: Grab.cs
// Author: Morgan Henry James
// Date Created: 20-03-2020
// Brief: Allows for the characters to initiate a grab.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Allows for the characters to initiate a grab.
/// </summary>
public class Grab : MonoBehaviour
{
	#region Variables
	#region Private

	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Called when the attack hit box hits another trigger box.
	/// </summary>
	/// <param name="other">The object it collides with.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			FighterController opponent = other.GetComponentInParent<FighterController>();
			FighterController parent = GetComponentInParent<FighterController>();

			if (opponent != parent && parent.grabbedFighter == null && opponent.fighterGrabbingThisFighter == null && opponent.grabbedFighter == null)
			{
				parent.SetState(FighterController.CharacterStates.Grabbing);
				opponent.characterAudio.GrabSound();
				opponent.SetState(FighterController.CharacterStates.Grabbed);
				opponent.characterAudio.GrabbedSound();
				parent.grabbedFighter = opponent;
				opponent.fighterGrabbingThisFighter = parent;
				parent.currentSpeed = parent.speedWhilstGrabbing;
			}
		}
	}

	#endregion
	#region Public

	#endregion
	#endregion
}