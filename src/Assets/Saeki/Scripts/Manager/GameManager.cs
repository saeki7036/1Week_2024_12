using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

public class GameManager : MonoBehaviour
{
    //static�p�����[�^���g�p
    //�v���C���[�̃I�u�W�F�N�g�����L���b�V��
    static GameObject playerObject;

    //Enemy��Bullet�̌v�Z�Ɏg�p
    public static GameObject Getplayer => playerObject;

    //static�p�����[�^���g�p
    //�X�R�A�Ǘ��ϐ�
    static int mainGameScore;
   
    //�X�R�A���Z
    public static void AddScore(int add) => mainGameScore += add;

    enum GameState//�Q�[���X�e�[�g
    {
        Before,
        MainGame,
        Clear,
        GameOver
    }

    [SerializeField]
    GameState gameState;//�Q�[���X�e�[�g�Ǘ��ϐ�

    public bool IsMainGameState => gameState == GameState.MainGame;//���C���Q�[���̃t���O����

    public bool IsGameOver => gameState == GameState.GameOver;//�Q�[���I�[�o�[�̃t���O����

     bool IsGameClear => gameState == GameState.Clear;//�N���A�̃t���O����

    [SerializeField]
    Player playerScript;//�v���C���[�̃N���X

    [SerializeField]
    GameObject finishAnimartionObject;//�N���A���̃A�j���[�V�����̃I�u�W�F�N�g

    [SerializeField] 
    TextMeshProUGUI scoreText;//�X�R�A�\���e�L�X�g

    [SerializeField] 
    TextMeshProUGUI clearScoreText;//�N���A���̃X�R�A�\���e�L�X�g

    //true�ɐݒ肷���UnityRoom�̃����L���O�@�\�𗘗p
    [SerializeField] 
    bool sendScoreFlag = false;

    bool bossDead;//�{�X�̎��S�t���O

    public void BosskillFlag() => bossDead = true; //�{�X�̎��S�t���O�X�V

    void Start()
    {
        //�X�R�A������
        mainGameScore = 0;

        //Player���L���b�V��
        playerObject = GameObject.FindGameObjectWithTag("Player");

        //�Q�[���X�e�[�g������
        gameState = GameState.MainGame;

        //�{�X�̎��S�t���O������
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
            PlayFinishAnimation();//�A�j���[�V�����N��
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

            //�N���A���
            return GameState.Clear;
        }

        //����ȊO�̓��C���Q�[�����
        return GameState.MainGame;
    }
}
