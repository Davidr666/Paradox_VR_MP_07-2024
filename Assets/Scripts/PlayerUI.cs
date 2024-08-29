using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text healthText;
    public Slider healthBar;

    private void Update()
    {
        var playerNetwork = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerNetwork>();
        if (playerNetwork != null)
        {
            healthBar.value = playerNetwork.health.Value;
            healthText.text = $"Health: {playerNetwork.health.Value}";
        }
    }
}