﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int maskCount = 0;
    public int syringeCount = 0;
    public int livesLeft = 0;
    public int coinsCollectedPerGame = 0;
    public DateTime gameStartTime;
    public bool isDoctor = false;
    public string role = "HUMAN";
    public static GameManager inst;
    public Text scoreText;
    public Text maskText;
    public Text syringeText;
    public Text roleText;
    public Text livesText;
    public Text guiText;
    public Text flashText;
    public MovePlayer playerMovement;
    public DateTime superManEffectStamp = DateTime.MinValue;
    public int DOCTOR_POWER_POINT = 10;
    public int doctorModePoints = 0;
    public bool gameStart = true;
    public int docMasks;
    public int docSyringes;
    public DateTime docStartTime;
    public int docTimeSeconds = 0;
    public int cumulativeDocPoints = 0;
    public int cumulativeSupermanPoints = 0;
    public int supermanCount = 0;
    public int pointsPerLife = 0;

    public void IncrementScore()
    {
        GameManager.inst.pointsPerLife++;
        if (DateTime.Now <= GameManager.inst.superManEffectStamp)
        {
            GameManager.inst.cumulativeSupermanPoints++;
        }

        if (isDoctor)
        {
            GameManager.inst.doctorModePoints++;
        }

        emptyMessage();
        score++;
        //TODO: change point divisor to 100
        playerMovement.speed = playerMovement.startSpeed + (score / 100) * playerMovement.speedIncreasePer100Points;
        scoreText.text = "Score: " + score;
    }

    public void IncrementScore(int value)
    {
        GameManager.inst.pointsPerLife += value;
        if (DateTime.Now <= GameManager.inst.superManEffectStamp)
        {
            GameManager.inst.cumulativeSupermanPoints += value;
        }

        if(isDoctor)
        {
            GameManager.inst.doctorModePoints += value;
        }

        emptyMessage();
        score+= value;
        //TODO: change point divisor to 100
        playerMovement.speed = playerMovement.startSpeed + (score / 100) * playerMovement.speedIncreasePer100Points;
        scoreText.text = "Score: " + score;
    }

    public void DecrementScore(int value)
    {
        GameManager.inst.pointsPerLife -= value;
        if (DateTime.Now <= GameManager.inst.superManEffectStamp)
        {
            GameManager.inst.cumulativeSupermanPoints -= value;
        }

        if (isDoctor)
        {
            GameManager.inst.doctorModePoints -= value;
        }

        if (value < score)
        {
            score -= value;
        }
        else
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
    }

    public IEnumerator SetSuperManStamp()
    {
        
        flashText.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        flashText.text = "SUPERMAN DRIVE ACTIVE!";
        flashText.gameObject.SetActive(false);
        GameManager.inst.supermanCount++;
        superManEffectStamp = DateTime.Now.AddSeconds(7);
    }

    public DateTime GetSuperManStamp()
    {
        return superManEffectStamp;
    }

    public void IncrementMask()
    {
        emptyMessage();
        maskCount++;
        
        maskText.text = "Masks: " + maskCount;
    }

    public void IncrementMask(int value)
    {
        emptyMessage();
        maskCount+=value;

        maskText.text = "Masks: " + maskCount;
    }

    public bool DecrementMask()
    {
        if (maskCount > 0)
            maskCount--;
        else
            return false;

        maskText.text = "Masks: " + maskCount;

        if (maskCount > 0) return true;
        else return false;
    }

    public void DecrementMask(int value)
    {
        emptyMessage();
        if (value < maskCount)
        {
            maskCount -= value;
        }
        else
        {
            maskCount = 0;
            if (isDoctor) {
                ChangeRole();
            }
        }

        maskText.text = "Masks: " + maskCount;
    }

    public void IncrementSyringe()
    {
        emptyMessage();
        syringeCount++;

        syringeText.text = "Syringes: " + syringeCount;
    }

    public void IncrementSyringe(int value)
    {
        emptyMessage();
        syringeCount+=value;

        syringeText.text = "Syringes: " + syringeCount;
    }

    public bool DecrementSyringe()
    {
        if (syringeCount > 0)
            syringeCount--;
        else
            return false;

        syringeText.text = "Syringes: " + syringeCount;
        if (syringeCount > 0) return true;
        else return false;
    }

    public void DecrementSyringe(int value)
    {
        emptyMessage();
        if (value < syringeCount)
        {
            syringeCount -= value;
        }
        else
        {
            syringeCount = 0;
            if (isDoctor) {
                ChangeRole();
            }
        }

        syringeText.text = "Syringes: " + syringeCount;
    }

    public void IncrementLives()
    {
        emptyMessage();
        if (livesLeft<3)
            livesLeft++;

        livesText.text = "Lives left: " + livesLeft;
    }

    public void emptyMessage()
    {
        guiText.text = "";
    }

    public bool DecrementLives()
    {
        Analytics.CustomEvent("Life Lost", new Dictionary<string, object>
              {
                { "Lives remaining", GameManager.inst.livesLeft },
                { "Points Earned", GameManager.inst.pointsPerLife },
              });

        if (livesLeft > 1)
            livesLeft--;
        else
            return false;
        
        GameManager.inst.pointsPerLife = 0;
        livesText.text = "Lives left: " + livesLeft;
        return true;
    }

    public void ChangeRole()
    {
        isDoctor = !isDoctor;
        if (isDoctor)
        {
            role = "DOCTOR";
            docStartTime = DateTime.Now;
            Analytics.CustomEvent("Doctor switch", new Dictionary<string, object>
              {
                { "mask", GameManager.inst.maskCount },
                { "syringes", GameManager.inst.syringeCount },
              });
            docMasks = GameManager.inst.maskCount;
            docSyringes = GameManager.inst.syringeCount;
        } else
        {
            if (gameStart)
            {
                gameStart = !gameStart;
            }
            else
            {
                DateTime docEndTime = DateTime.Now;
                int seconds = (int)System.Math.Abs((GameManager.inst.docStartTime - docEndTime).TotalSeconds);
                GameManager.inst.docTimeSeconds += seconds;

                Analytics.CustomEvent("Human switch", new Dictionary<string, object>
                  {
                    { "mask", GameManager.inst.docMasks },
                    { "syringes", GameManager.inst.docSyringes },
                    { "points", GameManager.inst.doctorModePoints }
                  });
                GameManager.inst.cumulativeDocPoints += GameManager.inst.doctorModePoints;
                GameManager.inst.doctorModePoints = 0;
                GameManager.inst.docMasks = 0;
                GameManager.inst.docSyringes = 0;
            }
            role = "HUMAN";
        }

        SetMessage("Role changed to "+role);
        roleText.text = "Role: " + role;
    }

    public void SetMessage(string message)
    {
        guiText.text = message;
    }

    private void Awake()
    {
        inst = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
