using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.VisualScripting;

public class PlayerNetwork : NetworkBehaviour {

	private NetworkVariable<int> randomNumber = 
	new NetworkVariable<int>(
		1, 
		NetworkVariableReadPermission.Everyone, 
		NetworkVariableWritePermission.Owner);

	[SerializeField] private float speed;
	[SerializeField] private TextMeshProUGUI valueText;
	[SerializeField] private GameObject sphere1;
	[SerializeField] private GameObject sphere2;
	[SerializeField] private GameObject cube1;

	public override void OnNetworkSpawn()
	{
        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log("Client id: " + OwnerClientId + ", random number: " + randomNumber.Value);
            valueText.text = "El nuevo valor es: " + randomNumber.Value.ToString();
            //ChangeObjects();
        };
	}

    private void ChangeObjects()
    {
        sphere1.gameObject.GetComponent<Renderer>().material.color = Color.green;
        sphere2.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        cube1.gameObject.SetActive(false);
    }

	private void Update()
	{
		if (!IsOwner)
			return;

		MovePlayer();

		if (Input.GetKeyDown(KeyCode.N))
		{
			randomNumber.Value = Random.Range(0, 100);
			//ChangeObjects();
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			TestServerRpc("Hola", 3, true);
		}
	}
	private void MovePlayer()
	{
		var moveX = Input.GetAxis("Horizontal");
		var moveZ = Input.GetAxis("Vertical");

		Vector3 moveDir = new Vector3(moveX, 0, moveZ);
		transform.Translate(moveDir * speed * Time.deltaTime) ;
	}

	[ServerRpc]
	public void TestServerRpc(string str, int n, bool b)
	{
		Debug.Log($"Test ServerRpc called by: {OwnerClientId}, message: {str}");
	}

    [ClientRpc]
    public void TestClientRpc()
    {
        Debug.Log("Test ClientRpc called by: " + OwnerClientId);
    }
}
