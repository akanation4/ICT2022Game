
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを扱う
 
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
        if(inputField.text == "1234"){
        Debug.Log("正しいです");
        text.GetComponent<Text>().text = "パスワード完了";
        }else
        {
        // テキストに入力内容を表示
        text.GetComponent<Text>().text = "パスワードが違います";
        Debug.Log("正しくないです");
    }

}
}