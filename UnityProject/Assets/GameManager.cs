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
    public Text characterName;
    public Image characterImage;

    // 宣言と初期化
    public int stage = 0; // 進行度の管理(ディレクトリの表示状況のコントロール等)
    public string input = "Text"; // 入力された文字列を格納

    // パラメータ
    public float waitTime = 1.0f; // テキストの最低表示時間
    public string user = "俺くん";
    public string chiki = "チキ"; // チキ(チュートリアルのキャラクター)の名前
    public string chikiImg = "chiki"; // チキの画像の名前


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

        StartCoroutine(Chapter0());
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// ストーリーの進行例
    /// </summary>
    IEnumerator Main()
    {
        /* 000 */
        inputField.enabled = false;
        DisplayText("Hello, world");
        DisplayImage("start");
        DisplayDirectory("World");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 001 */
        resetInput();
        DisplayText("What is your name?");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 002 */
        inputField.enabled = false;
        user = inputField.text;
        DisplayText("Hello, " + user);
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
        /* 005 */
        inputField.enabled = false;
        DisplayText("You are in the sea");
        DisplayImage("sea");
        DisplayDirectory("Sea");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 006 */
        resetInput();
        DisplayText("Do you want to go to the mountain? (yes or no)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 007 */
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
        /* 008 */
        inputField.enabled = false;
        DisplayText("You are in the mountain");
        DisplayImage("mountain");
        DisplayDirectory("Mountain");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 009 */
        resetInput();
        DisplayText("Do you want to go to the sea? (yes or no)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        /* 010 */
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

    IEnumerator Chapter0()
    {
        while(true)
        {
            resetInput();
            DisplayCharacterName("***");
            DisplayText("あなたの名前を入力してください");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            inputField.enabled = false;
            user = inputField.text;
            DisplayText(user + "さんですね");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            resetInput();
            DisplayText("このままはじめますか？(y/n)");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "y")
            {
                break;
            }
            else
            {
                continue;
            }
        }
        StartCoroutine(Chapter1());
    }

    IEnumerator Chapter1()
    {
        stage = 1;
        inputField.enabled = false;
        // DisplayImage("start");
        DisplayDirectory("World");
        DisplayCharacterName(user);
        DisplayText("「...ここは？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("目を開けると、私は見知らぬ世界にいた。いや、植物や動物はどことなく見たことはあるので完全に知らない世界というわけではないが、なんとなく雰囲気が違う気がするのだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("道に倒れてたのでおそらく意識を失っていたのだと思うのだが、その前の記憶が思い出せない。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「とりあえず、止まってても何も始まらないだろうし、道沿いに進んでみるか。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("私は道沿いに見える街に向かって歩みを進めた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("…のだが。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「おかしい、街は近くに見えてるはずなのに、一向に入口すら見えてこない。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("街に向かって歩いてるはずなので、そろそろ街の入口や家のひとつくらい見えて来るはずなのに、一向にそのようなものが見えてこないのである。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「方向音痴ではなかったはずなのだが、さて、どうしたものか...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("???");
        DisplayText("「あなた、ここで何をしてるの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「？」\n後ろから突然声をかけられたので振り返ってみたが、そこには誰もいない。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("???");
        DisplayText("「どこ見てるの、下よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacter(chikiImg);
        DisplayCharacterName(user);
        DisplayText("「下？」\n言われた通り下を見てみると、そこには変な生物が浮いていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「うわっ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));





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
        if (stage == 0)
        {
            text =  "World\n" +
                    "\t├ Sea\n" +
                    "\t└ Mountain\n";
        }

        if (stage == 1)
        {
            text =  "World\n";
        }

        text = text.Replace(currentLocation, currentLocation + " *");
        directoryText.text = text;
    }

    /// <summary>
    /// キャラクターの名前を表示する
    /// </summary>
    public void DisplayCharacterName(string name)
    {
        characterName.text = name;
    }

    /// <summary>
    /// キャラクターの画像を表示する
    /// </summary>
    public void DisplayCharacter(string path)
    {
        characterImage.sprite = Resources.Load<Sprite>("Pic/" + path);
    }

    /// <summary>
    /// 入力欄をリセットする
    /// 入力を促すテキストの前に置く
    /// </summary>
    public void resetInput()
    {
        inputField.text = "";
        inputField.enabled = true;
    }
}
