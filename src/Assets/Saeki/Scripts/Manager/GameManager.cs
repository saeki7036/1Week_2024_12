using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using unityroom.Api;
public class GameManager : MonoBehaviour
{
    //static�p�����[�^���g�p
    static GameObject playerObject;

    //�v���C���[�̃I�u�W�F�N�g�����L���b�V��
    public static GameObject Getplayer => playerObject;

    static int mainGameScore;
    public int GetScore => mainGameScore;
    public static void AddScore(int add) => mainGameScore += add;
    enum GameState
    {
        Before,
        MainGame,
        Clear,
        GameOver
    }
    [SerializeField]
    GameState gameState;
   
    public bool IsMainGameState => gameState == GameState.MainGame;
    public bool IsGameClear => gameState == GameState.Clear;
    public bool IsGameOver => gameState == GameState.GameOver;

    [SerializeField]
    Player playerScript;

    [SerializeField]
    GameObject finishAnimartionObject;

    [SerializeField] 
    TextMeshProUGUI scoreText;
    [SerializeField] 
    TextMeshProUGUI clearScoreText;

    bool bossDead;

    //true�ɐݒ肷���UnityRoom�̃����L���O�@�\�𗘗p
    [SerializeField] 
    bool sendScoreFlag = false;

    public void BosskillFlag() => bossDead = true; 

    void Start()
    {
        mainGameScore = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        gameState = GameState.MainGame;
        bossDead = false;
    }

     
    
    void FixedUpdate()
    {
        //�X�R�AText��
        scoreText.text = mainGameScore.ToString();

        //�Q�[���t���[�ύX
        gameState = stateChange();

        //�Q�[���N���A��
        if (IsGameClear) 
            PlayFinishAnimation();
    }

    void PlayFinishAnimation()
    {
        //�N���A���A�j���[�V�����̃I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ύX
        if(finishAnimartionObject.activeSelf == false)
            finishAnimartionObject.SetActive(true);
    }

    GameState stateChange()
    {
        //���C���Q�[���ȊO�Ȃ猻�݂̃Q�[���t���[���ێ�
        if (gameState != GameState.MainGame)
            return gameState;

        //�v���C���[���S
        if(playerScript.IsDard)
            return GameState.GameOver;

        //�{�X���S
        if (bossDead)
        {
            //�N���A���̃X�R�A�ōX�V
            clearScoreText.text = mainGameScore.ToString();

            //Unityroom��Score���M�p(�n�C�X�R�A��)
            if(sendScoreFlag)
                UnityroomApiClient.Instance.SendScore(1, mainGameScore, ScoreboardWriteMode.HighScoreDesc);

            return GameState.Clear;
        }
           
        return GameState.MainGame;
    }
}
