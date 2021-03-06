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
    public Button battle_toggle;
    public Text battle_text;

    public Text playerspeed_displaytxt;
    public Text allyspeed_displaytxt;
    public Text enemy1speed_displaytxt;
    public Text enemy2speed_displaytxt;

    public string[] characters;
    public int[] staticspeeds;
    public int[] tempspeeds;

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
    public int movement_speed;

    void Start()
    {
        sequence_style = "Before Sequence";
        sequence_text = GameObject.Find("SequenceStyle_txt").GetComponent<Text>();

        style_toggle = GameObject.Find("ToggleBattleStyle_btn").GetComponent<Button>();
        style_text = GameObject.Find("ToggleBattleStyle_txt").GetComponent<Text>();
        speed_toggle = GameObject.Find("ToggleSpeeds_btn").GetComponent<Button>();
        speed_text = GameObject.Find("ToggleSpeeds_txt").GetComponent<Text>();
        battle_toggle = GameObject.Find("ToggleBattle_btn").GetComponent<Button>();
        battle_text = GameObject.Find("ToggleBattle_txt").GetComponent<Text>();

        playerspeed_displaytxt = GameObject.Find("PlayerSpeedText").GetComponent<Text>();
        allyspeed_displaytxt = GameObject.Find("AllySpeedText").GetComponent<Text>();
        enemy1speed_displaytxt = GameObject.Find("Enemy1SpeedText").GetComponent<Text>();
        enemy2speed_displaytxt = GameObject.Find("Enemy2SpeedText").GetComponent<Text>();

        style_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
        style_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        speed_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
        speed_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        battle_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
        battle_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        battle_text.text = "Start Battle";

        staticspeeds = new int[4];
        tempspeeds = new int[4];

        onRandomSpeed();

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
        movement_speed = 1000;
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
                            player_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0,0);
                        }
                        else if (character_move_checks == 0 && player_obj.transform.position.x >= -550)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }   
                        }
                        else if (character_move_checks == 2 && player_obj.transform.position.x > -770)
                        {
                            player_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && player_obj.transform.position.x <= -770)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            player_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 217, 204, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Ally")
                    {
                        if (character_move_checks == 0 && ally_obj.transform.position.x < -550)
                        {
                            ally_obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                            turn_order_text.text = "Ally's Turn";
                            ally_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 0 && ally_obj.transform.position.x >= -550)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 2 && ally_obj.transform.position.x > -770)
                        {
                            ally_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && ally_obj.transform.position.x <= -770)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            ally_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 135, 217, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Enemy1")
                    {
                        if (character_move_checks == 0 && enemy1_obj.transform.position.x > 200)
                        {
                            enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                            turn_order_text.text = "Enemy 1's Turn";
                            enemy1_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 0 && enemy1_obj.transform.position.x <= 200)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 2 && enemy1_obj.transform.position.x < 420)
                        {
                            enemy1_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && enemy1_obj.transform.position.x >= 420)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Enemy2")
                    {
                        if (character_move_checks == 0 && enemy2_obj.transform.position.x > 200)
                        {
                            enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                            turn_order_text.text = "Enemy 2's Turn";
                            enemy2_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 0 && enemy2_obj.transform.position.x <= 200)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 2 && enemy2_obj.transform.position.x < 420)
                        {
                            enemy2_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && enemy2_obj.transform.position.x >= 420)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }

                    if (which_turn == 4)
                    {
                        turn_order_text.text = "Turns Over, Repeating Soon";
                        StartCoroutine(waitSomeTime(1f));
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
                            turn_order_text.text = "";
                            player_choice_menu.SetActive(true);
                        }
                        else if (choice_made == 1)
                        {
                            if (character_move_checks == 0 && player_obj.transform.position.x < -550)
                            {
                                player_obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                                turn_order_text.text = "Player's Turn";
                                player_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                            }
                            else if (character_move_checks == 0 && player_obj.transform.position.x >= -550)
                            {
                                if (character_move_checks == 0)
                                {
                                    character_move_checks = 1;
                                    StartCoroutine(waitSomeTime(0.5f));
                                }
                            }
                            else if (character_move_checks == 2 && player_obj.transform.position.x > -770)
                            {
                                player_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                            }
                            else if (character_move_checks == 2 && player_obj.transform.position.x <= -770)
                            {
                                if (character_move_checks == 2)
                                {
                                    character_move_checks = 3;
                                    StartCoroutine(waitSomeTime(0.5f));
                                }
                            }
                            else if (character_move_checks == 4)
                            {
                                player_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 217, 204, 255);
                                character_move_checks = 0;
                                choice_made = 0;
                                which_turn++;
                            }
                        }
                    }
                    else if (turn_order[which_turn] == "Ally")
                    {
                        if (choice_made == 0)
                        {
                            turn_order_text.text = "";
                            ally_choice_menu.SetActive(true);
                        }
                        else if (choice_made == 1)
                        {
                            if (character_move_checks == 0 && ally_obj.transform.position.x < -550)
                            {
                                ally_obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                                turn_order_text.text = "Ally's Turn";
                                ally_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                            }
                            else if (character_move_checks == 0 && ally_obj.transform.position.x >= -550)
                            {
                                if (character_move_checks == 0)
                                {
                                    character_move_checks = 1;
                                    StartCoroutine(waitSomeTime(0.5f));
                                }
                            }
                            else if (character_move_checks == 2 && ally_obj.transform.position.x > -770)
                            {
                                ally_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                            }
                            else if (character_move_checks == 2 && ally_obj.transform.position.x <= -770)
                            {
                                if (character_move_checks == 2)
                                {
                                    character_move_checks = 3;
                                    StartCoroutine(waitSomeTime(0.5f));
                                }
                            }
                            else if (character_move_checks == 4)
                            {
                                ally_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 135, 217, 255);
                                character_move_checks = 0;
                                choice_made = 0;
                                which_turn++;
                            }
                        }
                    }
                    else if (turn_order[which_turn] == "Enemy1")
                    {
                        if (character_move_checks == 0 && enemy1_obj.transform.position.x > 200)
                        {
                            enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                            turn_order_text.text = "Enemy 1's Turn";
                            enemy1_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 0 && enemy1_obj.transform.position.x <= 200)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 2 && enemy1_obj.transform.position.x < 420)
                        {
                            enemy1_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && enemy1_obj.transform.position.x >= 420)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }
                    else if (turn_order[which_turn] == "Enemy2")
                    {
                        if (character_move_checks == 0 && enemy2_obj.transform.position.x > 200)
                        {
                            enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                            turn_order_text.text = "Enemy 2's Turn";
                            enemy2_obj.transform.Translate(-1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 0 && enemy2_obj.transform.position.x <= 200)
                        {
                            if (character_move_checks == 0)
                            {
                                character_move_checks = 1;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 2 && enemy2_obj.transform.position.x < 420)
                        {
                            enemy2_obj.transform.Translate(1 * movement_speed * Time.deltaTime, 0, 0);
                        }
                        else if (character_move_checks == 2 && enemy2_obj.transform.position.x >= 420)
                        {
                            if (character_move_checks == 2)
                            {
                                character_move_checks = 3;
                                StartCoroutine(waitSomeTime(0.5f));
                            }
                        }
                        else if (character_move_checks == 4)
                        {
                            enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
                            character_move_checks = 0;
                            which_turn++;
                        }
                    }

                    if (which_turn == 4)
                    {
                        turn_order_text.text = "Turns Over, Repeating Soon";
                        StartCoroutine(waitSomeTime(1f));
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
            for (int r = 0; r < staticspeeds.Length; r++)
            {
                staticspeeds[r] = Random.Range(1, 99);
            }
        }

        playerspeed_displaytxt.text = "Speed " + staticspeeds[0];
        allyspeed_displaytxt.text = "Speed " + staticspeeds[1];
        enemy1speed_displaytxt.text = "Speed " + staticspeeds[2];
        enemy2speed_displaytxt.text = "Speed " + staticspeeds[3];
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
            battle_toggle.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            battle_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            battle_text.text = "Stop Battle";
            sortCharacters();
        }
        else
        {
            battle_start = 0;
            style_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
            style_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            speed_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
            speed_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            battle_toggle.GetComponent<Image>().color = new Color32(0, 73, 229, 255);
            battle_text.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
            battle_text.text = "Start Battle";

            sequence_started = 0;
            player_choice_menu.SetActive(false);
            ally_choice_menu.SetActive(false);
            choice_made = 0;

            player_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 217, 204, 255);
            player_obj.transform.position = new Vector3(-770, player_obj.transform.position.y, player_obj.transform.position.z);
            ally_obj.GetComponent<SpriteRenderer>().color = new Color32(109, 135, 217, 255);
            ally_obj.transform.position = new Vector3(-770, ally_obj.transform.position.y, ally_obj.transform.position.z);
            enemy1_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
            enemy1_obj.transform.position = new Vector3(420, enemy1_obj.transform.position.y, enemy1_obj.transform.position.z);
            enemy2_obj.GetComponent<SpriteRenderer>().color = new Color32(171, 20, 30, 255);
            enemy2_obj.transform.position = new Vector3(420, enemy2_obj.transform.position.y, enemy2_obj.transform.position.z);

            which_turn = 0;
            character_move_checks = 0;
            turn_order_text.text = "";
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void sortCharacters()
    {
        for (int b = 0; b < turn_order.Length; b++)
        {
            turn_order[b] = "";
        }

        for (int s = 0; s < turn_order.Length; s++)
        {
            tempspeeds[s] = staticspeeds[s];
        }

        highest_speed_value = 0;
        highest_speed_char = 0;
        speed_check = 0;

        while (speed_check < turn_order.Length)
        {
            for (int s = 0; s < turn_order.Length; s++)
            {
                if (tempspeeds[s] > highest_speed_value)
                {
                    highest_speed_value = tempspeeds[s];
                    highest_speed_char = s;
                }
            }

            if (speed_check < turn_order.Length)
            {
                turn_order[speed_check] = characters[highest_speed_char];
                tempspeeds[highest_speed_char] = 0;
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

    IEnumerator waitSomeTime(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        
        if (which_turn < 4 && battle_start == 1)
        {
            character_move_checks++;
        }
        else if (which_turn <= 4 && battle_start == 1)
        {
            turn_order_text.text = "";
            which_turn = 0;

            if (sequence_style == "Before Sequence")
            {
                player_choice_menu.SetActive(true);
                sequence_started = 0;
            }
        }
    }
}