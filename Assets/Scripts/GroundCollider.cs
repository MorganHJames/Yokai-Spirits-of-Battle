////////////////////////////////////////////////////////////
// File: GroundCollider.cs
// Author: Morgan Henry James
// Date Created: 14-05-2020
// Brief: Calls the function in the fighter controller to check if the player is grounded.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Calls the function in the fighter controller to check if the player is grounded.
/// </summary>
public class GroundCollider : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The fighter controller script.
	/// </summary>
	private FighterController fighterController;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the fighter controlelr script.
	/// </summary>
	private void Start()
	{
		if (transform.parent.parent.gameObject.name == "SandBag(Clone)")
        {
			fighterController = transform.parent.parent.GetComponent<FighterController>();
		}
        else
        {
			fighterController = transform.parent.GetComponent<FighterController>();
		}
	}

	/// <summary>
	/// Handles the on trigger stay and calls the function in the fighter controller to handle it.
	/// </summary>
	/// <param name="collider">The colider staying in.</param>
	private void OnTriggerStay(Collider collider)
	{
		fighterController.TriggerStay(collider);
	}

	/// <summary>
	/// Handles the on trigger stay and calls the function in the fighter controller to handle it.
	/// </summary>
	/// <param name="collider">The colider staying in.</param>
	private void OnTriggerEnter(Collider other)
	{
		fighterController.TriggerEnter(other);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}