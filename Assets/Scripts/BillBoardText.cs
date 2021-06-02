////////////////////////////////////////////////////////////
// File: BillBoardText.cs
// Author: Morgan Henry James
// Date Created: 13-02-2020
// Brief: Makes the canvas face the camera.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Makes the canvas face the camera.
/// </summary>
public class BillBoardText : MonoBehaviour
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
	/// Copys the cameras rotation.
	/// </summary>
	private void Update()
	{
		transform.rotation = Camera.main.transform.rotation;
	}
	#endregion
	#region Public

	#endregion
	#endregion
}