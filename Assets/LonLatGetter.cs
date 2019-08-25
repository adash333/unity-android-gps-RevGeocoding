using System.Collections;
using UnityEngine;

// 緯度経度取得クラス
public class LonLatGetter : MonoBehaviour
{
    // 緯度経度取得間隔（秒）
    private const float IntervalSeconds = 1.0f;

    // ロケーションサービスのステータス
    private LocationServiceStatus locationServiceStatus;

    // 緯度
    public float Longitude { get; private set; }

    // 経度
    public float Latitude { get; private set; }

    // 緯度経度情報が取得可能か
    public bool CanGetLonLat()
    {
        return Input.location.isEnabledByUser;
    }

    // 緯度経度取得処理
    // 一定時間毎に非同期実行するための戻り値
    private IEnumerator Start()
    {
        while (true)
        {
            locationServiceStatus = Input.location.status;
            if (Input.location.isEnabledByUser)
            {
                switch (locationServiceStatus)
                {
                    case LocationServiceStatus.Stopped:
                        Input.location.Start();
                        break;
                    case LocationServiceStatus.Running:
                        Longitude = Input.location.lastData.longitude;
                        Latitude = Input.location.lastData.latitude;
                        break;
                    default:
                        break;
                }
            }

            yield return new WaitForSeconds(IntervalSeconds);
        }
    }
}