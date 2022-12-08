using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

/// <summary>
/// ゲームの管理を行うクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    // Unity上で設定する
    public Text mainText;
    public InputField inputField;
    public Image image;
    public Text directoryText;

    public int count = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        // コンポーネントを取得
        inputField = inputField.GetComponent<InputField>();
        mainText = mainText.GetComponent<Text>();
        image = image.GetComponent<Image>();
        directoryText = directoryText.GetComponent<Text>();

        /* 000 */
        inputField.enabled = false;
        DisplayText("Hello, world");
        DisplayImage("start");
        DisplayDirectory("world");
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        // エンターキーが押されたらストーリーを進行する
        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            switch (count)
            {
                /* 001 */
                case 1:
                    inputField.enabled = true;
                    DisplayText("What is your name?");
                    break;
                /* 002 */
                case 2:
                    inputField.enabled = false;
                    DisplayText("Hello, " + inputField.text);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// テキストを表示する
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    public void DisplayText(string text)
    {
        mainText.text = text;
    }

    /// <summary>
    /// 画像を表示する
    /// </summary>
    /// <param name="path">画像のパス(ファイル名・拡張子なし)</param>
    public void DisplayImage(string path)
    {
        image.sprite = Resources.Load<Sprite>("Pic/" + path);
    }

    /// <summary>
    /// ディレクトリを表示する
    /// </summary>
    /// <param name="currentLocation">現在のディレクトリ</param>
    public void DisplayDirectory(string currentLocation)
    {
        string text = "";
        if (count == 0)
        {
            text =  "world\n" +
                    "\t└ Hokkaido\n" +
                    "\t\t├ Sapporo\n" +
                    "\t\t└ Chitose\n" +
                    "\t\t\t└ CIST\n";
        }

        text = text.Replace(currentLocation, currentLocation + " *");
        directoryText.text = text;
    }
}
