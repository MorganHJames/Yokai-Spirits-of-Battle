////////////////////////////////////////////////////////////
// File: ScrollingTexture.cs
// Author: Morgan Henry James
// Date Created: 29-05-2020
// Brief: Scrolls the texture offset to make the image look like it is moving.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Scrolls the texture offset to make the image look like it is moving.
/// </summary>
public class ScrollingTexture : MonoBehaviour
{
    #region Variables
    #region Private
    /// <summary>
    /// How much the texture will scroll on the x axis.
    /// </summary>
    [Tooltip("How much the texture will scroll on the x axis.")]
    [SerializeField] private float scrollX = 0.5f;

    /// <summary>
    /// How much the texture will scroll on the y axis.
    /// </summary>
    [Tooltip("How much the texture will scroll on the y axis.")]
    [SerializeField] private float scrollY = 0.5f;

    /// <summary>
    /// The objects renderer.
    /// </summary>
    [Tooltip("The objects renderer.")]
    [SerializeField] private Renderer objectRenderer = null;
    #endregion
    #region Public

    #endregion
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// Applies the scrolling.
    /// </summary>
    private void Update()
    {
        objectRenderer.material.mainTextureOffset = new Vector2(objectRenderer.material.mainTextureOffset.x + scrollX * Time.deltaTime, objectRenderer.material.mainTextureOffset.y + scrollY * Time.deltaTime);
    }
    #endregion
    #region Public

    #endregion
    #endregion
}