using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    public Text timerText;
    public Image objectImage;
    //public AudioSource bgmSource;

    // 宣言
    public int stage; // 進行度の管理(ディレクトリの表示状況のコントロール等)
    public string input; // 入力された文字列を格納
    public static bool beingMeasured; // タイマー計測中の変数
    public float waitTime; // テキストの最低表示時間
    public string user;
    public string chiki; // チキ(チュートリアルのキャラクター)の名前
    public string chikiImg; // チキの画像の名前
    public string unknownName; // 不明なキャラクターの名前
    public string sys; // システムメッセージの名前
    public string pb; // 手帳のファイル名
    public string mine; // エネ(敵キャラクター)の名前
    public string mineImg; // エネの画像の名前
    public float limit; // タイムリミット
    public bool isClearAlley; // Alleyのクリア状況
    public bool isClearDownTown; // Downtownのクリア状況
    public int numAlley; // Alleyの問題番号
    public int numDownTown; // Downtownの問題番号
    public Regex rx; // 正規表現
    public string cmd; // sshコマンド
    public bool isClearSSH; // sshのクリア状況



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
        timerText = timerText.GetComponent<Text>();
        objectImage = objectImage.GetComponent<Image>();

        // パラメータ初期化
        stage = 0; // 進行度の管理(ディレクトリの表示状況のコントロール等)
        input = "Text"; // 入力された文字列を格納
        waitTime = 0.2f; // テキストの最低表示時間
        user = "俺くん"; // プレイヤーの名前
        chiki = "チキ"; // チキ(チュートリアルのキャラクター)の名前
        chikiImg = "chiki"; // チキの画像の名前
        unknownName = "???"; // 不明なキャラクターの名前
        sys = "***"; // システムメッセージの名前
        pb = "pocketbook.txt"; // 手帳のファイル名
        mine = "ミーネ"; // エネ(敵キャラクター)の名前
        mineImg = "mine"; // エネの画像の名前
        limit = 10.0f; // タイムリミット
        beingMeasured = false; // タイマー計測中の変数
        isClearAlley = false; // Alleyのクリア状況
        isClearDownTown = false; // Downtownのクリア状況
        numAlley = 2; // Alleyの問題番号
        numDownTown = 3; // Downtownの問題番号
        isClearSSH = true; // sshのクリア状況


        timerText.text = "";
        ToggleObjectOpacity(true);
        rx = new Regex("{ssh -i secretkey.pem [A-Za-z0-9]+@192.168.11.11}", RegexOptions.Compiled);

        StartCoroutine(Chapter0());
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        if (beingMeasured)
        {
            limit -= Time.deltaTime;
            timerText.text = limit.ToString("0.00");
        } else {
            timerText.text = "";
            return;
        }

        if (limit <= 0.0f)
        {
            limit = 0.0f;
            beingMeasured = false;
            timerText.text = limit.ToString("0.00");
            timerText.text = "";
            StartCoroutine(Chapter5_failuer());
        }

    }

    IEnumerator Chapter0()
    {
        ResetInput();
        DisplayImage("title");
        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        directoryText.text = "";
        DisplayCharacterName(sys);
        DisplayText("2022年度ICTプロジェクト\n<color=yellow>「ゲーミフィケーションで学ぶLinuxコマンド教材」</color>作品\n　　　　　<color=orange>あなたの名前を入力してEnterで開始</color>");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        inputField.enabled = false;
        user = inputField.text;
        StartCoroutine(Chapter1());
    }

    IEnumerator Chapter1()
    {
        stage = 1;
        inputField.enabled = false;
        DisplayImage("start");
        DisplayDirectory(null);
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

        ToggleImageOpacity(false);
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

        DisplayText("退屈だったからほぼ寝てたけど、ふと目を覚ました時に先生が「<color=orange>ssh</color>は遠く離れたコンピュータと通信するための──」って言ってたのを家で思い出して自分でもやってみたんだ。");
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

        ResetInput();
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
                ResetInput();
                DisplayText("「違うわ、<color=orange>cd</color> cyber_entrance よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayImage("cyber_entrance");
        DisplayText("「そう、その調子よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
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
                ResetInput();
                DisplayText("「違うわ、マップの移動をするときは<color=orange>cd</color>を使うのよ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayImage("cyber_central");
        DisplayText("「いい感じね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
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
                ResetInput();
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

        ResetInput();
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
                ResetInput();
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

        DisplayText("①ディレクトリを移動して4つの数字を集める。\n②脱出するための秘密鍵を入手する。\n③『ゲート』に行き、脱出のためのコマンドを叫ぶ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("コマンド → <color=orange>ssh</color> -i (②の名前) (自身の名前)@(①で集めた4つの数字)");
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
        DisplayText("「通信が暗号化されてなかったら部外者に何をやろうとしてるか筒抜けになるでしょ。それを防止するために暗号化というのは行われるのよ。」");
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

        DisplayText($"こうしてこの変な生物、{chiki}との旅が始まった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter3());
    }

    IEnumerator Chapter3() {
        DisplayCharacterName(user);
        DisplayText("「話を戻すけど、さっき言ってた『視える』ってやつ、どうやったらできるの？さっきはよくわからないままだったから。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
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
                ResetInput();
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

        ResetInput();
        DisplayCharacterName(chiki);
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

                ResetInput();
                DisplayText("「今回はcyber_avenueの方に進むのよ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayImage("cyber_avenue");
        DisplayDirectory("cyber_avenue");
        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayCharacterName(user);
        DisplayText("「ここがcyber_avenueか…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「待って、誰かいる。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
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

        DisplayText("それならば話は別だ。少女がなぜ紙切れを拾っているのかはわからない。だが、この問題に正解することで手がかりが手に入るのなら問題に挑んだ方がいいだろうと私は思った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「──いや、やっぱりやるよ、どんな問題でも来い！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("と意気込んだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayImage("black");
        DisplayText("──のだが、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayImage("cyber_avenue");
        DisplayText("「さっぱりわからないぃぃ………」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

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
            ResetInput();
            DisplayCharacterName("Q1.");
            DisplayText("普段Webサイトにアクセスする際に用いているURLの最初の文字列は何でしょう(半角数字で解答)\n1.http  2.https  3.hhtp  4.ttps");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "2")
            {
                break;
            }
            else
            {
                ResetInput();
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

        DisplayCharacterName(mine);
        DisplayText($"「ありがとー！また{mine}とあそんでね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayCharacterName(user);
        DisplayText("「うん、また──え……？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"あの子の名前{mine}っていうんだ、と思いながら彼女の別れのあいさつに返事しようと彼女の方を向くと、彼女の姿はどこにもなかった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「消えた……？いったいどこに行ったのかしら……？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「どこに行ったんだろう……でもひとまず紙切れは見つかったね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(chiki);
        DisplayText("「そうね、なくさないようにbagにしまっておきましょうか。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「いいけど、バッグなんて持ってないよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ないなら作ればいいじゃない！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「この世界はそんなこともできるのか…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ディレクトリ…フォルダを作るのと同じよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("「<color=orange>mkdir</color> /bagって打ってみて」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "mkdir /bag" || input == "mkdir /bag/")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayText("「違うわ、<color=orange>mkdir</color> /bag よ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName(user);
        DisplayText("「…これでいいの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(chiki);
        DisplayText("「出来ているわよ。そして<color=orange>mv</color> paper_1.txt /bagしてみて」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "mv paper_1.txt /bag" || input == "mv paper_1.txt /bag/")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayText("「違うわ、<color=orange>mv</color> paper_1.txt /bag よ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName(chiki);
        DisplayText("「これでbagにしまえたわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「この後は…とりあえずcyber_centralに戻る？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そうね、戻りましょうか。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("「1つ前にいたディレクトリに戻るには<color=orange>cd</color> .. が便利よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd .." || input == "cd ../")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayDirectory("cyber_central");
        DisplayCharacterName(user);
        DisplayText("謎の少女は不思議だが、それよりも今は優先すべき事がある。来た道を戻ることにした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter3_cyber_central());
    }

    IEnumerator Chapter3_cyber_central()
    {
        inputField.enabled = false;
        ResetInput();
        DisplayDirectory("cyber_central");
        DisplayImage("cyber_central");
        DisplayCharacterName(sys);

        string area = "cyber_alley、cyber_downtown";
        if (!isClearAlley && !isClearDownTown)
        {
            area = "cyber_alley、cyber_downtown";
        }
        else if (isClearAlley && !isClearDownTown)
        {
            area = "cyber_downtown";
        }
        else if (!isClearAlley && isClearDownTown)
        {
            area = "cyber_alley";
        }
        else
        {
            StartCoroutine(Chapter3_final());
        }

        if (!isClearAlley || !isClearDownTown)
        {
            DisplayText($"行き先をコマンドで指定してください。\n行動可能エリア→{area}");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }


        while (!isClearAlley || !isClearDownTown)
        {
            input = inputField.text;
            if (input == "cd cyber_alley" && !isClearAlley)
            {
                StartCoroutine(Chapter3_cyber_alley());
                break;
            }
            else if (input == "cd cyber_downtown" && !isClearDownTown)
            {
                StartCoroutine(Chapter3_cyber_downtown());
                break;
            }
            else
            {
                ResetInput();
                DisplayText("「cd (行きたい場所) で入力してください」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }
    }

    IEnumerator Chapter3_cyber_alley()
    {
        inputField.enabled = false;
        DisplayDirectory("cyber_alley");
        DisplayImage("cyber_alley");
        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayCharacterName(user);
        DisplayText("cyber_entranceで光っていた道に従って細い路地裏を進んでいくと、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(mineImg);
        DisplayCharacterName(mine);
        DisplayText("「あ！またあったね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"という声とともに{mine}がこちらに向かって駆け寄ってきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「{mine}！また会ったね。どうしてこんなところに？子供がこんなところにいたら危ないよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「ちょっとさがしものしてたの。みつからなかったけどね……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「手伝おうか？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「ううん、だいじょうぶ。……あ！でもかわりにこれみつけたよ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"と言いながら{mine}が見せてきたものは、光っている紙だった。これはまさか──");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「それってもしかして……紙に数字書いてない？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("と聞くと、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「うん！かかれてあるよ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("やっぱりそうだ。あれにはきっとIPアドレスの一部が書かれている。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「それ貰ってもいいかな……？さっきも言ったと思うけど、それがないと私たちは帰れないんだ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「えー…どうしようかなぁ。そうだ！またわたしのだすもんだいにせいかいできたらあげる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("また問題か。正直早く帰りたいという気持ちが強いので面倒くさいが、それを正直に言って拗ねられても困るのでここはこの子の出す問題に付き合うとしよう。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「……いいよ。どんな問題かな？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「ありがとう！それじゃあもんだいをだすね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そう言って{mine}は私に問題を出してきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            ResetInput();
            if (isClearDownTown) numAlley = 3;
            DisplayCharacterName($"Q{numAlley}.");
            DisplayText("インターネットで通信を行う際に使われる、機器同士が通信を送受信する場所として仮想的に定められている物は何でしょう　1.サーバ  2.URL  3.通信プロトコル  4.ポート");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "4")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(mine);
                DisplayText("「不正解！Enterで戻ってね！」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName($"A.{numAlley}.");
        DisplayText("正解：ポート　コメント：サーバはブラウザ等に情報を提供してくれるものである。通信プロトコルは、お互いにどんな通信をするのかを予め定めた決め事である。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("さらに、その決め事ごとにどのポートを使うのかを取り決めている。例えば、httpsであれば443番ポート、httpであれば80番ポート、sshであれば23番ポートが用いられる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        inputField.enabled = false;
        DisplayCharacterName(mine);
        DisplayText("「せいかい！これあげる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"何とか正解した私は、{mine}から紙切れを受け取った。そこには「168」と書かれており、裏面には「2番目」、と書かれていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「それじゃあまたね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「うん、またね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayText($"そう言って{mine}は私達の横を通り過ぎ、cyber_centralに戻っていった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("paper_2.txt...バッグにしまうのを忘れないようにしなきゃな。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "mv paper_2.txt /bag" || input == "mv paper_2.txt /bag/")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「bagにしまうのは<color=orange>mv</color> paper_2.txt /bag よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName(user);
        DisplayText("「私達も戻ろっか。欲しいものも手に入れることができたし。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そうね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(user);
        DisplayText("私達もcyber_centralに戻ることにした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd .." || input == "cd ../")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }
        inputField.enabled = false;
        isClearAlley = true;
        DisplayCharacterName(user);
        DisplayText("今回の問題もそれなりに知識が必要だ。あの少女は何者なのだろうと考えながら、cyber_alleyを後にした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter3_cyber_central());
    }

    IEnumerator Chapter3_cyber_downtown()
    {
        inputField.enabled = false;
        DisplayDirectory("cyber_downtown");
        DisplayImage("cyber_downtown");
        DisplayCharacterName(user);
        DisplayText("cyber_entranceで光っていた方向を辿ると下町にやって来た。だが、下町のどこを探しても光っている紙は見当たらなかった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「どこかで手がかりを見逃したのかしら……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「来た道を戻ってみる？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「うーん……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("と、私たちがこれからどうするかべきかを考えていると、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(mineImg);
        DisplayCharacterName(mine);
        DisplayText("「あれ？どうしてここにいるの？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"と{mine}に後ろから声をかけられた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「{mine}？こんなところでまた会うなんて思ってなかったよ。こんにちは。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「こんにちは！あのね、さっきこんなものをひろったんだけど、もしかしてふたりがさがしてるものってこれかな？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"{mine}は光る紙を見せながら私たちにそう言った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「それだ……さっきはどっかで見逃してたのかな？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「多分そうね……迂闊だったわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「{mine}、その紙切れ、よかったら私たちが貰ってもいいかな？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「いいよ！そのかわりまたわたしのつくったもんだいにこたえたほしいな！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「いいよ。どんな問題だい？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「ありがとう！それじゃあもんだい！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そして、{mine}は私に問題を出してきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            ResetInput();
            if (!isClearDownTown) numDownTown = 2;
            DisplayCharacterName($"Q{numDownTown}.");
            DisplayText("こんにちの社会で、暗号化を行う際一般的に使われている方式はどれ？(半角数字で解答)\n1.DSA 2.RSA 3.ECDSA 4.edDSA");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "2")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「不正解！Enterで戻ってね！」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName($"A{numDownTown}.");
        DisplayText("正解：RSA　コメント：細田守の長編アニメ映画「サマーウォーズ」で主人公が解いていた暗号もRSA暗号。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「せいかい！これあげる！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"何とか正解した私は、{mine}から紙切れを受け取った。そこには「11.11」と書かれており、裏面には「3、4番目」、と書かれていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「二人ともまたね！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(mineImg);
        DisplayCharacterName(user);
        DisplayText($"そう言って、{mine}は街の人ごみに紛れて消えていった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(chiki);
        DisplayText("「何なのかしらね…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(user);
        DisplayText("ともかく、もらったpaper_3-4.txtはバッグにしまっておこう。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "mv paper_3-4.txt /bag" || input == "mv paper_3-4.txt /bag/")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「バッグにしまうのなら、<color=orange>mv</color> paper_3-4.txt /bag ね。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayText("「私達も戻ろう。まだやるべきこともたくさんあるし。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ええ。戻りましょうか。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(user);
        DisplayText($"{chiki}と頷きあい、私達はcyber_centralに戻ることにした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cd .." || input == "cd ../")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }
        inputField.enabled = false;
        isClearDownTown = true;
        DisplayCharacterName(user);
        DisplayText("普通の旅行なら観光していくのになという気持ちもあったが、cyber_downtownを後にした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter3_cyber_central());
    }

    IEnumerator Chapter3_final()
    {
        inputField.enabled = false;
        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayDirectory("cyber_central");
        DisplayCharacterName(user);
        DisplayText("cyber_centralに戻ると、そこには人が誰もいなかった。さっきまで人がいたのにどういう事だろうか。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「ねえ{chiki}、おかしくない？さっきまで人がたくさんいたはずだよね？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ええ、おかしいわね。cyber_centralはこの街の中でも最も人通りが多いところ。人がいないなんて異常だわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(unknownName);
        DisplayText("「おかしくなんてないよ。だってわたしがやったことだもん。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("2人");
        DisplayText("「「！？」」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterImage(mineImg);
        DisplayCharacterName(user);
        DisplayText($"突然背後から声がしたので{chiki}とともに後ろを振り返ってみると、そこには{mine}がいた。しかし、なんだか様子がおかしい。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「{mine}……？また会ったね、どうしたの？私がやったことって……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「また会ったね。言った通り、これは私がやったこと。二人をこの世界から逃がさないためにね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「え……？逃がさないって、なんでそんなこと──」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「理由は簡単。『迷い人』って不思議な力を持っててね。普通は来ることが難しいこの世界と向こうの世界を簡単につなぐことができるの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「あなた、向こうからこっちに来たんだよね？それならその逆も出来そうっていうのはなんとなく想像できるんじゃない？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「まぁ……私が元の世界に戻ろうとするためにはそれをする必要があるんだし……」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「でしょ？その力を利用して、私は……いえ、『私達は』、あなた方の世界を崩壊させようと思ってるの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「崩壊！？どうして！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterImage("mineFace");
        DisplayCharacterName(mine);
        DisplayText("「そんなの、私がトロイの木馬だからに決まってるからでしょう？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「トロイの木馬？どこかで聞いたことあるような……あ！{mine}と最初に会った時に{mine}が言ってたやつか！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「そう。トロイの木馬っていうのは普通のファイルの裏に隠れながら個人情報を盗むことができるソフトウェアなの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「一見すると普通のファイルとなんら変わりないから引っかけやすいのだけどね、難点として普通は私達単体ではファイルを複製することはできないの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「でも、Linuxコマンドを使えるあなたとそこのちんちくりんの力を利用すれば、簡単にファイルを複製し、全世界にばらまく事ができるってこと。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「そして世界中の個人情報を盗み、ばらまき、混乱に陥れようってわけ。わかった？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「仕組みや目的はわかったけど、だからと言って協力する気は全くないよ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「そうでしょうね。でもそんなことはどうだっていいの。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("<color=orange>mv</color> you chiki .reverse_central/.");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stage = 5;
        ToggleImageOpacity(true);
        DisplayImage("black");
        DisplayDirectory(null);
        DisplayCharacterName(user);
        DisplayText("「え──」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"{mine}に何か言い返そうと思ったが、意味不明な言葉が{mine}から出てきた途端、私の意識は途切れた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(mine);
        DisplayText("「──さようなら。隠しディレクトリの中で私達のために永遠に利用されてちょうだい。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter4());
    }

    IEnumerator Chapter4()
    {
        DisplayDirectory(".reverse_central");
        DisplayImage("reverse_central");
        DisplayCharacterName(user);
        DisplayText("「え...ここは...？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("周辺を見渡してみると、元居た場所とは全く違う、黒と緑を基調とした場所だった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「なんというか、不気味で寂しい場所だな...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(chiki);
        DisplayText("「どうやら、ここは隠しフォルダの中のようね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「かくし...？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そう。隠しフォルダ。今までいた場所とは違って、何らかの理由で隠されている場所よ。ありていに言ってしまえば裏世界のようなものかしら。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そう語る{chiki}も、顔色はあまり良くなかった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「なるほど...でも、どうしてそんなところに...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「あの子がやったのね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("間違いない。あの子が何かつぶやいた途端に世界の様相が一変したのだから。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「それはそうだけど！じゃあどうやって帰ればいいんだ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"一縷の望みを賭け、{chiki}にそう問うてみる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「わからないわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("帰ってきたのは無情にも否定の言葉だった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「そんな！じゃあどうしたらいいんだ...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("半ば自棄になりながら、私は吐息を漏らした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「まずは脱出の手掛かりを探すしかないでしょうね...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「ああ...そうだよな...うん...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayText("そう力なく答えた後、目の前が真っ暗になったように感じながらも私はふらふらと歩き出した。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「手掛かりって言ってもそんな物あるとは思えないし...ん？これは...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("ふと立ち止まり前を見ると、見慣れた文字が目に飛び込んできた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「これは...日本語？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("ゆっくりと目を動かす。そこには");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("謎のメッセージ");
        DisplayText("『この場......ゲ...を......カ.........す』");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「文字がかすれていてうまく読み取れないな...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("目を凝らして、ゆっくりと読み返してみる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「この場...に...ゲート...カギ...残す..？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「この場所にゲートを開くカギ...まさか！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("もう一度、今度はその概形がはっきり見えた文字に目を通す。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("謎のメッセージ");
        DisplayText("『この場所にゲートを開くカギを残す』");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("最初に文字に対し抱いた疑問は確信に変わった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「間違いない。この場所には先人が残してくれた元の世界へ戻る手がかりが残されている。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("自分の中で目の前の事実を受け入れるべく、私がそう呟くと、");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(chiki);
        DisplayText("「この隠しフォルダの中にも入れるなんて大したものだわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("チキが笑いながら冗談っぽく言った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「感心してる場合じゃないよ！ここにも脱出のヒントがあるって分かったんだ、探すしかないよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"私は力強く{chiki}に告げた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「あら、さっきと比べてずいぶん元気になったわね。その調子で頑張って探しましょう。私も手伝うわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"見つけた希望を逃すまいと、私は{chiki}と一緒に足早に歩き始めた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stage = 7;
        DisplayDirectory(".reverse_central");
        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayText($"しばらく歩いていると、{chiki}が突然立ち止まった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chikiImg);
        DisplayCharacterName(chiki);
        DisplayText("「ここらへんなら『視える』かもしれないわ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「えーっと、道や物が光って見えるってやつ？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(chiki);
        DisplayText("そうよ、やってみて");
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
                ResetInput();
                DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        inputField.enabled = false;
        stage = 6;
        DisplayDirectory(".reverse_central");
        DisplayCharacterName(chiki);
        DisplayText("「探索の基本は移動の<color=orange>cd</color>, 存在する物を確認する<color=orange>ls</color>, 中身を見る<color=orange>cat</color>よ。覚えておくといいわ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            ResetInput();
            DisplayDirectory(".reverse_central");
            ToggleImageOpacity(true);
            DisplayCharacterImage(null);
            DisplayCharacterName(sys);
            DisplayText("探索先を指定してください。\nred_box, green_box, blue_box, yellow_box");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            Debug.Log(input);
            if (input == "cd red_box" || input == "cd red_box/")
            {
                DisplayDirectory("red_box");
                DisplayCharacterName(chiki);
                DisplayText("「ここは、red_boxね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>ls</color>してみて」");
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
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("red_box");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color>してみて」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>cat</color> paper.txtよ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("��.��!zY");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「文字化けしているわね…」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("何も得られなかった…");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「戻りましょうか」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
            }
            else if (input == "cd green_box" || input == "cd green_box/")
            {
                DisplayDirectory("green_box");
                DisplayCharacterName(chiki);
                DisplayText("「ここは、green_boxね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>ls</color>してみて」");
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
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                        continue;
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("green_box");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color>してみて」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>cat</color> paper.txtよ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("縺ｯ縺ゑｽ樒ｷ?繧∝?繧?騾ｱ髢灘ｻｶ縺ｳ縺ｭ縺医°縺ｪ縺");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「文字化けしているわね…」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("何も得られなかった…");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「戻りましょうか」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                        continue;
                    }
                }

                inputField.enabled = false;
                continue;
            }
            else if(input == "cd blue_box" || input == "cd blue_box/")
            {
                ResetInput();
                DisplayDirectory("blue_box");
                DisplayCharacterName(chiki);
                DisplayText("「ここは、blue_boxね」");
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
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                        continue;
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("blue_box");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    ResetInput();
                    inputField.enabled = true;

                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayText("「違うわ、<color=orange>cat</color> paper.txtよ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                        continue;
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("hint:<color=orange>ssh</color>の正しき名を示せ");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「なるほど…？」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("ヒントを得られた！");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「戻りましょうか」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「違うわ、<color=orange>cd</color> .. よ。");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
            }
            else if (input == "cd yellow_box" || input == "cd yellow_box/")
            {
                DisplayCharacterName(chiki);
                DisplayText("「入るのにはパスワードがいるみたい」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(sys);
                DisplayText("Password:");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                input = inputField.text;
                Debug.Log(input);
                if (input == "secureshell")
                {
                    break;
                }
                else
                {
                    DisplayCharacterName(sys);
                    DisplayText("Permission denied, please try again.");
                    yield return new WaitForSeconds(waitTime);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                }
            }
            else
            {
                inputField.enabled = false;
                DisplayCharacterName(chiki);
                DisplayText("「違うわ。<color=orange>cd</color> (行きたい場所)よ」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        ResetInput();
        DisplayDirectory("yellow_box");
        DisplayCharacterName(chiki);
        DisplayText("「<color=orange>ls</color>してみましょう」");
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
                ResetInput();
                DisplayText("「違うわ、<color=orange>ls</color>よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                continue;
            }
        }

        inputField.enabled = false;
        DisplayCharacterName("yellow_box");
        DisplayText("secretkey.pem");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「見つけたぞ！！これが先人の残した手がかりだ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私は声高に叫ぶと、{chiki}がこちらへ近づいてきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chiki);
        DisplayCharacterName(chiki);
        DisplayText("「ひとまず、bagにしまっておきましょうか。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("もし『迷い人』がまた現れたときのために、<color=orange>cp</color>でしまいましょう。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cp secretkey.pem /bag" || input == "cp secretkey.pem /bag/")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「違うわ、<color=orange>cp</color> secretkey.pem /bag/よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        inputField.enabled = false;
        stage = 7;
        DisplayDirectory(".reverse_central");
        DisplayCharacterName(chiki);
        DisplayText("「やったわね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そういう{chiki}の顔は、あまり明るくはなかった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「どうしたの{chiki}！手がかりは見つかったってのに暗い顔してさ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私は気分よく{chiki}に尋ねた。すると${chiki}は");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「そうね。でもまだ解決していない課題があることを忘れていないかしら？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("と逆に質問してきた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「え？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私はあっけにとられた顔をして固まった。{chiki}がその回答を告げた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ここから出る方法よ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「ああ...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("先ほどまでの高揚感は消え、一気に現実に引き戻された。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「依然としてここから脱出する方法は見つからないままね。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("いくら手がかりを見つけても、ここから出ないことには始まらないことを忘れていた。考えないでいた、という方が正しいかもしれない。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"しばらく固まってから、ひきつった顔で{chiki}に声をかける。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「なあ、本当は何か知っているとか...ない？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"半ば諦めながらも、{chiki}に聞いてみる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「いいえ。申し訳ないけど、全く見当がつかないわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("しばらくの間、沈黙が続いた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("ぐるぐると頭の中でネガティブな考えが回る。怖い。帰れなかったらどうしよう。嫌だ。嫌だ。嫌だ...");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"10分ほど経っただろうか。{chiki}が声を上げた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ここで立ち止まってても何も起きないわよ。もう少し探索してみない？もしかしたら、まだ見つかっていない手がかりがあるかもしれないわよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「そうだけど...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("沈み切った声で、私は返事をした。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「もう悩む時間は終わりよ。このあたりを探索しつくしたわけじゃないんだし、次はあっちの方を調べてみましょうよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そう言うと、{chiki}は私の手を引いて進み始めた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「しゃっきりしなさい！できることを全部してからでも、絶望するのは遅くない。そうでしょう？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「あと一歩で帰れるでしょう？凹んでる場合じゃないと思わない？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"手を引きながら、{chiki}は何度も励ましの言葉を投げかけてくれる。しばらくすると");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「そうだよな。ありがとう{chiki}。ここでくよくよしていても仕方ない。もうひと踏ん張りだ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"{chiki}の鼓舞を受け気力を取り戻した私は、まだ探索していない場所へと歩みを進めた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayCharacterName(chiki);
        DisplayText("「さ、ここでも<color=orange>ls</color>してみましょ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("手がかりは見つからないままかなりの時間が経過した。お互いの気力も尽きてきていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("しかし、このままでは終われない。ダメ元でも<color=orange>ls</color>してみるか。");
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
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「違うわ。<color=orange>ls</color> よ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        inputField.enabled = false;
        stage = 8;
        DisplayDirectory(".reverse_central");
        DisplayCharacterName(user);
        DisplayText("「これは…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("絶望的な状況に一筋の光が見えた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("さあ、さっきと同じ要領で探索よ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            ResetInput();
            DisplayDirectory(".reverse_central");
            ToggleImageOpacity(true);
            DisplayCharacterImage(null);
            DisplayCharacterName(sys);
            DisplayText("探索先を指定してください。\ncontainer, safe, pot, bottle");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            input = inputField.text;
            if (input == "cd container" || input == "cd container/")
            {
                DisplayDirectory("container");
                DisplayCharacterName(chiki);
                DisplayText("「ここはcontainerね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>ls</color>してみましょ。」");
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
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>ls</color> してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                DisplayCharacterName("container");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("hint:長き時間を共にした相棒の名を示せ");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("「ふむ…」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「…」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayText("「戻りましょうか。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cd</color> .. してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
            }
            else if (input == "cd safe" || input == "cd safe/")
            {
                DisplayCharacterName(chiki);
                DisplayText("「どうやらパスワードが必要みたいね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(sys);
                DisplayText("Password: ");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                input = inputField.text;
                if (input == "チキ")
                {
                    break;
                }
                else
                {
                    ResetInput();
                    DisplayCharacterName(sys);
                    DisplayText("Permission denied, try again.");
                    yield return new WaitForSeconds(waitTime);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    continue;
                }
            }
            else if (input == "cd pot" || input == "cd pot/")
            {
                DisplayDirectory("pot");
                DisplayCharacterName(chiki);
                DisplayText("「ここはpotね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>ls</color>してみましょ。」");
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
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>ls</color> してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                DisplayCharacterName("pot");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("ãã");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「文字化けしているわね。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("何も得られなかった…");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterImage(chiki);
                DisplayText("「戻りましょうか。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cd</color> .. で戻りましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
            }
            else if (input == "cd bottle" || input == "cd bottle/")
            {
                DisplayDirectory("bottle");
                DisplayCharacterName(chiki);
                DisplayText("「ここはbottleね」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>ls</color>してみましょ。」");
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
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>ls</color> してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                DisplayCharacterName("bottle");
                DisplayText("paper.txt");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cat paper.txt")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cat</color> paper.txt してみましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }

                inputField.enabled = false;
                DisplayCharacterName("paper.txt");
                DisplayText("쳤��������?������");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(chiki);
                DisplayText("「文字化けしているわね。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                DisplayCharacterName(user);
                DisplayText("何も得られなかった…");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayCharacterImage(chiki);
                DisplayText("「戻りましょうか。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                while (true)
                {
                    input = inputField.text;
                    if (input == "cd .." || input == "cd ../")
                    {
                        break;
                    }
                    else
                    {
                        ResetInput();
                        DisplayCharacterName(chiki);
                        DisplayText("「<color=orange>cd</color> .. で戻りましょ。」");
                        yield return new WaitForSeconds(waitTime);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
                    }
                }
            }
            else
            {
                inputField.enabled = false;
                DisplayCharacterName(chiki);
                DisplayText("探索は<color=orange>cd</color>, <color=orange>ls</color>, <color=orange>cat</color>で行うのよ。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        inputField.enabled = false;
        DisplayDirectory("safe");

        DisplayCharacterName(user);
        DisplayText("「入れた…？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(chiki);
        DisplayText("「まずは<color=orange>ls</color>よ」");
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
                ResetInput();
                DisplayText("「<color=orange>ls</color> するのよ。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        DisplayCharacterName("safe");
        DisplayText("paper.txt");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayCharacterName(chiki);
        DisplayText("「<color=orange>cat</color> paper.txt で見てみましょう。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            if (input == "cat paper.txt")
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(chiki);
                DisplayText("「<color=orange>cat</color> paper.txt で見てみましょう。」");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        inputField.enabled = false;
        DisplayCharacterName("paper.txt");
        DisplayText("/world/cyber_entrance/cyber_central");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「頑張ってそれっぽいものは見つけたけれども、結局意味の分からない文字列じゃないか。それにしても、パスワードが{chiki}だなんて不思議なこともあるもんだな。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"解読した暗号に感じた疑問を飲み込みつつ、{chiki}に声をかける。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chiki);
        DisplayText("「なあ、この文字列に何か心当たりがあるか？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"{chiki}は謎の文字列を一瞥して");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「いいえ、無い...あら、これは...もしかして...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("心当たりはない、そう言わんとした口が止まる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「ん？なにか知っているのか！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私は思わず声を荒らげた。{chiki}は落ち着いて返答する。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「これは他の場所へのパスよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("聞き覚えのない単語が聞こえた。パス？");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「バスっていうのは？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私の質問に、{chiki}は簡潔に答えた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「簡単に言えば目的地点といったところかしら。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「おお！して、その目的地点っていうのは？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("きっと帰れるという期待を漏らしながら、場所について聞いてみる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「これが、少し曖昧なのよ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「曖昧？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「最初に話した<color=orange>ssh</color>の話を覚えているかしら。あれと同じで、曖昧な地点を指定して移動を行うと、自分の居場所が分からなくなるし最悪四肢が分離するわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「そんな...。せっかく戻るヒントが得られたのに...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私は大きく落胆した。しかし、{chiki}の一言でその落胆は一気に覆った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ただ、もしかしたら何とかできるかもしれないわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「本当に！？じゃあ...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ただし、さっきも言ったリスクを知っておいてほしいわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("先ほど話してくれたリスクが頭をよぎる。いくら帰れる可能性があるとしても、バラバラになってしまう可能性があるというのはかなり怖い。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"そんなことを考えていると、{chiki}が言葉を続けた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「それでも、信じて任せてくれるなら全身全霊をかけて移動に臨むわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"{chiki}が真っ直ぐにこちらを見つめている。その覚悟に満ちた目に、私も腹が決まった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"「何を今更。信じてるよ。{chiki}のこと。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"精一杯の笑顔を{chiki}に向ける。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ありがとう。それじゃあ、私につかまって。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("チキも微笑みを浮かべた。すぐに、真剣な表情に戻る。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「ああ。よろしくな。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"私は、{chiki}と離れないようにしっかりと手を握った。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「さあ、跳ぶわよ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayImage("black");
        DisplayCharacterName(user);
        DisplayText($"そう告げた{chiki}に身を任せていると、何度か強い衝撃を感じた。目を閉じてぐっとこらえながらしばらく経つと、{chiki}の声が聞こえた");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ふう...なんとか、戻ってこれたわ。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayImage("sky");
        stage = 9;
        DisplayDirectory(null);
        DisplayCharacterName(user);
        DisplayText("目を開けると、見慣れた場所が眼前に広がっていた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter5());
    }

    IEnumerator Chapter5()
    {
        DisplayImage("cyber_central");
        DisplayDirectory("cyber_central");
        DisplayCharacterName(user);
        DisplayText("「やっとこっちの世界に戻ってきた…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「気をつけて！穴が空いているわ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「え…？穴なんて空いているわk」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「うわあああああ！！！！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        stage = 10;
        DisplayDirectory("gate");
        DisplayImage("gate");
        ToggleImageOpacity(false);
        DisplayCharacterName(chiki);
        DisplayText("「ビックリしたわね！………大丈夫？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("「な…なんとか…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"元いた場所に戻ったかと思えば、穴に吸い込まれるように落ちてしまった。落下したダメージはなかなかだが、{chiki}は浮いているから無傷みたいだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ねぇ、あっちを見て」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleObjectOpacity(false);
        ToggleImageOpacity(true);
        DisplayCharacterImage(null);
        DisplayCharacterName(user);
        DisplayText($"{chiki}の指を指す方向に目を向けると、明らかに空間を歪ませている物体があった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName("2人");
        DisplayText("「「これが…ゲート…」」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText("確証はないが手帳のメモ通りであればここで<color=orange>ssh</color>すれば元の世界に戻れるはずだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("突然あたりが暗くなる。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(sys);
        DisplayText("「外部からの不正なアクセスを可能にする脆弱性が発見されました。セキュリティパッチを適用します。」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「{user}！急いで！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("チキが切迫した様子で叫ぶ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「ここが外部につながる『セキュリティホール』だと認識されたのかもしれないわ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("「ゲートが塞がれたら二度と元の世界に戻れない！！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"謎のシステムによってゲートは攻撃を受けているが、{chiki}が必死にそれを止めようとしている。猶予はなさそうだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        beingMeasured = true; // タイマー開始

        DisplayText($"「そういえば、これまでの手がかりを{chiki}がmemo.txtに残してくれていたな」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("そんなことを思いながらゲートの前まで足早に移動する。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ResetInput();
        DisplayText("「ここでコマンドを叫ぶんだ！」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        while (true)
        {
            input = inputField.text;
            cmd = input;
            if (input == "cat memo.txt")
            {
                inputField.enabled = false;
                DisplayCharacterName("memo.txt");
                DisplayText("<color=orange>ssh</color>コマンド→<color=orange>ssh</color> -i (秘密鍵) (自身の名前)@(数字①).(数字②).(数字③).(数字④)");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

                ResetInput();
                DisplayText("秘密鍵→secretkey.pem\n数字①→192　数字②→168　数字③→11　数字④→11\n自身の名前→元の世界での自分の名前の半角英数字");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
            else if (rx.IsMatch(input))
            {
                break;
            }
            else
            {
                ResetInput();
                DisplayCharacterName(user);
                DisplayText("違うみたいだ。<color=orange>cat</color> memo.txtでもう一度確認しよう。");
                yield return new WaitForSeconds(waitTime);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        beingMeasured = false; // タイマー停止
        DisplayCharacterName(user);
        DisplayText($"{cmd}！！！");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleObjectOpacity(true);
        DisplayImage("white");
        DisplayCharacterName(user);
        DisplayText("あたり一面が光につつまれる。どうやらこの世界とはお別れのようだ。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("せめてチキに感謝の気持ちを伝えたかったのだが、もはや自分の意志で動くことも、言葉を発することもできなかった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
    }

    IEnumerator Chapter5_failuer()
    {
        isClearSSH = false;

        ToggleObjectOpacity(true);
        DisplayCharacterName(user);
        DisplayText("ゲートは崩壊した。もう元の世界に戻ることは不可能なのだろうか。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「〇〇…私を支えてて…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(false);
        DisplayCharacterImage(chiki);
        DisplayCharacterName(user);
        DisplayText($"ボロボロになった{chiki}は静かに語りかけた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「私の最後の力を使ってゲートを複製するわ。一回だけなら<color=orange>ssh</color>することもできるはず…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"「最後の力って…{chiki}はどうなるんだ！？」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(chiki);
        DisplayText("「いいから…！見てなさいよ」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayCharacterName(user);
        DisplayText($"そういうと{chiki}は手を前にさし出し力を込めた。手の先から放たれた光は不完全ではあるが、空間を切り抜いた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleObjectOpacity(false);
        DisplayCharacterName(chiki);
        DisplayText("「ssh -i secretkey.pem you@192.168.11.11...」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        ToggleImageOpacity(true);
        ToggleObjectOpacity(true);
        DisplayImage("white");
        DisplayCharacterName(user);
        DisplayText("チキがそう呟くとあたり一面が光に包まれた。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        StartCoroutine(Chapter6());
    }

    IEnumerator Chapter6()
    {
        stage = 1;
        DisplayDirectory(null);
        DisplayCharacterName("");
        DisplayText("...");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayImage("black");
        DisplayCharacterName("");
        DisplayText("...");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayImage("room");
        DisplayCharacterName(user);
        DisplayText("「ここは…」");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("そこは元の世界の自分の部屋だった。パソコンには引数なしでsshをした記録が残っている。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("興味本位で試しに同じコマンドを試してみる。\"↑\"キーと\"Enter\"キー…");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText("sshの利用法がただただ表示されるだけだった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        DisplayText($"{chiki}は無事だったのだろうか。それだけが気がかりだった。");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        if (isClearSSH)
        {
            DisplayCharacterName(sys);
            DisplayText("\\ピコン/");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            cmd = cmd.Replace("ssh -i secretkey.pem ", "").Replace("@192.168.11.11", "");

            DisplayCharacterName(chiki);
            DisplayText($"『@{cmd}\nこっちが元の世界？ずいぶんGUIが取り入れられてるのね！』");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            DisplayCharacterName(user);
            DisplayText("「いや、連絡取れるんかい！」");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        DisplayCharacterName(sys);
        DisplayText("\n　　　　　　　　　　　～おしまい～");
        yield return new WaitForSeconds(waitTime);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        if (!isClearSSH)
        {
            DisplayText("もう一度プレイして、sshコマンドを成功させよう！");
            yield return new WaitForSeconds(waitTime);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
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
            case 1:
                text = "";
                break;
            case 2:
                text = "World\n" +
                       "└ cyber_entrance\n" +
                       "\t└ cyber_central\n";
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
            case 5:
                text = "└ cyber_central\n" +
                       "\t├ cyber_avenue\n" +
                       "\t├ cyber_alley\n" +
                       "\t├ cyber_downtown\n"+
                       "\t└ .reverse_central\n";
                break;
            case 6:
                text = "└ .reverse_central\n" +
                       "\t├ red_box\n" +
                       "\t├ green_box\n" +
                       "\t├ blue_box\n" +
                       "\t└ yellow_box\n";
                break;
            case 7:
                text = "└ .reverse_central\n";
                break;
            case 8:
                text = "└ .reverse_central\n" +
                       "\t├ container\n" +
                       "\t├ safe\n" +
                       "\t├ pot\n" +
                       "\t└ bottle\n";
                break;
            case 9:
                text = "World\n" +
                       "\t└ cyber_entrance\n" +
                       "\t\t└ cyber_central\n" +
                       "\t\t\t├ cyber_avenue\n" +
                       "\t\t\t├ cyber_alley\n" +
                       "\t\t\t└ cyber_downtown\n";
                break;
            case 10:
                text = "gate";
                break;
            default:
                text = "";
                break;
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
    public void ResetInput()
    {
        inputField.text = "";
        inputField.enabled = true;
    }

    /// <summary>
    /// キャラクター画像の透明度を切り替える
    /// true -> 透明, false -> 不透明
    /// </summary>
    public void ToggleImageOpacity(bool boolValue)
    {
        if (boolValue)
        {
            characterImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            characterImage.color = new Color(1, 1, 1, 1);
        }
    }

    /// <summary>
    /// オブジェクト画像の透明度を切り替える
    /// true -> 透明, false -> 不透明
    /// </summary>
    public void ToggleObjectOpacity(bool boolValue)
    {
        if (boolValue)
        {
            objectImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            objectImage.color = new Color(1, 1, 1, 1);
        }
    }
}
