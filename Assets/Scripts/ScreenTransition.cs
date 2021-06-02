////////////////////////////////////////////////////////////
// File: ScreenTransition.cs
// Author: Morgan Henry James
// Date Created: 21-05-2020
// Brief: Handles the screen transition.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the screen transition.
/// </summary>
public class ScreenTransition : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The screen transition animator.
	/// </summary>
	[Tooltip("The screen transition animator.")]
	[SerializeField] private Animator screenTransitionAnimator = null;

	/// <summary>
	/// The scene name to go to.
	/// </summary>
	private string sceneToGoToName = "";
    #endregion
    #region Public
    
    #endregion
    #endregion
    
    #region Methods
    #region Private
	/// <summary>
	/// Goes to the set scene.
	/// </summary>
    private void GoToNextScene()
	{
		SceneManager.LoadScene(sceneToGoToName, LoadSceneMode.Single);
	}
    #endregion
    #region Public
	/// <summary>
	/// Starts the screen transition animation.
	/// Sets the next scene to go to.
	/// Goes to said scene after a delay.
	/// </summary>
	/// <param name="transitionTime"></param>
	/// <param name="sceneName"></param>
    public void ScreenTransistion(float transitionTime, string sceneName)
	{
		screenTransitionAnimator.Play("ScreenTransitionEnd");
		sceneToGoToName = sceneName;
		Invoke("GoToNextScene", transitionTime);
	}
    #endregion
    #endregion
}