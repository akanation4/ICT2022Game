using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

class StoryBlock {
    public string story;
    public string option1text;
    public string option2text;
    public StoryBlock option1Block;
    public StoryBlock option2Block;

    public StoryBlock(string story, string option1text = "", string option2text = "",
        StoryBlock option1Block = null, StoryBlock option2Block = null) {

        this.story = story;
        this.option1text = option1text;
        this.option2text = option2text;
        this.option1Block = option1Block;
        this.option2Block = option2Block;
    }
}





public class GameManager : MonoBehaviour {
    public Text mainText;
    public Button option1;
    public Button option2;
    public InputField inputField;

    StoryBlock currentBlock;

    static StoryBlock block8 = new StoryBlock("You decided to sit here forever. Game over", "", "");
    static StoryBlock block7 = new StoryBlock("You decided to go back to the start. Game over", "", "");
    static StoryBlock block6 = new StoryBlock("You decided to go back to the start. Game over", "", "", block7, block8);
    static StoryBlock block5 = new StoryBlock("You decided to go back to the start. Game over", "Inspect floor", "", block6, block2);
    static StoryBlock block4 = new StoryBlock("You decided to go back to the start. Game over", "Check your pockets", "Inspect floor", block5, block6);
    static StoryBlock block3 = new StoryBlock("You noticed a big, wooden doors on the opposite side of the room. After quick inspection, it looks like they are closed. But there is a key lock!", "Inspect floor", "Do nothing", block1, block8);
    static StoryBlock block2 = new StoryBlock("You started to panic and screamed for help, but it looks like, you're here completry alone.", "Check the doors", "Sit down", block3, block8);
    static StoryBlock block1 = new StoryBlock("You just woke up in a small, dark cell in an old castel.", "Look for other people", "Check the doors", block2, block3);



    // Start is called before the first frame update
    void Start()
    {
        // DisplayBlock(block1);

        // string path = Application.dataPath + "/story.csv";
        // bool isAppend = true;

        // using (StreamReader fs = new StreamReader(path))
        // {
        //     while (fs.Peek() != -1)
        //     {
        //         Debug.Log(fs.ReadLine());
        //     }
        // }

        inputField = inputField.GetComponent<InputField>();
        mainText = mainText.GetComponent<Text>();

        mainText.text = "Enter 'Hello, world'";
        inputField.text = "";
    }

    void DisplayBlock(StoryBlock block) {
        Debug.Log(currentBlock);
        mainText.text = block.story;
        option1.GetComponentInChildren<Text>().text = block.option1text;
        option2.GetComponentInChildren<Text>().text = block.option2text;

        currentBlock = block;
        Debug.Log(currentBlock.option1Block);
    }

    public void Button1Clicked() {
        Debug.Log("Button 1 clicked");
        DisplayBlock(currentBlock.option1Block);
    }

    public void Button2Clicked() {
        DisplayBlock(block2);
    }

    public void InputText() {
        if (inputField.text == "Hello, world") {
            mainText.text = "Good! Hello, world";
        } else {
            mainText.text = "Wrong! Try again";
        }
    }
}
