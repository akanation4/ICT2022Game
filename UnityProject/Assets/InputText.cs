using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを扱う
using System.Text.RegularExpressions;

public struct Command{
    public string command;
    public string option;
}

public class InputText : MonoBehaviour
{
 // inputfieldを格納する変数
    public InputField inputField;

    // テキストを格納する変数
    public Text text;

    // Use this for initialization
    void Start () {

        //InputFieldコンポーネントを格納
        inputField = GetComponent<InputField>();

    }
    
    // Update is called once per frame
    void Update () {
    
    }
    
    // InputFieldに入力された内容をテキストに表示
    public void DisplayText()
    {
        //構造体を宣言
        Command command = new Command();
        // InputFieldに入力された内容をコマンド自体と引数に分解したい
        string[] splitKey = inputField.text.Split(' ');
        foreach (string key in splitKey){
            System.Console.WriteLine(key);
           // Debug.Log(key);
            if(Regex.IsMatch(key, @"ls")){
                command.command = key;
            Debug.Log(command.command);
        }else if(Regex.IsMatch(key, @"-/d")){
                command.option = key;
            Debug.Log(command.option);
                }

        //最初に構造体とかで「ls」とか「cd」とかコマンド自体を定義->要更新
        //Rexex.IsMatchで入力された文字列にオプションが含まれているかを判定
        //ここから下は動作可能
        if(Regex.IsMatch(inputField.text,@"-l")){
        Debug.Log("-l");
        // テキストに判定結果を表示
        text.GetComponent<Text>().text = "listオプションを検知しました";
        //Debug.Log("きたー");
        }else if(Regex.IsMatch(inputField.text, @"-a")){
        // テキストに判定結果を表示
        text.GetComponent<Text>().text = "addオプションを検知しました";
        //Debug.Log("-a");
        }else{
        text.GetComponent<Text>().text = "オプションを検知できませんでした";
        Debug.Log("オプションを検知できませんでした");
        }
    }

    }
}