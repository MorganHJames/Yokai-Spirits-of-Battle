////////////////////////////////////////////////////////////
// File: Attack.cs
// Author: Morgan Henry James
// Date Created: 01/02/2020
// Brief: Controls everything to do with attacking.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Controls everything to do with attacking.
/// </summary>
public class Attack : MonoBehaviour
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
	/// The percentage difference needed to out prioritize another attack.
	/// </summary>
	static private float damageDiffence = 8.0f;
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

			if (opponent != parent)
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
					Vector3 directionForKnockBack = (opponent.transform.position - transform.parent.position).normalized;
					opponent.rb.AddForce((directionForKnockBack + knockBackDirectionAddition) * (knockBack * opponent.damageTaken), ForceMode.Impulse);

					opponent.characterAudio.HurtSound();

					if (opponent.currentCharacterState == FighterController.CharacterStates.ChargingOrRecover || opponent.currentCharacterState == FighterController.CharacterStates.ChargingOrRecoverNoMovement)
					{
						opponent.SetState(FighterController.CharacterStates.Interrupted);
					}
				}
			}
		}
		else if (other.CompareTag("Attack"))
		{
			FighterController opponent = other.GetComponentInParent<FighterController>();
			FighterController parent = GetComponentInParent<FighterController>();

			if (opponent != parent)
			{
				Attack opposingAttack = other.GetComponent<Attack>();

				float difference = ((damage - opposingAttack.damage) / ((damage + opposingAttack.damage) / 2.0f)) * 100.0f;

				// If within or below damage difference both cancel.
				if (Mathf.Abs(difference) <= damageDiffence || opposingAttack.damage > damage)
				{
					parent.SetState(FighterController.CharacterStates.Clashed);
				}
			}
		}
	}
	#endregion
	#region Public

	#endregion
	#endregion
}