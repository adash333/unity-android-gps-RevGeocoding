using System.Collections;
using System.Collections.Generic;
using MiniJSON;
// https://gist.github.com/darktable/1411710 から
// MiniJSON.csをダウンロードしてProjectタブ > Plugins　フォルダに入れる必要あり
using UnityEngine;

public class LonLatToAddr : MonoBehaviour
{
    // APIのパラメータテンプレートつきURL
    private const string ApiBaseUrl = "https://www.finds.jp/ws/rgeocode.php?json&lon={0}&lat={1}";

    // 住所文字列
    public string Address { get; private set; }

    // 経度緯度から住所文字列を取得
    // 遅延評価用戻り値
    public IEnumerator GetAddrFromLonLat(float longitude, float latitude)
    {
        // URLにパラメータを埋め込み
        string url = string.Format(ApiBaseUrl, longitude, latitude);

        // APIを実行して経度緯度を保持
        using (WWW www = new WWW(url))
        {
            // API非同期実行用yield return
            yield return www;

            // 結果JSONのデシリアライズ
            var desirializedData = (Dictionary<string, object>)Json.Deserialize(www.text);

            // 成功した場合のみ処理
            if ((long)desirializedData["status"] == 200)
            {
                // 都道府県＋市区町村を文字列として取得
                var result = (Dictionary<string, object>)desirializedData["result"];
                var prefecture = (Dictionary<string, object>)result["prefecture"];
                var municipality = (Dictionary<string, object>)result["municipality"];
                Address = (string)prefecture["pname"] + " " + (string)municipality["mname"];
            }
        }
    }
}

