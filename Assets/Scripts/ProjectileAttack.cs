////////////////////////////////////////////////////////////
// File: ProjectileAttack.cs
// Author: Morgan Henry James
// Date Created: 02-02-2020
// Brief: Spawns a projectile on enable then disables itself.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Spawns a projectile on enable then disables itself.
/// </summary>
public class ProjectileAttack : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The damage the attack does.
	/// </summary>
	[Tooltip("The damage the attack does.")]
	[SerializeField] private float damage = 1.0f;

	/// <summary>
	/// The direction of knock back added to the direction away from the player.
	/// </summary>
	[Tooltip("The direction of knock back added to the direction away from the player.")]
	[SerializeField] private Vector3 knockBackDirectionAddition = new Vector3(0.0f, 0.1f, 0.0f);

	/// <summary>
	/// The knock back the attack does.
	/// </summary>
	[Tooltip("The knock back the attack does.")]
	[SerializeField] private float knockBack = 1.0f;

	/// <summary>
	/// The speed the projectile travels.
	/// </summary>
	[Tooltip("The speed the projectile travels.")]
	[SerializeField] private float speed = 1.0f;

	/// <summary>
	/// The lifetime of the projectile.
	/// </summary>
	[Tooltip("The lifetime of the projectile.")]
	[SerializeField] private float lifetime = 10.0f;

	/// <summary>
	/// The projectile to shoot.
	/// </summary>
	[Tooltip("The projectile to shoot.")]
	[SerializeField] private GameObject projectile = null;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Destroys the project at the end of its life.
	/// Sets the rigid body of the projectile.
	/// </summary>
	private void OnEnable()
	{
		GameObject spawnedProjectile = Instantiate(projectile, transform.position, transform.rotation, null);
		Projectile spawnedProjectileScript = spawnedProjectile.GetComponent<Projectile>();
		spawnedProjectileScript.damage = damage;
		spawnedProjectileScript.knockBackDirectionAddition = knockBackDirectionAddition;
		spawnedProjectileScript.knockBack = knockBack;
		spawnedProjectileScript.speed = speed;
		spawnedProjectileScript.lifetime = lifetime;
		spawnedProjectileScript.ownerFighter = GetComponentInParent<FighterController>();
	}
	#endregion
	#region Public

	#endregion
	#endregion
}