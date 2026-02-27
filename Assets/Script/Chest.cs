using UnityEngine;

public class Chest : MonoBehaviour
{
	public Canvas canvas;
	public GameEvent<Chest> onOpenChest;
	public readonly Inventory inventory;
	public bool isPlayerEnter = false;

	void Start()
	{
		canvas.gameObject.SetActive(false);
	}

	void Update()
	{
		if (isPlayerEnter && Input.GetKeyDown(KeyCode.E))
		{
			onOpenChest.Raise(this);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			canvas.gameObject.SetActive(true);
			isPlayerEnter = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			canvas.gameObject.SetActive(false);
			isPlayerEnter = false;
		}
	}
}
