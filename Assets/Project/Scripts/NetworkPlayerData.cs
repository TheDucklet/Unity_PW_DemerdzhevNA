using UnityEngine;
using Unity.Netcode;

/// <summary>
/// Структура кастомного сетевого пакета данных PTS (Player Tracking State)
/// </summary>
public struct PlayerTrackingState : INetworkSerializable
{
    public Vector3 Position;
    public Quaternion Rotation;
    public int Score;
    public float Health;

    // Метод бинарной сериализации пакета
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Position);
        serializer.SerializeValue(ref Rotation);
        serializer.SerializeValue(ref Score);
        serializer.SerializeValue(ref Health);
    }
}

/// <summary>
/// Сетевой скрипт обработки и синхронизации пакетов данных игрока
/// </summary>
public class NetworkPlayerData : NetworkBehaviour
{
    private PlayerStats localStats;

    private void Start()
    {
        localStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        // Только локальный владелец объекта отправляет пакет своего состояния
        if (!IsOwner) return;

        PlayerTrackingState ptsPacket = new PlayerTrackingState
        {
            Position = transform.position,
            Rotation = transform.rotation,
            Score = localStats != null ? localStats.Score : 0,
            Health = localStats != null ? localStats.Health : 100f
        };

        // Отправка пакета данных на сервер
        SubmitPTSPacketServerRpc(ptsPacket);
    }

    [ServerRpc]
    private void SubmitPTSPacketServerRpc(PlayerTrackingState packet)
    {
        // Сервер получает пакет и рассылает всем клиентам
        ReceivePTSPacketClientRpc(packet);
    }

    [ClientRpc]
    private void ReceivePTSPacketClientRpc(PlayerTrackingState packet)
    {
        if (!IsOwner)
        {
            // Обновляем виртуальное положение и данные стороннего игрока
            transform.position = Vector3.Lerp(transform.position, packet.Position, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Slerp(transform.rotation, packet.Rotation, Time.deltaTime * 15f);
        }
    }
}