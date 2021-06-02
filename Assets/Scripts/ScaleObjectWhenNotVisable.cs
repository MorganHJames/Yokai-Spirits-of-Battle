////////////////////////////////////////////////////////////
// File: ScaleObjectWhenNotVisable.cs
// Author: Morgan Henry James
// Date Created: 30-03-2020
// Brief: Scales the object when not in view.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Scales the object when not in view.
/// </summary>
public class ScaleObjectWhenNotVisable : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The layer mask to ignore for the ray cast.
	/// </summary>
	[Tooltip("The layer mask to ignore for the ray cast.")]
	[SerializeField] private LayerMask layerMask = 1;

	/// <summary>
	/// The layer mask to ignore for the ray cast.
	/// </summary>
	[Tooltip("The other layer mask to ignore for the ray cast.")]
	[SerializeField] private LayerMask layerMask2 = 1;

	/// <summary>
	/// The scene camera.
	/// </summary>
	private Camera sceneCamera;

	/// <summary>
	/// The ignore mask.
	/// </summary>
	private LayerMask ignoreMask = 1;

	/// <summary>
	/// The max scale of the object.
	/// </summary>
	[Tooltip("The max scale of the object.")]
	[SerializeField] private float maxScale = 10.0f;

	/// <summary>
	/// The speed of scaling.
	/// </summary>
	[Tooltip("The speed of scaling.")]
	[SerializeField] private float scaleSpeed = 10.0f;

	/// <summary>
	/// A counter that counts the frames the object is scene or not scene.
	/// </summary>
	private int currentStack = -35;

	/// <summary>
	/// The max stack of frames.
	/// </summary>
	private int maxStack = 35;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets the camera.
	/// </summary>
	private void Start()
	{
		ignoreMask = layerMask | layerMask2;
		sceneCamera = Camera.main;
	}

	/// <summary>
	/// Sends out a ray cast from the camera to this object and if it hits then scale down the sphere else scale it up.
	/// </summary>
	private void Update()
	{
		RaycastHit hit;

		if (Physics.Raycast(sceneCamera.transform.position, gameObject.transform.position - sceneCamera.transform.position, out hit, Mathf.Infinity, ~ignoreMask))
		{
			if (hit.collider.gameObject.tag == "PlayerSphere")
			{
				if (currentStack == -maxStack)
				{
					transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * scaleSpeed);
				}
				currentStack--;
			}
			else
			{
				if (currentStack == maxStack)
				{
					transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxScale, maxScale, maxScale), Time.deltaTime * scaleSpeed);
				}
				currentStack++;
			}
		}

		currentStack = Mathf.Clamp(currentStack, -maxStack, maxStack);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}