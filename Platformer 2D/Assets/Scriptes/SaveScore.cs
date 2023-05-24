using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class SaveScore : MonoBehaviour
{
    public static int score;
    private static int loadedScore;
    private static int[] scores;
    private bool isEmpty;
    public Text scoreText;
    public static SaveScore Instance;
    private void Start()
    {
        Instance = this;
        scores = new int[5];
    }
    public void CheckScore() 
    {
        LoadGameScore();
        if (isEmpty)
            for (int i = 0; i < scores.Length; i++)
                scores[i] = 0;
        loadedScore = scores[4];
        Debug.Log("score " + score);
        if (score > loadedScore)
        {
            scores[4] = score;
            Debug.Log("score > loadedScore");
            int i = 4;
            while(i>0)
            {
                Debug.Log(scores[i] + ":"+ scores[i - 1]);
                if (scores[i] > scores[i - 1])
                    Swap(ref scores[i], ref scores[i-1]);
                else break;
                i--;
            }
            SaveGameScore();
        }
    }
    private void Swap(ref int a, ref int b)
    {
        int n = a;
        a = b;
        b = n;
    }
    private void SaveGameScore()
    {
        BinaryFormatter bF = new BinaryFormatter();//������ ����������� ��� ������������/��������������, �������� �� ������� ���� � ����� �������� ������
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedData.dat");//������ �������� ���� �� ������������ ������ ��� ���������������� ������
        SavedData data = new SavedData();//������ ��������� ������ 
        data.savedScore = scores;//���������� � ���� ������, ����������� � ����������
        Debug.Log("Save" + scores[0]);
        Debug.Log("SaveData" + data.savedScore[0]);
        bF.Serialize(file, data);//����������� ������ ���������� � ���������� � ����
        file.Close();
        Debug.Log("Data Saved!!");
    }
   private void LoadGameScore()
    {
        if(File.Exists(Application.persistentDataPath+"/MySavedData.dat"))
        {
            BinaryFormatter bF = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedData.dat", FileMode.Open);
            SavedData data = (SavedData)bF.Deserialize(file);
            file.Close();
            scores = data.savedScore;
            Debug.Log("Load" + scores[0]);
            Debug.Log("LoadData" + data.savedScore[0]);
            Debug.Log("Game Data Loaded!!");
            isEmpty = false;
        }
        else
        {
            isEmpty = true;
        }
    }
   public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedData.dat"))
        {
            BinaryFormatter bF = new BinaryFormatter();
          File.Delete(Application.persistentDataPath + "/MySavedData.dat");
            Array.Clear(scores,0,5);
            scoreText.text = "Lets play to have records!";
            //scoreText.text = scoreToSave.ToString();
            Debug.Log("Data Reset Complete!!");
        }
        //else
        //{
        //    Debug.LogError("NoSavedDataToDelete");
        //}
    }
    public void Print()
    {
        LoadGameScore();
        int i = 1;
        if(scores[0]==0)
            scoreText.text = "Lets play to have records!";
        else
        foreach (int s in scores)
        {
                if (s == 0)
                    break;
            scoreText.text +=i.ToString()+ ". "+ s.ToString()+ "\n";
                i++;
        }
    }
}