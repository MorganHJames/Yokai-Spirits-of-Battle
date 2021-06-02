////////////////////////////////////////////////////////////
// File: FallingTile.cs
// Author: Morgan Henry James
// Date Created: 29-05-2020
// Brief: Makes the tile fall after the player walks on it.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Makes the tile fall after the player walks on it.
/// </summary>
public class FallingTile : MonoBehaviour
{
    #region Variables
    #region Private
    /// <summary>
    /// The animator for the falling tile.
    /// </summary>
    [Tooltip("The animator for the falling tile.")]
    [SerializeField] private Animator animator = null;

    /// <summary>
    /// The time it takes for the tile to fall whilst the player is atop.
    /// </summary>
    [Tooltip("The time it takes for the tile to fall whilst the player is atop.")]
    [SerializeField] private float timeToFall = 2.0f;

    /// <summary>
    /// The time the player has been on the tile.
    /// </summary>
    private float currentTimeOnTile = 0.0f;
    #endregion
    #region Public

    #endregion
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// Called when the water falling tile box hits another trigger box.
    /// </summary>
    /// <param name="other">The object it collides with.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTimeOnTile += Time.deltaTime;

            if (currentTimeOnTile >= timeToFall)
            {
                animator.Play("FallingTile");
                currentTimeOnTile = 0.0f;
            }
        }
    }
    #endregion
    #region Public

    #endregion
    #endregion
}