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
    //public AudioSource bgmSource;

    // 宣言と初期化
    public int stage = 0; // 進行度の管理(ディレクトリの表示状況のコントロール等)
    public string input = "Text"; // 入力された文字列を格納

    // パラメータ
    public float waitTime = 0.5f; // テキストの最低表示時間
    public string user = "俺くん";
    public string chiki = "チキ"; // チキ(チュートリアルのキャラクター)の名前
    public string chikiImg = "chiki"; // チキの画像の名前
    public string unknownName = "???"; // 不明なキャラクターの名前


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
        characterName = characterName.GetComponent<Text>();
        characterImage = characterImage.GetComponent<Image>();
        //bgmSource = bgmSource.GetComponent<AudioSource>();

        StartCoroutine(Chapter1());
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Mute");
        }
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
        while (true)
        {
            resetInput();
            characterImage.sprite = null;
            directoryText.text = "";
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
        DisplayDirectory(null);
        DisplayCharacterName(user);
        DisplayText("「...ここは？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        goto jump;

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

        DisplayCharacterName(unknownName);
        DisplayText("「あなた、ここで何をしてるの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「？」\n後ろから突然声をかけられたので振り返ってみたが、そこには誰もいない。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「どこ見てるの、下よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(user);
        DisplayText("「下？」\n言われた通り下を見てみると、そこには変な生物が浮いていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「うわっ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「うわって何よ、酷い反応ね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「だって浮いてる生物なんて見たことなかったから……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「見たことない……？あなた、さては飛ばされた人？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「飛ばされた？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("意味の分からない事を言われたので聞き返してみる");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「そう。あなた、sshしなかった？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「えす、えす、えいち…？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「…何も知らないままsshしたのね。アクセス先を指定せずsshすると、エラーを起こしてここに飛ばされるのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「そういう人は時々見かけるけど、私たちはそういう人を『迷い人』と呼んでいるわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「何を言って──」\nと言いかけたが、ふと脳裏に倒れる前の記憶が脳裏に蘇ってきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("そうだ、私はさっきまで学校の授業でパソコンのコマンド？とやらについて習ってたんだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("退屈だったからほぼ寝てたけど、ふと目を覚ました時に先生が「sshは遠く離れたコンピュータと通信するための──」って言ってたのを思い出して興味本位で自宅のパソコンでやってみたんだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("そしたらここに──");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「思い出した──ってそうじゃない！おい変なの、どうしたら元の世界に帰れる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText($"「変なのじゃない、私は{chiki}よ。元の世界に帰りたいなら、どこかにあるっていうここと元の世界とをつなぐ『ゲート』を探す必要があるわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「...しかし、あなた運がいいわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「運がいい？どういうこと？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「だって『ゲート』は目の前に見えている街にあるって噂だもの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「──えぇぇぇぇぇぇぇぇぇぇぇぇぇぇぇぇ！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ほら、いい情報もあったんだからぼさぼさしないで街に入るわよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「入るって言っても、さっきから街に向かっていくら歩いても入口とか出てこないんだが。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「.......あなた<color=orange>cd</color>も知らないの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「しー、でぃー...?」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「......知らないのね。じゃあ教えてあげるわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「<color=orange>cd</color>ってのはね、移動するために使うコマンドなのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「なるほど」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayCharacterName(chiki);
        DisplayText("「試しに、<color=orange>cd</color> cyber_entrance って打ってみて」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd cyber_entrance")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayText("「違うわ、<color=orange>cd</color> cyber_entrance よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayText("「そう、その調子よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayText("「そしたら、さっきと同じように cyber_central に行ってみて。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd cyber_central")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayText("「違うわ、マップの移動をするときは<color=orange>cd</color>を使うのよ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayText("「いい感じね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayText("「そうそう、マップを見たいときは<color=orange>pwd</color>を使うのよ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "pwd")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayText("「違うわ、マップを見たいときは<color=orange>pwd</color>と打つの。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        stage = 2;
        DisplayDirectory("cyber_central");
        DisplayText("「マップが見えるでしょ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        jump:
        DisplayText("「これで自分が今どこにいるかを見ることができるわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「なるほど...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter2());
    }

    IEnumerator Chapter2()
    {
        stage = 3;
        DisplayDirectory("cyber_central");

        DisplayCharacterName(chiki);
        DisplayText("「この世界では、あなたや私が喋っている日本語以外に『Linuxコマンド』という言葉があるわ。」");
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
            text = "World\n" +
                    "\t├ Sea\n" +
                    "\t└ Mountain\n";
        }

        if (stage == 1)
        {
            text = "";
        }

        if (stage == 2)
        {
            text = "World\n" +
                    "\t└ cyber_entrance\n" +
                    "\t\t└ cyber_central\n";
        }

        if (stage == 3)
        {
            text = "└ cyber_central\n";
        }
        if (currentLocation != null)
        {
            text = text.Replace(currentLocation, currentLocation + " *");
        }

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
    public void DisplayCharacterImage(string path)
    {
        characterImage.sprite = Resources.Load<Sprite>("Pic/" + path);
    }

    // public void playBgm(string path)
    // {
    //     bgmSource.Play(Resources.Load<AudioClip>("Bgm/" + path));
    // }

    /// <summary>
    /// 入力欄をリセットする
    /// 入力を促すテキストの前に置く
    /// </summary>
    public void resetInput()
    {
        inputField.text = "";
        inputField.enabled = true;
    }

    // IEnumerator Test(Block[] blocks)
    // {
    //     int id = blocks[0].id;
    //     while (true)
    //     {
    //         switch (blocks[id].type)
    //         {
    //             case Type.Strory:
    //                 if (blocks[id].stage != null)
    //                 {
    //                     stage = blocks[id].stage;
    //                 }
    //                 inputField.enabled = false;
    //                 if (blocks[id].background != null)
    //                 {
    //                     DisplayImage(blocks[id].background);
    //                 }
    //                 if (blocks[id].currentLocation != null)
    //                 {
    //                     DisplayDirectory(blocks[id].currentLocation);
    //                 }
    //                 if (blocks[id].characterName != null)
    //                 {
    //                     DisplayCharacterName(blocks[id].characterName);
    //                 }
    //                 DisplayText(blocks[id].text);
    //                 yield return new WaitForSeconds(waitTime);
    //                 yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
    //                 id = blocks[id].nextId;
    //                 break;
    //             default:
    //         }
    //     }
    // }

    // enum Type
    // {
    //     Story,
    //     Loop
    // }

    // struct Block
    // {
    //     int id;
    //     Type type;
    //     string text;
    //     string characterName;
    //     bool input;
    //     int stage;
    //     string background;
    //     string currentLocation;
    //     int nextId;
    // }

    // Block[] test = new Block[]
    // {
    //     new Block(0, Type.Story, "「…ここは？」", user, false, 1, null, "World"),
    //     new Block(1, Type.Story, "目を開けると、わたしは見知らぬ世界にいた。", null, false, null, null, null)
    // };
}
