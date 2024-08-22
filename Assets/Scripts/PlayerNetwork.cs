using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerNetwork : NetworkBehaviour {

	private NetworkVariable<int> randomNumber = 
	new NetworkVariable<int>(
		1, 
		NetworkVariableReadPermission.Everyone, 
		NetworkVariableWritePermission.Owner);

	[SerializeField] private float speed;
	[SerializeField] private TextMeshProUGUI valueText;

	public override void OnNetworkSpawn()
	{
		randomNumber.OnValueChanged += (int previousValue, int newValue) =>
		{
			Debug.Log("Client id: " + OwnerClientId + ", random number: " + randomNumber.Value);
			valueText.text = randomNumber.Value.ToString();
		};
	}

	private void Update()
	{
		if (!IsOwner)
			return;

		MovePlayer();

		if(Input.GetKeyDown(KeyCode.N))
			randomNumber.Value = Random.Range(0, 100);

	}

	private void MovePlayer()
	{
		var moveX = Input.GetAxis("Horizontal");
		var moveZ = Input.GetAxis("Vertical");

		Vector3 moveDir = new Vector3(moveX, 0, moveZ);
		transform.Translate(moveDir * speed * Time.deltaTime) ;
	}
}
