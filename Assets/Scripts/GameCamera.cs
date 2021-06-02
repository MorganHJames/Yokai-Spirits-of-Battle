////////////////////////////////////////////////////////////
// File: GameCamera.cs
// Author: Morgan Henry James
// Date Created: 29-03-2020
// Brief: Zooms in and out and moves the camera to fit all the characters on the screen with some margin up to a point.
//////////////////////////////////////////////////////////// 

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zooms in and out and moves the camera to fit all the characters on the screen with some margin up to a point.
/// </summary>
public class GameCamera : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The players that the camera is accounting for.
	/// </summary>
	private List<FighterController> fighters = new List<FighterController>();

	/// <summary>
	/// The players that the camera is accounting for.
	/// </summary>
	public List<Transform> targets = new List<Transform>();

	/// <summary>
	/// The offset for the camera.
	/// </summary>
	[Tooltip("The offset for the camera.")]
	[SerializeField] private Vector3 offSet = Vector3.zero;

	/// <summary>
	/// The maximum movement for the camera.
	/// </summary>
	[Tooltip("The maximum movement for the camera.")]
	[SerializeField] private Vector3 maxMove = Vector3.zero;

	/// <summary>
	/// The speed of the smoothing.
	/// </summary>
	[Tooltip("The speed of the smoothing.")]
	[SerializeField] private float smoothSpeed = 0.5f;

	/// <summary>
	/// Minimum zoom.
	/// </summary>
	[Tooltip("The minimum zoom.")]
	[SerializeField] private float minZoom = 40.0f;

	/// <summary>
	/// Maximum zoom.
	/// </summary>
	[Tooltip("The maximum zoom.")]
	[SerializeField] private float maxZoom = 10.0f;

	/// <summary>
	/// The zoom limiter.
	/// </summary>
	[Tooltip("The zoom limiter.")]
	[SerializeField] private float zoomLimiter = 50.0f;

	/// <summary>
	/// The scene camera.
	/// </summary>
	[Tooltip("The scene camera.")]
	[SerializeField] private new Camera camera = null;

	/// <summary>
	/// The current velocity of the camera.
	/// </summary>
	private Vector3 velocity = Vector3.zero;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets all the target points.
	/// </summary>
	private void Awake()
	{
		foreach (GameManager.CharacterInfo characterInfo in GameManager.Instance.characterInfos)
		{
			fighters.Add(characterInfo.fighterController);
			targets.Add(characterInfo.fighterController.transform);
		}

		if (GameManager.Instance.characterInfos.Count == 1)
		{
			fighters.Add(GameManager.Instance.sandBagController);
			targets.Add(GameManager.Instance.sandBagController.transform);
		}
	}

	/// <summary>
	/// Removes and adds transforms to the transforms list depending on if the character is dead or not.
	/// </summary>
	private void Update()
	{
		foreach (FighterController fighterController in fighters)
		{
			if (fighterController.isDead && targets.Contains(fighterController.transform))
			{
				targets.Remove(fighterController.transform);
			}
			else if (!fighterController.isDead && !targets.Contains(fighterController.transform))
			{
				targets.Add(fighterController.transform);
			}
		}
	}

	/// <summary>
	/// Sets the camera position to the center point with an offset.
	/// </summary>
	private void LateUpdate()
	{
		if (targets.Count == 0)
		{
			return;
		}

		Move();
		Zoom();
	}

	/// <summary>
	/// Zooms the camera in and out.
	/// </summary>
	private void Zoom()
	{
		float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
	}

	/// <summary>
	/// Gets the greatest distance between players.
	/// </summary>
	/// <returns>The greatest distance between players.</returns>
	private float GetGreatestDistance()
	{
		Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		return Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
	}

	/// <summary>
	/// Moves the camera to the correct position.
	/// </summary>
	private void Move()
	{
		Vector3 centerPoint = GetCenterPoint();

		Vector3 newPosition = centerPoint + offSet;

		newPosition.x = Mathf.Clamp(newPosition.x, offSet.x - maxMove.x, offSet.x + maxMove.x);
		newPosition.y = Mathf.Clamp(newPosition.y, offSet.y - maxMove.y, offSet.y + maxMove.y);
		newPosition.z = Mathf.Clamp(newPosition.z, offSet.z - maxMove.z, offSet.z + maxMove.z);

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
	}

	/// <summary>
	/// Gets the center point of the players.
	/// </summary>
	/// <returns>The center point of the players as a vector 3.</returns>
	private Vector3 GetCenterPoint()
	{
		if (targets.Count == 1)
		{
			return targets[0].transform.position;
		}

		Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		return bounds.center;
	}
	#endregion
	#region Public

	#endregion
	#endregion
}