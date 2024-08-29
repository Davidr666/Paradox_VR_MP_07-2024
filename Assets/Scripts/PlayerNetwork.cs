using System;
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

	// Change player color
	public Renderer playerRenderer;
	public Slider healthBar;
	public TextMeshProUGUI playerNameText;

	public NetworkVariable<float> health = new NetworkVariable<float>(100f);
	public NetworkVariable<Color> playerColor = new NetworkVariable<Color>(Color.white);
	
	public override void OnNetworkSpawn()
	{
		playerNameText.text = "Player " + OwnerClientId ; 
		randomNumber.OnValueChanged += HandleRandomNumberChanged;
		if(IsLocalPlayer) ChangeColor(Color.cyan);
	}

	public override void OnNetworkDespawn() => randomNumber.OnValueChanged -= HandleRandomNumberChanged;

	private void Start()
	{
		// Change the color of the local player
		//if(IsLocalPlayer) ChangeColor(Color.cyan);
	}

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
		Debug.Log($"Test ServerRpc called by: {OwnerClientId}, message: {str}");
		Debug.Log("is server: " + IsServer);
		Debug.Log("is client: " + IsClient);
		Debug.Log("is local player: " + IsLocalPlayer);
	}
	
	[ServerRpc]
	private void ChangeColorServerRpc(Color newColor)
	{
		Debug.Log("Change color called by ServerRpc.");
		Debug.Log("is server: " + IsServer);
		Debug.Log("is client: " + IsClient);
		Debug.Log("is local player: " + IsLocalPlayer);
		playerColor.Value = newColor;
		playerRenderer.material.color = playerColor.Value;
	}
	
	[ServerRpc]
	private void ModifyHealthServerRpc(float amount)
	{
		health.Value = Mathf.Clamp(health.Value + amount, 0, 100);
		Debug.Log("ModifyHealthServerRpc called by ServerRpc.");
		Debug.Log("is server: " + IsServer);
		Debug.Log("is client: " + IsClient);
		Debug.Log("is local player: " + IsLocalPlayer);
	}

    [ClientRpc]
    public void TestClientRpc()
    {
        Debug.Log("Test ClientRpc called by: " + OwnerClientId);
        Debug.Log("is server: " + IsServer);
        Debug.Log("is client: " + IsClient);
        Debug.Log("is local player: " + IsLocalPlayer);
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
	    Debug.Log("Client id: " + OwnerClientId + ", random number: " + newValue);
	    valueText.text = "value: " + newValue;
    }

}
