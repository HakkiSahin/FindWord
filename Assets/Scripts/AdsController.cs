using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public static AdsController Instance;
    public RewardState State { get; set; }



    bool x2 = false;
    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool ReadyTransition()
    {
        return true;
    }

    public bool ReadyReward()
    {
        return true;
    }

    private void Awake()
    {
        Singleton();
    }


    public void ShowTransition()
    {
        Debug.Log("Ge�i� reklam� g�sterildi.");
    }



    public bool ShowReward(RewardState state)
    {
        Debug.Log("�d�l reklam� g�sterildi.");
        State = state;

        return RewardComplated(); // Oyun yay�nlanaca�� zaman buradan kesip �d�ll� reklam izlendikten sonra �al��an metotun i�ine yaz�lmal�
    }

    bool RewardComplated()
    {
        switch (State)
        {
            case RewardState.X2:
                return true;

            case RewardState.Restart:
                return true;

            case RewardState.AddMoney:
                return true;

            default:
                return false;
        }
    }





}

public enum RewardState
{
    X2,
    Restart,
    AddMoney
}
