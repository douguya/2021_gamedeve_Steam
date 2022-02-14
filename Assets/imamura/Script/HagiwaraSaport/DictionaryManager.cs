using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    // Dictionary<string, int[]> dic3 = ,new  Dictionary<string, int[]>();
    public static Dictionary<string, int[]> ItemDictionary = new Dictionary<string, int[]>
    {
        //{"�A�C�e����",new  int [2] {�|�C���g,�A�C�e������}}
        //�A�C�e������
        //0 �S�[���A�C�e��
        //1 �H���i
        //2 ���̑�(���ޑ҂�)
        {"�S�[��",                new int [2]{5,0 }},
        {"��g��",                new int [2]{5,1 }},
        {"���^��",                new int [2]{5,1 }},
        {"�v����",                new int [2]{2,1 }},
        {"�n���o�[�K�[",          new int [2]{3,1 }},
        {"�L�O�d��",              new int [2]{7,2 }},
        {"�^��",                  new int [2]{4,2 }},
        {"������",                new int [2]{4,1 }},
        {"�K�g�[�V���R��",        new int [2]{3,1 }},
        {"���ڋ�",                new int [2]{3,2 }},
        {"�����@",                new int [2]{5,2 }},
        {"�r�L�j",                new int [2]{4,2 }},
        {"�g�����W�X�^",          new int [2]{5,2 }},
        {"�ΒY",                  new int [2]{3,2 }},
        {"�Z��",                  new int [2]{7,2 }},
        {"����",                  new int [2]{2,2 }},
        {"�~����",                new int [2]{1,1 }},
        {"����",                  new int [2]{3,1 }},
        {"�W���x�C�U���̐l�`",    new int [2]{6,2 }},
        {"�^�R�̑�",              new int [2]{1,1 }},
        {"�}���S�[",              new int [2]{2,1 }},
        {"�E�N����",              new int [2]{3,2 }},
        {"�n�C�G�C�g�`���R",      new int [2]{2,1 }},
        {"�n�b�s�[",              new int [2]{5,2 }},
        {"�a�َq�����",          new int [2]{3,1 }},
        {"�`�L�����",            new int [2]{2,1 }},
        {"�w��",                  new int [2]{2,1 }},
        {"���A�̃p���t���b�g",    new int [2]{5,2 }},
        {"�`�L�����[����",        new int [2]{2,1 }},
        {"�N���~�p��",            new int [2]{2,1 }},
        {"���ܗg��",            new int [2]{2,1 }},
        {"�W�F�b�g�R�[�X�^�[",    new int [2]{5,2 }},
        {"�X�q",                  new int [2]{3,2 }},
        {"�M���@",                new int [2]{3,2 }},
        {"���C���{�[�u���b�W",    new int [2]{1,2 }},
        {"�P",                    new int [2]{3,2 }},
        {"���i",                  new int [2]{5,1 }},
        {"�͂���",                new int [2]{5,2 }},
        {"�R",                    new int [2]{9,2 }},
        {"�p���c",                new int [2]{1,2 }},
        {"�W�F���[�g",            new int [2]{3,1 }},
        {"����",                  new int [2]{6,2 }},
        {"�I�����s�b�N�̋L�O�i",  new int [2]{5,2 }},
        {"���a�ȐS",              new int [2]{8,2 }},
        {"���k�b�W",              new int [2]{4,2 }},
        {"���V���`",              new int [2]{4,2 }},
        {"�x�[�g�[���F���̃Y��",  new int [2]{2,2 }},
        {"��H��",                new int [2]{3,1 }},
        {"���{�̎�ł����ǂ�",    new int [2]{4,1 }},
        {"��������ԉ�",          new int [2]{4,2 }},
        {"�ʉ�����",              new int [2]{8,2 }},
        {"�|�e�`",                new int [2]{3,2 }},
        {"���[���P�[�L",          new int [2]{3,1 }},
        {"�y�p�[�~���g",          new int [2]{1,1 }},
        {"�p�t�F",                new int [2]{3,1 }},
        {"�p�p",                  new int [2]{1,2 }},
        {"�p�C�i�b�v��",          new int [2]{3,1 }},
        {"��������",              new int [2]{3,2 }},
        {"�L�O�{�[��",            new int [2]{7,2 }},
        {"���уt���C",            new int [2]{3,1 }},
        {"�L�E�C",                new int [2]{3,1 }},
        {"�g�t",                  new int [2]{5,2 }},
        {"�n��",                  new int [2]{5,2 }},
        {"���{��",              new int [2]{8,2 }},
        {"�����x�b�h",            new int [2]{8,2 }},
        {"��",                    new int [2]{2,2 }},
        {"�p�\�R��",              new int [2]{3,2 }},
        {"�����t",                new int [2]{1,2 }},
        {"�ώ�",                  new int [2]{2,1 }},
        {"�����^���[�̃X�g���b�v",new int [2]{4,2 }},
        {"���",                  new int [2]{3,1 }},
        {"�C�`�S",                new int [2]{1,1 }},
        {"��z�s�U",              new int [2]{5,1 }},
        {"�J�u�g���V",            new int [2]{3,2 }},
        {"�w���N���X�I�I�J�u�g",  new int [2]{10,2}},
        {"�����������̕i",        new int [2]{3,1 }},
        {"�����X",                new int [2]{3,1 }},
        {"���i",                  new int [2]{9,2 }},
        {"�X�N���b�v",            new int [2]{1,2 }},
        {"�J�g�����[�Z�b�g",      new int [2]{5,2 }},
        {"�N���W�b�g�J�[�h",      new int [2]{7,2 }},

    };



    public static Dictionary<string, string[,]> DayEffectictDictionary = new Dictionary<string, string[,]>
    {
      /*
       * 
      {���t,new string[,]{
                     1��   { �L�O��, ���ʂ̎��,��ɓ���A�C�e����ύX����BGM�̖��O },
                     2��   {"�N���W�b�g�̓�","�A�C�e��" ,"�N���W�b�g�J�[�h"}
       }},
    �@ 
      ���ʂ̎�ނɂ���
      �@��ɓ���A�C�e����ω�����BGM�����������
      �@�@�A�C�e���@�A�C�e������ɓ��邾���̂���
    �@ �@ �A�C�R���@�A�C�R�����ς��
    �@ �@ BGM�@�@�@�@BGM���ς��

      �@���̑��@�@���ޕs�@�L�O�����Ƃ̕ϐ��쐬���]�܂�����

      */
        { "7/1",new string[,]{
                                {"�N���~�p���̓�", "�A�C�e��", "�X�N���b�v" },
                                {"�N���W�b�g�̓�","�A�C�e��" ,"�N���W�b�g�J�[�h"}
        }},
        {"7/2",new string[,]{
                                { "�^�R�̓�", "�A�C�e��", "�^�R�̑�" },
                                {"���ǂ�̓�","�A�C�e��" ,"���߂�"}
        }},





    };



}
