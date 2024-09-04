using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour {
	[SerializeField] private Button serverBtn;
	[SerializeField] private Button hostBtn;
	[SerializeField] private Button clientBtn;

	private void Awake()
	{
		serverBtn.onClick.AddListener(()=>{
			NetworkManager.Singleton.StartServer();
			DisableMpButtons();
		});

		hostBtn.onClick.AddListener(() => {
			NetworkManager.Singleton.StartHost();
			DisableMpButtons();
		});

		clientBtn.onClick.AddListener(() => {
			NetworkManager.Singleton.StartClient();
			DisableMpButtons();
		});
	}

	private void DisableMpButtons()
	{
		serverBtn.gameObject.SetActive(false);
		hostBtn.gameObject.SetActive(false);
		clientBtn.gameObject.SetActive(false);
	}
}
