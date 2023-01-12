using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI������
using System.Text.RegularExpressions;

public struct Command{
    public string command;
    public string option;
}

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
        //�\���̂�錾
        Command command = new Command();
        // InputField�ɓ��͂��ꂽ���e���R�}���h���̂ƈ����ɕ���������
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

        //�ŏ��ɍ\���̂Ƃ��Łuls�v�Ƃ��ucd�v�Ƃ��R�}���h���̂��`->�v�X�V
        //Rexex.IsMatch�œ��͂��ꂽ������ɃI�v�V�������܂܂�Ă��邩�𔻒�
        //�������牺�͓���\
        if(Regex.IsMatch(inputField.text,@"-l")){
        Debug.Log("-l");
        // �e�L�X�g�ɔ��茋�ʂ�\��
        text.GetComponent<Text>().text = "list�I�v�V���������m���܂���";
        //Debug.Log("�����[");
        }else if(Regex.IsMatch(inputField.text, @"-a")){
        // �e�L�X�g�ɔ��茋�ʂ�\��
        text.GetComponent<Text>().text = "add�I�v�V���������m���܂���";
        //Debug.Log("-a");
        }else{
        text.GetComponent<Text>().text = "�I�v�V���������m�ł��܂���ł���";
        Debug.Log("�I�v�V���������m�ł��܂���ł���");
        }
    }

    }
}