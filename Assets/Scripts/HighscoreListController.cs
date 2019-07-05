using UnityEngine;

public class HighscoreListController : MonoBehaviour
{
	private string level;

	public void SetLevel(string level)
	{
		this.level = level;
	}

	public void Load()
	{
		print("load " + level);
	}
}
