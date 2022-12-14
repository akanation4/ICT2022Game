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
    public string userName = "Unknown";
    public string input = "Text";

    public float waitTime = 1.0f;


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

        StartCoroutine(Main());
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {

    }

    IEnumerator Main()
    {
        /* 000 */
        inputField.enabled = false;
        DisplayText("Hello, world");
        DisplayImage("start");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 001 */
        resetInput();
        DisplayText("What is your name?");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 002 */
        inputField.enabled = false;
        userName = inputField.text;
        DisplayText("Hello, " + userName);
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 003 */
        resetInput();
        DisplayText("Whitch do you want to go? (sea or mountain)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 004 */
        input = inputField.text;
        if (input == "sea")
        {
            StartCoroutine(Sea());
        }
        else if (input == "mountain")
        {
            StartCoroutine(Mountain());
        }
        else
        {
            DisplayText("Oops, I don't know that place");
        }

    }

    IEnumerator Sea()
    {
        inputField.enabled = false;
        DisplayText("You are in the sea");
        DisplayImage("sea");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayText("Do you want to go to the mountain? (yes or no)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        input = inputField.text;
        if (input == "yes")
        {
            StartCoroutine(Mountain());
        }
        else if (input == "no")
        {
            DisplayText("You are in the sea");
        }
        else
        {
            DisplayText("Oops, I don't know that answer");
        }
    }

    IEnumerator Mountain()
    {
        inputField.enabled = false;
        DisplayText("You are in the mountain");
        DisplayImage("mountain");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayText("Do you want to go to the sea? (yes or no)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        input = inputField.text;
        if (input == "yes")
        {
            StartCoroutine(Sea());
        }
        else if (input == "no")
        {
            DisplayText("You are in the mountain");
        }
        else
        {
            DisplayText("Oops, I don't know that answer");
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

    public void resetInput()
    {
        inputField.text = "";
        inputField.enabled = true;
    }
}
