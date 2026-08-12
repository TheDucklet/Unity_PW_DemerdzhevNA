using UnityEngine;
using Unity.Netcode;

/// <summary>
/// Управляет запуском P2P-хоста и присоединением сетевых клиентов.
/// </summary>
public class NetworkP2PManager : MonoBehaviour
{
    private void OnGUI()
    {
        // Простейший GUI-интерфейс для выбора роли при старте
        GUILayout.BeginArea(new Rect(20, 20, 250, 150));

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Запустить Хост (Host/P2P)", GUILayout.Height(40)))
            {
                // Запуск сервера и локального клиента одновременно
                NetworkManager.Singleton.StartHost();
                Debug.Log("<color=cyan>[P2P]</color> Сетевой хост успешно запущен.");
            }

            if (GUILayout.Button("Присоединиться (Client)", GUILayout.Height(40)))
            {
                // Подключение к существующему хосту
                NetworkManager.Singleton.StartClient();
                Debug.Log("<color=green>[P2P]</color> Подключение к хосту...");
            }
        }
        else
        {
            GUILayout.Label($"Режим: {(NetworkManager.Singleton.IsHost ? "Host" : "Client")}");
            GUILayout.Label($"Сетевой ID: {NetworkManager.Singleton.LocalClientId}");

            if (GUILayout.Button("Отключиться", GUILayout.Height(30)))
            {
                NetworkManager.Singleton.Shutdown();
            }
        }

        GUILayout.EndArea();
    }
}