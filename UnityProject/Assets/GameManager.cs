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
    public string pb = "pocketbook.txt"; // 手帳のファイル名
    public string mine = "ミーネ"; // エネ(敵キャラクター)の名前
    public string mineImg = "mine"; // エネの画像の名前


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

        StartCoroutine(Chapter3());
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
            DisplayImage(null);
            DisplayCharacterImage(null);
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

        //goto jump;

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
        DisplayText("「そう。あなた、<color=orange>ssh</color>しなかった？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「えす、えす、えいち…？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「…何も知らないまま<color=orange>ssh</color>したのね。アクセス先を指定せず<color=orange>ssh</color>すると、エラーを起こしてここに飛ばされるのよ。」");
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

        DisplayText("退屈だったからほぼ寝てたけど、ふと目を覚ました時に先生が「<color=orange>ssh</color>は遠く離れたコンピュータと通信するための──」って言ってたのを思い出して興味本位で自宅のパソコンでやってみたんだ。");
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

        DisplayCharacterName(user);
        DisplayText("「り、りな…何？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「Linuxコマンド、さっき使った<color=orange>cd</color>とか<color=orange>pwd</color>がそれよ。普段はパソコンのコマンドで使うのだけど、この世界では普通に使うのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「ちょっと待って、パソコン上で使うって、じゃあここはパソコンの中ってこと！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「間違ってはないわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「......パソコンの中ってもっとケーブルとか変な機械ばっかりだと思ってた。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「それは物理的な話。この世界はネットワーク、いわば仮想的な世界よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「普通は目に見えないけど、こういう感じでLinuxコマンドを使うことで場所を移動できたり、マップを表示したりすることができるのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「そうなんだ。ところで、あの光ってる手帳は何？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「光ってる？あなた、もしかして『視えてるの』？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「『視える』？何それ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「この世界の人の中でも一部の人だけができることよ。自分の望んでることの助けになってくれる人や物が淡い光を放っているように見えるの。それができる人は珍しいのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「望んでること...元の世界に帰りたいってこと？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayCharacterName(chiki);
        DisplayText("「そうね。だからあの手帳はきっとあなたの助けになると思うわ。ただ、普通に調べるだけではあなたの望んでる情報は得られないわ。<color=orange>cat</color> pocketbook.txt と打ってみて」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cat pocketbook.txt")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayText("「違うわ、<color=orange>cat</color> pocketbook.txt よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName(pb);
        DisplayText("これを読んでるということは、どうやら君も『迷い人』となってしまったようだね。大丈夫、僕も過去に迷い人になった身だけど、なんとか脱出できたから。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("他の人が脱出できるように、ここに脱出方法を書き残しておきます。\n\n脱出方法");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("①4つの数字を集める。\n②脱出するための秘密鍵を入手する。\n③『ゲート』に行き、脱出のためのコマンドを叫ぶ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("コマンド → <color=orange>ssh</color> -i (②の名前) (己の名前)@(①で集めた4つの数字)");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("これができれば、君も脱出することができる。\n長い旅になるかもしれないけど、どうか無事に帰って来れることを陰ながら祈ってます。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「元の世界に帰るための方法.......らしいけどさっぱりわからない。ディレクトリ？秘密鍵？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「...なるほどね。落ち着いて、一つずつ説明するから。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「どういうことかわかるの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ええ。まずディレクトリについて。フォルダはわかる？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「フォルダ？Wordファイルとかがあるあのフォルダ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そう、ディレクトリっていうのはいわゆるフォルダの事で、ファイルやフォルダを入れるための入れ物のことを指すの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「なるほど。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「次に秘密鍵。秘密鍵っていうのは暗号化された通信をもとに戻すために必要なの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「通信が暗号化？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「通信が暗号化されてなかったら部外者に何やろうとしてるか筒抜けになるでしょ。それを防止するために暗号化というのは行われるのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「確かに...。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「最後に4つの数字に関して。私も確証はないけど、多分これはIPアドレスを作れ、ということだと思うわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「IPアドレス？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そう。あなたの家に住所はあるでしょ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「それはそうだ。無いと配達物が届かないからな。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「それと同じよ。IPアドレスっていうのはパソコンやなどがネットワークに接続している時に個別に割り振られている識別番号の事。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「データを送る時や通信相手を指定する時に必要になるわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「つまり、それが無いと私たちは調べ物をしたりeメールを受け取ったりすることができないってこと！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ええ、ひいてはこの世界から脱出することもできないわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「それは困る！現実でやり残したことがたくさんあるからそれができないままなんてごめんだ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「それなら、頑張って4つの数字と秘密鍵を集めて、ゲートに向かうしかないわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「そうだね。さっさと探し始めよう。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("こうしてこの変な生物、チキとの旅が始まった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter3());
    }

    IEnumerator Chapter3() {
        DisplayCharacterName(user);
        DisplayText("「話を戻すけど、さっき言ってた『視える』ってやつ、どうやったらできるの？さっきはよくわからないままだったから。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        goto jump;
        resetInput();
        DisplayCharacterName(chiki);
        DisplayText("「そういえば教えてなかったわね。せっかくだからここで試してみましょ。<color=orange>ls</color>って打ってみて。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "ls")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        stage = 4;
        DisplayDirectory("cyber_central");
        DisplayText("「いくつかの道が光って見えない？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「今回は道が光って見えたけど、物が光る事もあるわ。覚えとくといいわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「わかった。ありがとう。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        resetInput();
        DisplayCharacterName(user);
        DisplayText("「それじゃあ、cyber_avenueってところに進んでみましょ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd cyber_avenue")
            {
                break;
            }
            else
            {
                DisplayText("「違うわ、マップを移動したいときは cd (行きたい場所) よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                resetInput();
                DisplayText("「今回はcyber_avenueの方に進むのよ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayDirectory("cyber_avenue");
        DisplayCharacterImage(null);
        DisplayCharacterName(chiki);
        DisplayText("「待って、誰かいる。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterImage(mineImg);
        DisplayCharacterName(user);
        DisplayText("街の通りを進んでいくと、不気味な仮面をつけ、黒いフードを被った少女が道の真ん中に佇んでいるのが見えた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("少女は私たちがここに来るのをずっと待っていたかのように、私たちの姿を目に捉えると一目散にこちらに駆け寄ってきた。そして──");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「ねーねー、わたしのかんがえたもんだいといて！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("といきなり言ってきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「は…？ちょっと、どういうつもり？あなたは何者？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「わたしがだれかなんてどーでもいいことだよ。それよりもはやくもんだいにこたえてよ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("少女はこちらの言葉に耳を傾ける様子は一切なく、自分の出した問題を早く解いてくれと言い続けている。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("困った。とりあえず問題に答えたほうがいいのだろうか。でも答えたところでこちらにメリットなんてないからな……。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("なんて思ってると、目の前の少女が一枚の紙切れを見せながらこう続けた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「もしせいかいできたらこれあげる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「は？紙切れなんていらな──」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("と言いかけたところで、私はその紙切れが光っていることに気づいた。光っている、ということはもしかして手がかりなのだろうか？");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("それならば話は別だ。少女がなぜ紙切れを拾っているのかはわからない。だが、この問題に正解することで手がかりが手に入るのなら問題に挑んだ方がいいだろう、と私は思った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「──いや、やっぱりやるよ、どんな問題でも来い！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("と意気込んだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("──のだが、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「さっぱりわからないぃぃ………」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        jump:
        DisplayText("10分ほど考えてもさっぱりわからない。なんだトロイの木馬とは。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("そもそもマルウェアという単語自体初耳なのに、その中にあるトロイの木馬が云々と言われてもわからないに決まってる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("あまつさえその由来となった事例を聞かれても知るわけがないだろ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「まだー？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「……無理、お手上げです。もう少し簡単な問題とかないの……？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("とダメ元で聞いてみると、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「しかたないなぁ、いいよ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("なんと問題を優しくしてくれるようだ。自分より小さい子供に負けた気がしてならないが、元の世界に戻るためだ。恥は捨てよう。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「もんだい！ふだんWebサイトにアクセスするときにつかうURLのさいしょにあるもじれつはなーんだ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("お、これなら学校で習ったから解けそうだぞ。と考えていると、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「とくべつにせんたくしもいうね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("と言ってきた。急に優しくなって驚いたが、子供は自分の作った問題を他人に解いてほしいのかな、と自分で納得して出てきた問題に取り組む事にした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            resetInput();
            DisplayCharacterName("Q1.");
            DisplayText("普段Webサイトにアクセスする際に用いているURLの最初の文字列は何でしょう\n1.http  2.https  3.hhtp  4.ttps");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "2")
            {
                break;
            }
            else
            {
                resetInput();
                DisplayCharacterName(unknownName);
                DisplayText("「不正解！Enterで戻ってね！」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        DisplayCharacterName("A1.");
        DisplayText("正解：https\nコメント：httpは通信が暗号化されておらず安全ではない");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        inputField.enabled = false;
        DisplayCharacterName(unknownName);
        DisplayText("「せいかい！やくそくどおりこれあげる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("正解すると、少女は約束通り、一枚の紙切れを渡してくれた。そこには「192」と書かれている。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("また、さっきまで紙切れが光っていて気づかなかったが、紙の裏面には小さく「1番目」、と書かれていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("確か、pocketbook.txtには「4つの数字を並べ替える」と書いてあったので「1番目」とは並べ替えた時の順番の事を示しているのだろうか。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「何はともあれ、これで一つめの手がかりが手に入ったわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「そうだね、よかったよ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("***");
        DisplayText("続きは製品版で！");
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
        if (path == null)
        {
            image.sprite = null;
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Pic/" + path);
        }
    }

    /// <summary>
    /// ディレクトリを表示する
    /// </summary>
    /// <param name="currentLocation">現在のディレクトリ</param>
    public void DisplayDirectory(string currentLocation)
    {
        string text = "";
        switch (stage) {
            case 0:
                text = "World\n" +
                    "\t├ Sea\n" +
                    "\t└ Mountain\n";
                break;
            case 1:
                text = "";
                break;
            case 2:
                text = "World\n" +
                    "\t└ cyber_entrance\n" +
                    "\t\t└ cyber_central\n";
                break;
            case 3:
                text = "└ cyber_central\n";
                break;
            case 4:
                text = "└ cyber_central\n" +
                    "\t├ cyber_avenue\n" +
                    "\t├ cyber_alley\n" +
                    "\t└ cyber_downtown\n";
                break;
            default:
                text = "";
                break;
        }
        if (stage == 0)
        {
            text = "World\n" +
                    "\t├ Sea\n" +
                    "\t└ Mountain\n";
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
        if (path == null)
        {
            characterImage.sprite = null;
        }
        else
        {
            characterImage.sprite = Resources.Load<Sprite>("Pic/" + path);
        }
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
