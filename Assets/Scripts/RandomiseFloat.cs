////////////////////////////////////////////////////////////
// File: RandomiseFloat.cs
// Author: Morgan Henry James
// Date Created: 25-03-2020
// Brief: Starts the animations at a random time.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Starts the animations at a random time.
/// </summary>
public class RandomiseFloat : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The animator of the lantern.
	/// </summary>
	[Tooltip("The animator of the lantern.")]
	[SerializeField] private Animator animator = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Starts the animation at a random time.
	/// </summary>
	private void Awake()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		animator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
	}
	#endregion
	#region Public

	#endregion
	#endregion
}