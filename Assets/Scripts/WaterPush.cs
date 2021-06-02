////////////////////////////////////////////////////////////
// File: WaterPush.cs
// Author: Morgan Henry James
// Date Created: 29-05-2020
// Brief: Pushes the player forward.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Pushes the player forward.
/// </summary>
public class WaterPush : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// How fast to push the player.
	/// </summary>
	[Tooltip("How fast to push the player.")]
	[SerializeField] private float moveSpeed = 0.5f;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Called when the water push hit box hits another trigger box.
	/// </summary>
	/// <param name="other">The object it collides with.</param>
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			FighterController opponent = other.GetComponentInParent<FighterController>();
			opponent.rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}