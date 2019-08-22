
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour {

    public player playerPrefab;
    public List<player> players; //
    public Character[] ninja; //
    public Character[] samurai; //
    EventSystem eventSystem;
    StandaloneInputModule inputModule;

    //Buttons for each character
    public GameObject Ninja01Panel;
    public GameObject Ninja02Panel;
    public GameObject Samurai01Panel;
    public GameObject Samurai02Panel;
    public GameObject CharPanels;
    public GameObject ControlPanel;
    bool gameStarts = true;
    bool firstGame = true;


    //Buttons to be first selected when Character Selector opens up
    public Button firstNinja01; // = Ninja01Panel.GetComponent<Button>();
    public Button firstSamurai01;
    public Button firstNinja02;
    public Button firstSamurai02;

    public Text MenuText;
    public Text P1Text;
    public Text P2Text;

    int currentPlayer = 0;

    //Game State Stuff
    bool playing = false;

    //Adding the sounds needed
    public AudioSource buttonPress;
    public AudioSource characterSelectBg;
    public AudioSource fightLevelBg;
    bool playSound = false;

    //Images for Speech Bubbles and HP Bars
    public Image ninja01Agg;
    public Image ninja01Pass;
    public Image ninja01Dead;
    public Image ninja01HpFull;
    public Image ninja01HpHalf;
    public Image ninja01HpEmpty;
    public Image ninja02Agg;
    public Image ninja02Pass;
    public Image ninja02Dead;
    public Image ninja02HpFull;
    public Image ninja02HpHalf;
    public Image ninja02HpEmpty;
    public Image Samurai01Agg;
    public Image Samurai01Pass;
    public Image Samurai01Dead;
    public Image Samurai01HpFull;
    public Image Samurai01HpHalf;
    public Image Samurai01HpEmpty;
    public Image Samurai02Agg;
    public Image Samurai02Pass;
    public Image Samurai02Dead;
    public Image Samurai02HpFull;
    public Image Samurai02HpHalf;
    public Image Samurai02HpEmpty;

    //Buttons for when the player's HP goes done
    public Button ninja01DeadPanel;
    public Button ninja02DeadPanel;
    public Button Samurai01DeadPanel;
    public Button Samurai02DeadPanel;

    //player 1 or player 2 winning sign of each round
    public GameObject player1wins;
    public GameObject player2wins;

    //game over
    public GameObject gameover;
    bool isGameOver = false;

    //Called when button in panel is pressed.
    public void OnCharacterSelect(int selectedCharacter)
    {
        
        //Ninja provide value 0-3, Samurai 10-13
        player p = players[currentPlayer];
        if (selectedCharacter < 10)
        {
            Character c = ninja[selectedCharacter];
            p.hasCharacter = true;
            p.characterName = c.characterName;
            p.faction = c.faction;
            p.HP = c.HP;
            p.charSprite = c.charSprite;
            p.ac = c.ac;

            //Disable all Ninja buttons (and anything associated with it)
            Ninja01Panel.SetActive(false);
            Ninja02Panel.SetActive(false);
            ninja01DeadPanel.gameObject.SetActive(false);
            ninja02DeadPanel.gameObject.SetActive(false);
            ninja01Agg.GetComponent<Image>().enabled = false;
            ninja01Pass.GetComponent<Image>().enabled = false;
            ninja01Dead.GetComponent<Image>().enabled = false;
            ninja01HpFull.GetComponent<Image>().enabled = false;
            ninja01HpHalf.GetComponent<Image>().enabled = false;
            ninja01HpEmpty.GetComponent<Image>().enabled = false;
            ninja02Agg.GetComponent<Image>().enabled = false;
            ninja02Pass.GetComponent<Image>().enabled = false;
            ninja02Dead.GetComponent<Image>().enabled = false;
            ninja02HpFull.GetComponent<Image>().enabled = false;
            ninja02HpHalf.GetComponent<Image>().enabled = false;
            ninja02HpEmpty.GetComponent<Image>().enabled = false;

            //Place the cursor to the character that still has HP
            if (samurai[0].HP > 0)
            {
                Samurai01Panel.GetComponentInChildren<Button>().Select();
            }
            else if (samurai[1].HP > 0)
            {
                Samurai02Panel.GetComponentInChildren<Button>().Select();
            }
        }
        else
        {
            Character c = samurai[selectedCharacter - 10];
            p.hasCharacter = true;
            p.characterName = c.characterName;
            p.faction = c.faction;
            p.HP = c.HP;
            p.charSprite = c.charSprite;
            p.ac = c.ac;

            //Disable all Samurai Buttons (and anything associated with it)
            Samurai01Panel.SetActive(false);
            Samurai02Panel.SetActive(false);
            Samurai01DeadPanel.gameObject.SetActive(false);
            Samurai02DeadPanel.gameObject.SetActive(false);
            Samurai01Agg.GetComponent<Image>().enabled = false;
            Samurai01Pass.GetComponent<Image>().enabled = false;
            Samurai01Dead.GetComponent<Image>().enabled = false;
            Samurai01HpFull.GetComponent<Image>().enabled = false;
            Samurai01HpHalf.GetComponent<Image>().enabled = false;
            Samurai01HpEmpty.GetComponent<Image>().enabled = false;
            Samurai02Agg.GetComponent<Image>().enabled = false;
            Samurai02Pass.GetComponent<Image>().enabled = false;
            Samurai02Dead.GetComponent<Image>().enabled = false;
            Samurai02HpFull.GetComponent<Image>().enabled = false;
            Samurai02HpHalf.GetComponent<Image>().enabled = false;
            Samurai02HpEmpty.GetComponent<Image>().enabled = false;

            if (ninja[0].HP > 0)
            {
                Ninja01Panel.GetComponentInChildren<Button>().Select();
                Debug.Log("can select ninja 1");
            }
            else if (ninja[1].HP > 0)
            {
                Ninja02Panel.GetComponentInChildren<Button>().Select();
                Debug.Log("can select ninja 2");
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //Determining the first button that can be selected
        eventSystem = gameObject.GetComponentInChildren<EventSystem>();
        inputModule = gameObject.GetComponentInChildren<StandaloneInputModule>();
        inputModule.enabled = false;

        //All SpeechBubbles/HpBar images start as false
        ninja01Agg.GetComponent<Image>().enabled = false;
        ninja01Pass.GetComponent<Image>().enabled = false;
        ninja01Dead.GetComponent<Image>().enabled = false;
        ninja01HpFull.GetComponent<Image>().enabled = false;
        ninja01HpHalf.GetComponent<Image>().enabled = false;
        ninja01HpEmpty.GetComponent<Image>().enabled = false;
        ninja02Agg.GetComponent<Image>().enabled = false;
        ninja02Pass.GetComponent<Image>().enabled = false;
        ninja02Dead.GetComponent<Image>().enabled = false;
        ninja02HpFull.GetComponent<Image>().enabled = false;
        ninja02HpHalf.GetComponent<Image>().enabled = false;
        ninja02HpEmpty.GetComponent<Image>().enabled = false;
        Samurai01Agg.GetComponent<Image>().enabled = false;
        Samurai01Pass.GetComponent<Image>().enabled = false;
        Samurai01Dead.GetComponent<Image>().enabled = false;
        Samurai01HpFull.GetComponent<Image>().enabled = false;
        Samurai01HpHalf.GetComponent<Image>().enabled = false;
        Samurai01HpEmpty.GetComponent<Image>().enabled = false;
        Samurai02Agg.GetComponent<Image>().enabled = false;
        Samurai02Pass.GetComponent<Image>().enabled = false;
        Samurai02Dead.GetComponent<Image>().enabled = false;
        Samurai02HpFull.GetComponent<Image>().enabled = false;
        Samurai02HpHalf.GetComponent<Image>().enabled = false;
        Samurai02HpEmpty.GetComponent<Image>().enabled = false;

        for (int i = 0; i < ninja.Length; i++)
        {
            ninja[i].HP = 100;
            samurai[i].HP = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count < 2)
        {
            if (playSound == false)
            {
                characterSelectBg.Play();
                playSound = true;
            }
            checkNewPlayer();
            if (players.Count == 1)
            {
                P1Text.text = players[0].characterName;
            }
        }

        if (!playing)
        {
            //Multiplayer Input
            MPcharSelect();

            //Singleplayer Input
            //SPcharSelect();
        } else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].inTeaRoom)
                {
                    fightLevelBg.Stop();
                }

                if (players[i].lives <= 0)
                {
                    updateCharHealth(players[i]);
                    players[0].canTurnRed = false;
                    players[1].canTurnRed = false;
                    if((ninja[0].HP <= 0 && ninja[1].HP <= 0) ||(samurai[0].HP <= 0 && samurai[1].HP <= 0))
                    {
                        gameover.SetActive(true);
                        players[0].GetComponent<player>().enabled = false;
                        players[1].GetComponent<player>().enabled = false;
                        break;  
                    }
                    //CharPanels.SetActive(true);
                    initPlayers();
                    playing = false;
                    //initialSelect();
                    StartCoroutine(playerlose(i));
                    fightLevelBg.Stop();
                    characterSelectBg.Play();

                    break;
                }
            }
        }

    }

    void initialSelect()
    {
        if (ninja[0].HP > 0)
        {
            Ninja01Panel.GetComponentInChildren<Button>().Select();
            Debug.Log("can select ninja 1");
        }
        else if (ninja[1].HP > 0)
        {
            Ninja02Panel.GetComponentInChildren<Button>().Select();
            Debug.Log("can select ninja 2");
        }
    }

    void updateCharHealth(player p)
    {
        for (int i = 0; i < ninja.Length; i++)
        {
            if (p.characterName == ninja[i].characterName)
            {
                ninja[i].HP -= 51;
                return;
            } else if (p.characterName == samurai[i].characterName)
            {
                samurai[i].HP -= 51;
                return;
            }
        }
    }

    void initPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].grabbing = false;
            players[i].isFiring = false;
            players[i].inTeaRoom = false;
            players[i].ballTravelling = false;
            players[i].gameObject.SetActive(false);
            players[i].lives = 3;
            players[i].hasCharacter = false;
        }
    }

    //Players choose characters one at a time after both have been added.
    void MPcharSelect()
    {
        if (players.Count == 2)
        {
            P1Text.text = players[0].characterName;
            P2Text.text = players[1].characterName;
            Character n01 = ninja[0];
            Character n02 = ninja[1];
            Character s01 = samurai[0];
            Character s02 = samurai[1];

            if (!Ninja01Panel.activeSelf && !Samurai01Panel.activeSelf &&
                !Ninja02Panel.activeSelf && !Samurai02Panel.activeSelf)
            {

                //If at full Hp, show full HP bar and Aggressive speech bubble
                if (n01.HP == 100)
                {
                    Ninja01Panel.SetActive(true);
                    ninja01HpFull.GetComponent<Image>().enabled = true;
                    ninja01Agg.GetComponent<Image>().enabled = true;
                }
                if (n02.HP == 100)
                {
                    Ninja02Panel.SetActive(true);
                    ninja02HpFull.GetComponent<Image>().enabled = true;
                    ninja02Agg.GetComponent<Image>().enabled = true;
                }
                if (s01.HP == 100)
                {
                    Samurai01Panel.SetActive(true);
                    Samurai01HpFull.GetComponent<Image>().enabled = true;
                    Samurai01Agg.GetComponent<Image>().enabled = true;
                }
                if (s02.HP == 100)
                {
                    Samurai02Panel.SetActive(true);
                    Samurai02HpFull.GetComponent<Image>().enabled = true;
                    Samurai02Agg.GetComponent<Image>().enabled = true;
                }

                //The winner of the round with more than 0 HP will show with half HP bar & passive speech bubble
                if (n01.HP > 0 && n01.HP < 100)
                {
                    Ninja01Panel.SetActive(false);
                    ninja01HpFull.GetComponent<Image>().enabled = false;
                    ninja01Agg.GetComponent<Image>().enabled = false;
                    firstNinja01.gameObject.SetActive(false);
                    ninja01HpEmpty.GetComponent<Image>().enabled = false;
                    ninja01Dead.GetComponent<Image>().enabled = false;

                    Ninja01Panel.SetActive(true);
                    ninja01HpHalf.GetComponent<Image>().enabled = true;
                    ninja01Pass.GetComponent<Image>().enabled = true;
                }
                if (n02.HP > 0 && n02.HP < 100)
                {
                    Ninja02Panel.SetActive(false);
                    ninja02HpFull.GetComponent<Image>().enabled = false;
                    ninja02Agg.GetComponent<Image>().enabled = false;
                    firstNinja02.gameObject.SetActive(false);
                    ninja02HpEmpty.GetComponent<Image>().enabled = false;
                    ninja02Dead.GetComponent<Image>().enabled = false;

                    Ninja02Panel.SetActive(true);
                    ninja02HpHalf.GetComponent<Image>().enabled = true;
                    ninja02Pass.GetComponent<Image>().enabled = true;
                }
                if (s01.HP > 0 && s01.HP < 100)
                {
                    Samurai01Panel.SetActive(false);
                    Samurai01HpFull.GetComponent<Image>().enabled = false;
                    Samurai01Agg.GetComponent<Image>().enabled = false;
                    firstSamurai01.gameObject.SetActive(false);
                    Samurai01HpEmpty.GetComponent<Image>().enabled = false;
                    Samurai01Dead.GetComponent<Image>().enabled = false;

                    Samurai01Panel.SetActive(true);
                    Samurai01HpHalf.GetComponent<Image>().enabled = true;
                    Samurai01Pass.GetComponent<Image>().enabled = true;
                }
                if (s02.HP > 0 && s02.HP < 100)
                {
                    Samurai02Panel.SetActive(false);
                    Samurai02HpFull.GetComponent<Image>().enabled = false;
                    Samurai02Agg.GetComponent<Image>().enabled = false;
                    firstSamurai02.gameObject.SetActive(false);
                    Samurai02HpEmpty.GetComponent<Image>().enabled = false;
                    Samurai02Dead.GetComponent<Image>().enabled = false;

                    Samurai02Panel.SetActive(true);
                    Samurai02HpHalf.GetComponent<Image>().enabled = true;
                    Samurai02Pass.GetComponent<Image>().enabled = true;
                }

                //The loser(0 Hp) will show with blackened screen and no text
                if (n01.HP <= 0)
                {
                    Ninja01Panel.SetActive(false);
                    ninja01HpFull.GetComponent<Image>().enabled = false;
                    ninja01Agg.GetComponent<Image>().enabled = false;
                    Ninja01Panel.SetActive(false);
                    ninja01HpHalf.GetComponent<Image>().enabled = false;
                    ninja01Pass.GetComponent<Image>().enabled = false;

                    ninja01DeadPanel.gameObject.SetActive(true);
                    ninja01DeadPanel.interactable = false;
                    ninja01HpEmpty.GetComponent<Image>().enabled = true;
                    ninja01Dead.GetComponent<Image>().enabled = true;
                }
                if (n02.HP <= 0)
                {
                    Ninja02Panel.SetActive(false);
                    ninja02HpFull.GetComponent<Image>().enabled = false;
                    ninja02Agg.GetComponent<Image>().enabled = false;
                    Ninja02Panel.SetActive(false);
                    ninja02HpHalf.GetComponent<Image>().enabled = false;
                    ninja02Pass.GetComponent<Image>().enabled = false;

                    ninja02DeadPanel.gameObject.SetActive(true);
                    ninja02DeadPanel.interactable = false;
                    ninja02HpEmpty.GetComponent<Image>().enabled = true;
                    ninja02Dead.GetComponent<Image>().enabled = true;
                }
                if (s01.HP <= 0)
                {
                    Samurai01Panel.SetActive(false);
                    Samurai01HpFull.GetComponent<Image>().enabled = false;
                    Samurai01Agg.GetComponent<Image>().enabled = false;
                    Samurai01Panel.SetActive(false);
                    Samurai01HpHalf.GetComponent<Image>().enabled = false;
                    Samurai01Pass.GetComponent<Image>().enabled = false;

                    Samurai01DeadPanel.gameObject.SetActive(true);
                    Samurai01DeadPanel.interactable = false;
                    Samurai01HpEmpty.GetComponent<Image>().enabled = true;
                    Samurai01Dead.GetComponent<Image>().enabled = true;
                }
                if (s02.HP <= 0)
                {
                    Samurai02Panel.SetActive(false);
                    Samurai02HpFull.GetComponent<Image>().enabled = false;
                    Samurai02Agg.GetComponent<Image>().enabled = false;
                    Samurai02Panel.SetActive(false);
                    Samurai02HpHalf.GetComponent<Image>().enabled = false;
                    Samurai02Pass.GetComponent<Image>().enabled = false;

                    Samurai02DeadPanel.gameObject.SetActive(true);
                    Samurai02DeadPanel.interactable = false;
                    Samurai02HpEmpty.GetComponent<Image>().enabled = true;
                    Samurai02Dead.GetComponent<Image>().enabled = true;
                }
                initialSelect();
            }
            if (!players[0].hasCharacter)
            {
                MenuText.text = "Player 1 choose your character!";
                setMenuInput(players[0].conNumber);
                currentPlayer = 0;
            }
            else if (players.Count == 2 && !players[1].hasCharacter)
            {
                MenuText.text = "Player 2 choose your character!";
                setMenuInput(players[1].conNumber);
                currentPlayer = 1;
            }
            else if (players.Count == 2 && players[0].hasCharacter && players[1].hasCharacter)
            {
                Ninja01Panel.SetActive(false);
                Ninja02Panel.SetActive(false);
                Samurai01Panel.SetActive(false);
                Samurai02Panel.SetActive(false);
                CharPanels.SetActive(false);
                if (firstGame == true)
                {
                    ControlPanel.SetActive(true);
                    if (Input.GetButtonDown("Start_" + players[0].conNumber) || Input.GetButtonDown("Start_" + players[1].conNumber))
                    {
                        Debug.Log("I PRESSED START PLEASE WORK");
                        ControlPanel.SetActive(false);
                    } else
                    {
                        return;
                    }
                    firstGame = false;
                }
                characterSelectBg.Stop();
                fightLevelBg.Play();
                Debug.Log(fightLevelBg.isPlaying);

                for (int i = 0; i < players.Count; i++)
                {
                    players[i].gameObject.transform.position = new Vector3(i * .7f, 0, 0);
                    players[i].gameObject.SetActive(true);
                }
                playing = true;
            }
        }
    }

    //One player can choose whatever.
    void SPcharSelect()
    {
        //Singleplayer Input
        if (players.Count > 0)
        {
            Character n01 = ninja[0];
            Character n02 = ninja[1];
            Character s01 = samurai[0];
            Character s02 = samurai[1];

            if (!Ninja01Panel.activeSelf && !Samurai01Panel.activeSelf &&
                !Ninja02Panel.activeSelf && !Samurai02Panel.activeSelf)
            {

                //If at full Hp, show full HP bar and Aggressive speech bubble
                if (n01.HP == 100)
                {
                    Ninja01Panel.SetActive(true);
                    ninja01HpFull.GetComponent<Image>().enabled = true;
                    ninja01Agg.GetComponent<Image>().enabled = true;
                }
                if (n02.HP == 100)
                {
                    Ninja02Panel.SetActive(true);
                    ninja02HpFull.GetComponent<Image>().enabled = true;
                    ninja02Agg.GetComponent<Image>().enabled = true;
                }
                if (s01.HP == 100)
                {
                    Samurai01Panel.SetActive(true);
                    Samurai01HpFull.GetComponent<Image>().enabled = true;
                    Samurai01Agg.GetComponent<Image>().enabled = true;
                }
                if (s02.HP == 100)
                {
                    Samurai02Panel.SetActive(true);
                    Samurai02HpFull.GetComponent<Image>().enabled = true;
                    Samurai02Agg.GetComponent<Image>().enabled = true;
                }

                //The winner of the round with more than 0 HP will show with half HP bar & passive speech bubble
                if (n01.HP > 0 && n01.HP < 100)
                {
                    Ninja01Panel.SetActive(false);
                    ninja01HpFull.GetComponent<Image>().enabled = false;
                    ninja01Agg.GetComponent<Image>().enabled = false;
                    firstNinja01.gameObject.SetActive(false);
                    ninja01HpEmpty.GetComponent<Image>().enabled = false;
                    ninja01Dead.GetComponent<Image>().enabled = false;

                    Ninja01Panel.SetActive(true);
                    ninja01HpHalf.GetComponent<Image>().enabled = true;
                    ninja01Pass.GetComponent<Image>().enabled = true;
                }
                if (n02.HP > 0 && n02.HP < 100)
                {
                    Ninja02Panel.SetActive(false);
                    ninja02HpFull.GetComponent<Image>().enabled = false;
                    ninja02Agg.GetComponent<Image>().enabled = false;
                    firstNinja02.gameObject.SetActive(false);
                    ninja02HpEmpty.GetComponent<Image>().enabled = false;
                    ninja02Dead.GetComponent<Image>().enabled = false;

                    Ninja02Panel.SetActive(true);
                    ninja02HpHalf.GetComponent<Image>().enabled = true;
                    ninja02Pass.GetComponent<Image>().enabled = true;
                }
                if (s01.HP > 0 && s01.HP < 100)
                {
                    Samurai01Panel.SetActive(false);
                    Samurai01HpFull.GetComponent<Image>().enabled = false;
                    Samurai01Agg.GetComponent<Image>().enabled = false;
                    firstSamurai01.gameObject.SetActive(false);
                    Samurai01HpEmpty.GetComponent<Image>().enabled = false;
                    Samurai01Dead.GetComponent<Image>().enabled = false;

                    Samurai01Panel.SetActive(true);
                    Samurai01HpHalf.GetComponent<Image>().enabled = true;
                    Samurai01Pass.GetComponent<Image>().enabled = true;
                }
                if (s02.HP > 0 && s02.HP < 100)
                {
                    Samurai02Panel.SetActive(false);
                    Samurai02HpFull.GetComponent<Image>().enabled = false;
                    Samurai02Agg.GetComponent<Image>().enabled = false;
                    firstSamurai02.gameObject.SetActive(false);
                    Samurai02HpEmpty.GetComponent<Image>().enabled = false;
                    Samurai02Dead.GetComponent<Image>().enabled = false;

                    Samurai02Panel.SetActive(true);
                    Samurai02HpHalf.GetComponent<Image>().enabled = true;
                    Samurai02Pass.GetComponent<Image>().enabled = true;
                }

                //The loser(0 Hp) will show with blackened screen and no text
                if (n01.HP <= 0)
                {
                    Ninja01Panel.SetActive(false);
                    ninja01HpFull.GetComponent<Image>().enabled = false;
                    ninja01Agg.GetComponent<Image>().enabled = false;
                    Ninja01Panel.SetActive(false);
                    ninja01HpHalf.GetComponent<Image>().enabled = false;
                    ninja01Pass.GetComponent<Image>().enabled = false;

                    ninja01DeadPanel.gameObject.SetActive(true);
                    ninja01DeadPanel.interactable = false;
                    ninja01HpEmpty.GetComponent<Image>().enabled = true;
                    ninja01Dead.GetComponent<Image>().enabled = true;
                }
                if (n02.HP <= 0)
                {
                    Ninja02Panel.SetActive(false);
                    ninja02HpFull.GetComponent<Image>().enabled = false;
                    ninja02Agg.GetComponent<Image>().enabled = false;
                    Ninja02Panel.SetActive(false);
                    ninja02HpHalf.GetComponent<Image>().enabled = false;
                    ninja02Pass.GetComponent<Image>().enabled = false;

                    ninja02DeadPanel.gameObject.SetActive(true);
                    ninja02DeadPanel.interactable = false;
                    ninja02HpEmpty.GetComponent<Image>().enabled = true;
                    ninja02Dead.GetComponent<Image>().enabled = true;
                }
                if (s01.HP <= 0)
                {
                    Samurai01Panel.SetActive(false);
                    Samurai01HpFull.GetComponent<Image>().enabled = false;
                    Samurai01Agg.GetComponent<Image>().enabled = false;
                    Samurai01Panel.SetActive(false);
                    Samurai01HpHalf.GetComponent<Image>().enabled = false;
                    Samurai01Pass.GetComponent<Image>().enabled = false;

                    Samurai01DeadPanel.gameObject.SetActive(true);
                    Samurai01DeadPanel.interactable = false;
                    Samurai01HpEmpty.GetComponent<Image>().enabled = true;
                    Samurai01Dead.GetComponent<Image>().enabled = true;
                }
                if (s02.HP <= 0)
                {
                    Samurai02Panel.SetActive(false);
                    Samurai02HpFull.GetComponent<Image>().enabled = false;
                    Samurai02Agg.GetComponent<Image>().enabled = false;
                    Samurai02Panel.SetActive(false);
                    Samurai02HpHalf.GetComponent<Image>().enabled = false;
                    Samurai02Pass.GetComponent<Image>().enabled = false;

                    Samurai02DeadPanel.gameObject.SetActive(true);
                    Samurai02DeadPanel.interactable = false;
                    Samurai02HpEmpty.GetComponent<Image>().enabled = true;
                    Samurai02Dead.GetComponent<Image>().enabled = true;
                }
                initialSelect();
            }
            if (!players[0].hasCharacter)
            {
                MenuText.text = "Player 1 choose your character!";
                setMenuInput(players[0].conNumber);

            }
            else if (players[0].hasCharacter)
            {
                CharPanels.SetActive(false);
                players[0].gameObject.SetActive(true);
                playing = true;
            }
        }
    }

    //Sets the inputModule to a specific controller
    void setMenuInput(int conNumber)
    {
        if (!inputModule.enabled) inputModule.enabled = true;
        inputModule.horizontalAxis = "L_XAxis_" + conNumber;
        inputModule.verticalAxis = "L_YAxis_" + conNumber;
        inputModule.submitButton = "A_" + conNumber;
        inputModule.cancelButton = "B_" + conNumber;
    }

    //Check if a new player has pressed start. Then make a player.
    void checkNewPlayer()
    {
        if (Input.GetButton("Start_1") && !playerExists(1) && players.Count < 2)
        {
            //print("Adding player.");
            player p = Instantiate(playerPrefab);
            p.conNumber = 1;
            p.gameObject.SetActive(false);
            players.Add(p);
            buttonPress.Play();
        }
        if (Input.GetButton("Start_2") && !playerExists(2) && players.Count < 2)
        {
            // print("Adding player.");
            player p = Instantiate(playerPrefab);
            p.conNumber = 2;
            p.gameObject.SetActive(false);
            players.Add(p);
            buttonPress.Play();
        }
        if (Input.GetButton("Start_3") && !playerExists(3) && players.Count < 2)
        {
            //print("Adding player.");
            player p = Instantiate(playerPrefab);
            p.conNumber = 3;
            p.gameObject.SetActive(false);
            players.Add(p);
        }
        if (Input.GetButton("Start_4") && !playerExists(4) && players.Count < 2)
        {
            //print("Adding player.");
            player p = Instantiate(playerPrefab);
            p.conNumber = 4;
            p.gameObject.SetActive(false);
            players.Add(p);
        }
        //print(Input.GetButton("Submit"));
        if (Input.GetButton("KB_Submit") && !playerExists(5) && players.Count < 2)
        {
            print("Adding player.");
            player p = Instantiate(playerPrefab);
            p.conNumber = 5;
            p.gameObject.SetActive(false);
            players.Add(p);
        }
    }

    //Check if a player exists.
    bool playerExists(int playerNum)
    {
        foreach (player p in players)
        {
            if (p.conNumber == playerNum) return true;
        }
        return false;
    }

    IEnumerator playerlose(int i)
    {
        if(i == 0)
        {
            player2wins.SetActive(true);
        }
        if(i == 1)
        {
            player1wins.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        player1wins.SetActive(false);
        player2wins.SetActive(false);
        CharPanels.SetActive(true);
        
        initialSelect();
    }
    //Open up Control Page
    void controllerPanel(int conNumber)
    {
        ControlPanel.SetActive(true);
        if (Input.GetButtonDown("Start_" + players[0].conNumber) || Input.GetButtonDown("Start_" + players[1].conNumber))
        {
            Debug.Log("I PRESSED START PLEASE WORK");
            ControlPanel.SetActive(false);
            characterSelectBg.Stop();
            fightLevelBg.Play();
        }
    }


}
