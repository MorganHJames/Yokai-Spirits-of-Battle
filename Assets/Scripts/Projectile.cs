////////////////////////////////////////////////////////////
// File: Projectile.cs
// Author: Morgan Henry James
// Date Created: 02-02-2020
// Brief: A projectile that goes through the air.
/// If it hits an enemy do damage.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// A projectile that goes through the air.
/// If it hits an enemy do damage.
/// </summary>
public class Projectile : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The rigid body of the character.
	/// </summary>
	private Rigidbody rb;
	#endregion
	#region Public
	/// <summary>
	/// The damage the attack does.
	/// </summary>
	[Tooltip("The damage the projectile does.")]
	[HideInInspector] public float damage = 1.0f;

	/// <summary>
	/// The direction of knock back added to the direction away from the player.
	/// </summary>
	[Tooltip("The direction of knock back added to the direction away from the projectile.")]
	[HideInInspector] public Vector3 knockBackDirectionAddition = new Vector3(0.0f, 0.1f, 0.0f);

	/// <summary>
	/// The knock back the attack does.
	/// </summary>
	[HideInInspector] public float knockBack = 1.0f;

	/// <summary>
	/// The speed the projectile travels.
	/// </summary>
	[HideInInspector] public float speed = 1.0f;

	/// <summary>
	/// The fighter that the projectile belongs to.
	/// </summary>
	[HideInInspector] public FighterController ownerFighter;

	/// <summary>
	/// The lifetime of the projectile.
	/// </summary>
	[HideInInspector] public float lifetime = 10.0f;
	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Destroys the project at the end of its life.
	/// Sets the rigid body of the projectile.
	/// </summary>
	private void Start()
	{
		Destroy(this.gameObject, lifetime);
		rb = GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Moves the projectile forward
	/// </summary>
	private void FixedUpdate()
	{
		rb.AddForce(transform.forward * speed);
	}

	/// <summary>
	/// Called when the attack hit box hits another trigger box.
	/// </summary>
	/// <param name="other">The object it collides with.</param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			FighterController opponent = other.GetComponentInParent<FighterController>();

			if (opponent != ownerFighter)
			{
				if (opponent.currentCharacterState == FighterController.CharacterStates.Blocking)
				{
					opponent.currentBlock -= damage;
					opponent.characterAudio.BlockedAttackSound();
				}
				else if (opponent.currentCharacterState == FighterController.CharacterStates.Respawning)
				{
					// Do nothing.
				}
				else
				{
					opponent.damageTaken += damage;
					Vector3 directionForKnockBack = (opponent.transform.position - transform.position).normalized;
					opponent.rb.AddForce((directionForKnockBack + knockBackDirectionAddition) * (knockBack * opponent.damageTaken), ForceMode.Impulse);

					opponent.characterAudio.HurtSound();

					if (opponent.currentCharacterState == FighterController.CharacterStates.ChargingOrRecover || opponent.currentCharacterState == FighterController.CharacterStates.ChargingOrRecoverNoMovement)
					{
						opponent.SetState(FighterController.CharacterStates.Interrupted);
					}

				}
				Destroy(this.gameObject);
			}
		}
		else if (other.CompareTag("Attack"))
		{
			FighterController opponent = other.GetComponentInParent<FighterController>();

			if (opponent != ownerFighter)
			{
				Destroy(this.gameObject);
			}
		}
		else if (other.CompareTag("Projectile"))
		{
			if (other.GetComponent<Projectile>().ownerFighter != ownerFighter)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}