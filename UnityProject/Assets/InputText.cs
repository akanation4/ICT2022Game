
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI������
 
public class InputText : MonoBehaviour
{
 // inputfield���i�[����ϐ�
    public InputField inputField;
 
    // �e�L�X�g���i�[����ϐ�
    public Text text;
     
 
    // Use this for initialization
    void Start () {
 
        //InputField�R���|�[�l���g���i�[
        inputField = GetComponent<InputField>();
 
    }
    
    // Update is called once per frame
    void Update () {
    
    }
 
    // InputField�ɓ��͂��ꂽ���e���e�L�X�g�ɕ\��
    public void DisplayText()
    {
        if(inputField.text == "1234"){
        Debug.Log("�������ł�");
        text.GetComponent<Text>().text = "�p�X���[�h����";
        }else
        {
        // �e�L�X�g�ɓ��͓��e��\��
        text.GetComponent<Text>().text = "�p�X���[�h���Ⴂ�܂�";
        Debug.Log("�������Ȃ��ł�");
    }

}
}