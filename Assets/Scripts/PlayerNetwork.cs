using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerNetwork : NetworkBehaviour {

	private NetworkVariable<int> randomNumber = 
	new NetworkVariable<int>(
		1, 
		NetworkVariableReadPermission.Everyone, 
		NetworkVariableWritePermission.Owner);

	[SerializeField] private float speed;
	[SerializeField] private TextMeshProUGUI valueText;

	public Slider healthBar;
	public TextMeshProUGUI playerNameText;

	public NetworkVariable<float> health = new NetworkVariable<float>(100f);
	public NetworkVariable<Color> playerColor = new NetworkVariable<Color>(Color.white);
	
	[SerializeField] private Camera playerCamera;
	[SerializeField] private GameObject playerFollowCamera;
	[SerializeField] private GameObject playerCameraRoot;
	[SerializeField] private GameObject uiCanvas;
	[SerializeField] private GameObject uiEventSystem;
	[SerializeField] private AudioListener listener;

	public bool checkIsOwner;
	
	public override void OnNetworkSpawn()
	{
		// playerNameText.text = "Player " + OwnerClientId ; 
		// randomNumber.OnValueChanged += HandleRandomNumberChanged;
		SetLocalPlayerItems(IsOwner);
		checkIsOwner = IsOwner; 
	}

	private void SetLocalPlayerItems(bool isPlayerOwner)
	{
		playerCamera.gameObject.SetActive(isPlayerOwner);
		playerFollowCamera.gameObject.SetActive(isPlayerOwner);
		playerCameraRoot.gameObject.SetActive(isPlayerOwner);
		uiCanvas.gameObject.SetActive(isPlayerOwner);
		uiEventSystem.gameObject.SetActive(isPlayerOwner);
		listener.gameObject.SetActive(isPlayerOwner);
	}

	public override void OnNetworkDespawn() => randomNumber.OnValueChanged -= HandleRandomNumberChanged;

	private void Update()
	{
		if (!IsOwner) return;

		MovePlayer();

		// Change color to random color
		if (Input.GetKeyDown(KeyCode.C)) 
			ChangeColor(new Color(Random.value, Random.value, Random.value));
		
		// Reduce health
		if (Input.GetKeyDown(KeyCode.H)) ModifyHealth(-10f);
		
		// Increase health
		if (Input.GetKeyDown(KeyCode.H)) ModifyHealth(+10f);
		
		if (Input.GetKeyDown(KeyCode.N)) randomNumber.Value = Random.Range(0, 100);
		if (Input.GetKeyDown(KeyCode.T)) TestServerRpc("Hola", 3, true);
	}
	
	private void MovePlayer()
	{
		var moveX = Input.GetAxis("Horizontal");
		var moveZ = Input.GetAxis("Vertical");

		var moveDir = new Vector3(moveX, 0, moveZ);
		transform.Translate(moveDir * speed * Time.deltaTime) ;
	}

	[ServerRpc]
	private void TestServerRpc(string str, int n, bool b)
	{
		Debug.Log("TestServerRpc called");
	}
	
	[ServerRpc]
	private void ChangeColorServerRpc(Color newColor)
	{
		Debug.Log($"Change Color called by: {OwnerClientId}, message: {newColor}");
		playerColor.Value = newColor;
	}
	
	[ServerRpc]
	private void ModifyHealthServerRpc(float amount)
	{
		health.Value = Mathf.Clamp(health.Value + amount, 0, 100);
		Debug.Log("ModifyHealthServerRpc called");
	}

    [ClientRpc]
    public void TestClientRpc()
    {
	    Debug.Log("TestClientRpc called by ClientRpc.");
    }
    
    public void ChangeColor(Color color)
    {
	    ChangeColorServerRpc(color);
    }

    public void ModifyHealth(float amount)
    {
	    ModifyHealthServerRpc(amount);
    }
    
    private void HandleRandomNumberChanged(int previousValue, int newValue)
    {
	    Debug.Log("handle random number changed from: " + previousValue + " to " + newValue);
	    valueText.text = "value: " + newValue;
    }
}
