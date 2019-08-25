using UnityEngine;
using UnityEngine.UI;

public class LonLatToUI : MonoBehaviour
{
    // テキストテンプレート
    private const string LonLatInfoTemplate = "緯度: {0}\n経度: {1}\n住所: {2}";

    // 表示用テキストUIオブジェクト
    private Text lonLatInfo;

    // 緯度経度取得オブジェクト
    private LonLatGetter lonLatGetter;

    // 逆ジオコーディングオブジェクト
    private LonLatToAddr lonLatToAddr;

    // Start is called before the first frame update
    private void Start()
    {
        // テキストラベルオブジェクトを取得
        lonLatInfo = GameObject.Find("LonLatInfo").GetComponent<Text>();

        // 経度緯度取得オブジェクトを取得
        lonLatGetter = GetComponent<LonLatGetter>();

        // 逆ジオコーディングオブジェクトを取得
        lonLatToAddr = GetComponent<LonLatToAddr>();
    }

    // Update is called once per frame
    void Update()
    {
        // 経度緯度の値をテキストUIに反映

        // 経度緯度の値を取得できるか判定
        if (lonLatGetter.CanGetLonLat())
        {
            StartCoroutine(lonLatToAddr.GetAddrFromLonLat(lonLatGetter.Longitude, lonLatGetter.Latitude));
            lonLatInfo.text = string.Format(LonLatInfoTemplate, lonLatGetter.Latitude.ToString(), lonLatGetter.Longitude.ToString(), lonLatToAddr.Address);
        }
        else
        {
            lonLatInfo.text = string.Format(LonLatInfoTemplate, "測定不能", "測定不能", "測定不能");
        }
    }
}
