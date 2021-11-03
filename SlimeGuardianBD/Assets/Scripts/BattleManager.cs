using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public string sequence_style;
    public Text sequence_text;

    public Button style_toggle;
    public Text style_text;
    public Button speed_toggle;
    public Text speed_text;

    public string[] characters;
    public int[] speed;

    public string[] turn_Order;
    public int highest_speed_value;
    public int highest_speed_char;
    public int speed_check;

    public Text turnOrder_text;

    public int battle_start;

    void Start()
    {
        sequence_style = "Before Sequence";
        sequence_text = GameObject.Find("SequenceStyle_txt").GetComponent<Text>();

        style_toggle = GameObject.Find("ToggleBattleStyle_btn").GetComponent<Button>();
        style_text = GameObject.Find("ToggleBattleStyle_txt").GetComponent<Text>();
        speed_toggle = GameObject.Find("ToggleSpeeds_btn").GetComponent<Button>();
        speed_text = GameObject.Find("ToggleSpeeds_txt").GetComponent<Text>();

        style_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
        style_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        speed_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
        speed_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);

        speed = new int[4];

        for (int r = 0; r < speed.Length; r++)
        {
            speed[r] = Random.Range(1, 99);
        }

        turn_Order = new string[4];
        highest_speed_value = 0;
        highest_speed_char = 0;
        speed_check = 0;

        turnOrder_text = GameObject.Find("CharacterTurn_txt").GetComponent<Text>();

        battle_start = 0;
    }

    void Update()
    {

    }

    public void onToggleStyle()
    {
        if (battle_start != 1)
        {
            if (sequence_style == "Before Sequence")
            {
                sequence_style = "During Sequence";
            }
            else
            {
                sequence_style = "Before Sequence";
            }
            sequence_text.text = sequence_style;
        }
    }

    public void onRandomSpeed()
    {
        if (battle_start != 1)
        {
            for (int r = 0; r < speed.Length; r++)
            {
                speed[r] = Random.Range(1, 99);
            }
        }
    }

    public void startSequence()
    {
        if (battle_start == 0)
        {
            battle_start = 1;
            style_toggle.GetComponent<Image>().color = new Color32(42, 55, 82, 255);
            style_text.GetComponent<Text>().color = new Color32(82, 82, 82, 255);
            speed_toggle.GetComponent<Image>().color = new Color32(42, 55, 82, 255);
            speed_text.GetComponent<Text>().color = new Color32(82, 82, 82, 255);
            sortCharacters();
        }
        else
        {
            battle_start = 0;
            style_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
            style_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            speed_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
            speed_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);

            for (int r = 0; r < speed.Length; r++)
            {
                speed[r] = Random.Range(1, 99);
            }
        }
    }

    void sortCharacters()
    {
        for (int b = 0; b < turn_Order.Length; b++)
        {
            turn_Order[b] = "";
        }

        highest_speed_value = 0;
        highest_speed_char = 0;
        speed_check = 0;

        while (speed_check < turn_Order.Length)
        {
            for (int s = 0; s < turn_Order.Length; s++)
            {
                if (speed[s] > highest_speed_value)
                {
                    highest_speed_value = speed[s];
                    highest_speed_char = s;
                }
            }

            if (speed_check < turn_Order.Length)
            {
                turn_Order[speed_check] = characters[highest_speed_char];
                speed[highest_speed_char] = 0;
                highest_speed_value = 0;
                highest_speed_char = 0;
                speed_check++;
            }
        }
    }
}
