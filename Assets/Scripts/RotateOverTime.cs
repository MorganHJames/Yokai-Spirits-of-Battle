////////////////////////////////////////////////////////////
// File: RotateOverTime.cs
// Author: Morgan Henry James
// Date Created: 29-05-2020
// Brief: Rotates an object on the y axis over time.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Rotates an object on the y axis over time.
/// </summary>
public class RotateOverTime : MonoBehaviour
{
    #region Variables
    #region Private
    /// <summary>
    /// The speed of the rotation.
    /// </summary>
    [Tooltip("The speed of the rotation.")]
    [SerializeField] private float speed = 0.5f;

    /// <summary>
    /// The rotation direction.
    /// </summary>
    [Tooltip("The rotation direction.")]
    [SerializeField] private bool clockwise = true;
    #endregion
    #region Public

    #endregion
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// Rotates the object on the y axis.
    /// </summary>
    private void Update()
    {
        if (clockwise)
        {
            transform.RotateAround(transform.position, transform.forward, Time.deltaTime * speed);
        }
        else
        {
            transform.RotateAround(transform.position, transform.forward, Time.deltaTime * -speed);
        }
    }
    #endregion
    #region Public

    #endregion
    #endregion
}