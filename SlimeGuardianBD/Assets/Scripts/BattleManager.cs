using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //CODE NOT FINAL: Code will be rewritten to be short and more optimized later in development, this is for testing two different battle styles

    public string sequence_style;
    public Text sequence_text;

    public Button style_toggle;
    public Text style_text;
    public Button speed_toggle;
    public Text speed_text;

    public string[] characters;
    public int[] speed;

    public string[] turn_order;
    public int highest_speed_value;
    public int highest_speed_char;
    public int speed_check;

    public int battle_start;

    public GameObject player_choice_menu;
    public GameObject ally_choice_menu;

    public int sequence_started;
    public int which_turn;
    public int choice_made;
    public Text turn_order_text;

    public GameObject player_obj;
    public GameObject ally_obj;
    public GameObject enemy1_obj;
    public GameObject enemy2_obj;
    public int character_move_checks;

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

        turn_order = new string[4];
        highest_speed_value = 0;
        highest_speed_char = 0;
        speed_check = 0;

        battle_start = 0;

        player_choice_menu = GameObject.Find("ChoiceMenuPlayer_bdr");
        player_choice_menu.SetActive(false);
        ally_choice_menu = GameObject.Find("ChoiceMenuAlly_bdr");
        ally_choice_menu.SetActive(false);

        sequence_started = 0;
        which_turn = 0;
        choice_made = 0;
        turn_order_text = GameObject.Find("CharacterTurn_txt").GetComponent<Text>();
        turn_order_text.text = "";

        player_obj = GameObject.Find("player_obj");
        player_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 217, 204, 255); 
        ally_obj = GameObject.Find("ally_obj");
        ally_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 135, 217, 255);
        enemy1_obj = GameObject.Find("enemy1_obj");
        enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
        enemy2_obj = GameObject.Find("enemy2_obj");
        enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
        character_move_checks = 0;
    }

    void Update()
    {
        if (sequence_started == 1)
        {
            if (sequence_style == "Before Sequence")
            {
                if (which_turn < turn_order.Length)
                {
                    if (turn_order[which_turn] == "Player")
                    {
                        if (character_move_checks == 0 && player_obj.transform.position.x < -550)
                        {
                            player_obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                            turn_order_text.text = "Player's Turn";
                            player_obj.transform.Translate(1, 0,0);
                        }
                        else if (character_move_checks == 0 && player_obj.transform.position.x >= -550)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime());
                            }   
                        }
                        else if (character_move_checks == 2 && player_obj.transform.position.x > -770)
                        {
                            player_obj.transform.Translate(-1, 0, 0);
                        }
                        else if (character_move_checks == 2 && player_obj.transform.position.x <= -770)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime());
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            player_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 217, 204, 255);
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Ally")
                    {
                        which_turn++;
                    }
                    else if (turn_order[which_turn] == "Enemy1")
                    {
                        which_turn++;
                    }
                    else if (turn_order[which_turn] == "Enemy2")
                    {
                        which_turn++;
                    }
                }
            }
            else if (sequence_style == "During Sequence")
            {
                if (which_turn < turn_order.Length)
                {
                    if (turn_order[which_turn] == "Player")
                    {
                        if (choice_made == 0)
                        {
                            player_choice_menu.SetActive(true);
                        }
                        else if (choice_made == 1)
                        {
                            choice_made = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Ally")
                    {
                        if (choice_made == 0)
                        {
                            ally_choice_menu.SetActive(true);
                        }
                        else if (choice_made == 1)
                        {
                            choice_made = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Enemy1")
                    {
                        which_turn++;
                    }
                    else if (turn_order[which_turn] == "Enemy2")
                    {
                        which_turn++;
                    }
                }
            }
        }
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

    public void startBattle()
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
        for (int b = 0; b < turn_order.Length; b++)
        {
            turn_order[b] = "";
        }

        highest_speed_value = 0;
        highest_speed_char = 0;
        speed_check = 0;
        which_turn = 0;

        while (speed_check < turn_order.Length)
        {
            for (int s = 0; s < turn_order.Length; s++)
            {
                if (speed[s] > highest_speed_value)
                {
                    highest_speed_value = speed[s];
                    highest_speed_char = s;
                }
            }

            if (speed_check < turn_order.Length)
            {
                turn_order[speed_check] = characters[highest_speed_char];
                speed[highest_speed_char] = 0;
                highest_speed_value = 0;
                highest_speed_char = 0;
                speed_check++;
            }
        }

        if (sequence_style == "Before Sequence")
        {
            which_turn = 0;
            player_choice_menu.SetActive(true);
        }
        else if (sequence_style == "During Sequence")
        {
            which_turn = 0;
            sequence_started = 1;
        }
    }

    public void playerAttackChoice()
    {
        if (sequence_style == "Before Sequence")
        {
            player_choice_menu.SetActive(false);
            ally_choice_menu.SetActive(true);
        }
        else if (sequence_style == "During Sequence")
        {
            player_choice_menu.SetActive(false);
            choice_made = 1;
        }
    }

    public void allyAttackChoice()
    {
        if (sequence_style == "Before Sequence")
        {
            ally_choice_menu.SetActive(false);
            sequence_started = 1;
        }
        else if (sequence_style == "During Sequence")
        {
            ally_choice_menu.SetActive(false);
            choice_made = 1;
        }
    }

    IEnumerator waitSomeTime()
    {
        yield return new WaitForSeconds(0.5f);
        character_move_checks++;
    }
}