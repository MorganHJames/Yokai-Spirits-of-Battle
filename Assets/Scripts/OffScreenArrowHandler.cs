////////////////////////////////////////////////////////////
// File: OffScreenArrowHandler.cs
// Author: Morgan Henry James
// Date Created: 31-03-2020
// Brief: Fades an arrow in that points to an off screen player.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fades an arrow in that points to an off screen player.
/// </summary>
public class OffScreenArrowHandler : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The arrow image.
	/// </summary>
	[Tooltip("The arrow image.")]
	[SerializeField] private Image arrow = null;

	/// <summary>
	/// The death effect prefab.
	/// </summary>
	[Tooltip("The death effect prefab.")]
	[SerializeField] private GameObject deathEffect = null;

	/// <summary>
	/// Fade Speed.
	/// </summary>
	[Tooltip("Fade Speed.")]
	[SerializeField] private float fadeSpeed = 10.0f;

	/// <summary>
	/// Edge offset.
	/// </summary>
	private float edgeOffset = 0.05f;

	/// <summary>
	/// The scene camera.
	/// </summary>
	private new Camera camera = null;

	/// <summary>
	/// The life count of the fighter the last time this was called.
	/// </summary>
	private int lifeCountLastTimeChecked = 3;
	#endregion
	#region Public
	/// <summary>
	/// The fighter controller to point to.
	/// </summary>
	[Tooltip("The fighter controller to point to.")]
	[HideInInspector] public FighterController fighterController = null;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets up the camera variable.
	/// </summary>
	private void Awake()
	{
		camera = Camera.main;
	}

	/// <summary>
	/// Sets the arrow to white if it is the npc.
	/// </summary>
	private void Start()
	{
		if (fighterController && fighterController.controllerNumber == 5)
		{
			arrow.color = Color.white;
			arrow.color = new Color(arrow.color.r, arrow.color.g, arrow.color.b, 0);
		}
	}

	/// <summary>
	/// Checks if the character is alive and off screen and if so fades in the arrow.
	/// Moves the arrow so that it is at the edge of the screen with an offset pointing to the character.
	/// </summary>
	private void Update()
	{
		if (fighterController)
		{
			// Get the view port position.
			Vector3 screenPos = camera.WorldToViewportPoint(fighterController.transform.position);

			if (fighterController.lives != lifeCountLastTimeChecked)
			{
				lifeCountLastTimeChecked = fighterController.lives;
				GameObject deathEffectPrefab = Instantiate(deathEffect);
				AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.KnockOut);
				deathEffectPrefab.transform.SetParent(this.transform);
				deathEffectPrefab.transform.localPosition = Vector3.zero;
				deathEffectPrefab.transform.rotation = Quaternion.identity;
				deathEffectPrefab.transform.SetParent(this.transform.parent);
				deathEffectPrefab.transform.position = Vector3.Lerp(deathEffectPrefab.transform.position, deathEffectPrefab.transform.parent.position, 0.25f);
				Destroy(deathEffectPrefab, 2.0f);

				if (fighterController.lives == 0)
				{
					fighterController.rank = GameManager.Instance.totalPlayers - GameManager.Instance.deadPlayers;
				}
			}

			// If on screen or is dead.
			if ((screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1) || fighterController.isDead)
			{
				arrow.color = Color.Lerp(arrow.color, new Color(arrow.color.r, arrow.color.g, arrow.color.b, 0), Time.deltaTime * fadeSpeed);

				return;
			}

			// Change the color of the arrow to visible.
			arrow.color = Color.Lerp(arrow.color, new Color(arrow.color.r, arrow.color.g, arrow.color.b, 1), Time.deltaTime * fadeSpeed);

			// Remaps the screen position to 2D.
			Vector2 onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2;

			// Gets the largest offset.
			float max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y));

			// Undo the mapping.
			onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f);

			// Changes the view port position to screen space.
			transform.position = camera.ViewportToScreenPoint(onScreenPos);

			// Adds an offset.
			transform.position = Vector3.Lerp(transform.position, camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)), edgeOffset);

			// Rotates the arrow to face the player.
			Vector3 dir = transform.position - camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			angle -= 90.0f;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}