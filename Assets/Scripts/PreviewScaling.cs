////////////////////////////////////////////////////////////
// File: PreviewScaling.cs
// Author: Morgan Henry James
// Date Created: 01-04-2020
// Brief: Contains two functions one to scale up and the other to scale down and delete.
//////////////////////////////////////////////////////////// 

using UnityEngine;

/// <summary>
/// Contains two functions one to scale up and the other to scale down and delete.
/// </summary>
public class PreviewScaling : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// True when scaling up.
	/// </summary>
	private bool scalingUp = true;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Applies the actual scaling.
	/// </summary>
	private void Update()
	{
		if (scalingUp)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 2.25f, Time.deltaTime * 10.0f);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10.0f);

			if (transform.localScale == Vector3.zero)
			{
				Destroy(gameObject);
			}
		}
	}
	#endregion
	#region Public
	/// <summary>
	/// Scales down the model.
	/// </summary>
	public void ScaleDown()
	{
		scalingUp = false;
	}
    #endregion
    #endregion
}