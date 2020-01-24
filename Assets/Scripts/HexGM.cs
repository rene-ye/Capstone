using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class HexGM : Photon.MonoBehaviour
{
    public const float ROUND_TIME = 15;
    public const float INTERMISSION_TIME = 5;
    public const string TIMER_KEY = "RoundTimeBegin";
    public const string UNITS_KEY = "FieldUnits";
    public Player hexPlayer;


    private static PhotonView pView;
    private static int round = 0; // 0 for buy, 1 for intermission, 2 for battle
    private static double roundTimeLeft = 0;
    private string lastPlayerFought = "";
    private double startTime = 0;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Hashtable hash = new Hashtable();
            startTime = PhotonNetwork.time;
            hash.Add(TIMER_KEY, startTime);
            PhotonNetwork.room.SetCustomProperties(hash);
            roundTimeLeft = ROUND_TIME - ((PhotonNetwork.time - startTime) / 1000);
        } else
        {
            roundTimeLeft = ROUND_TIME;
        }
    }

    void Update()
    {
        roundTimeLeft -= Time.deltaTime;
        if (roundTimeLeft <= 0)
        {
            switch (round)
            {
                // Shopping
                case 0:
                    UpdateUnits();
                    getNextOpponentName();
                    syncTimer();
                    round++;
                    break;
                //Intermission
                case 1:
                    roundTimeLeft = ROUND_TIME;
                    GetOpponentUnits(lastPlayerFought);
                    round++;
                    break;
                // Battle
                case 2:
                    roundTimeLeft = ROUND_TIME;
                    round = 0;
                    break;
                default:
                    break;
            }
            hexPlayer.switchRounds(round);
        }
    }

    private void syncTimer()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Hashtable hash = new Hashtable();
            hash.Add(TIMER_KEY, PhotonNetwork.time);
            PhotonNetwork.room.SetCustomProperties(hash);
        }
        startTime = (double)PhotonNetwork.room.CustomProperties[TIMER_KEY];
        roundTimeLeft = INTERMISSION_TIME - ((PhotonNetwork.time - startTime) / 1000);
        Debug.Log(roundTimeLeft);
    }

    private void UpdateTimer()
    {
        // Only the master can update timer
        roundTimeLeft -= Time.deltaTime;
        Hashtable hash = PhotonNetwork.room.CustomProperties;
        hash.Remove(TIMER_KEY);
        hash.Add(TIMER_KEY, roundTimeLeft);
        PhotonNetwork.room.SetCustomProperties(hash);
    }

    private void UpdateUnits()
    {
        hexPlayer.lockBoard();
        Hashtable hash = PhotonNetwork.player.CustomProperties;
        if (hash.ContainsKey(UNITS_KEY)) {
            hash.Remove(UNITS_KEY);
        }
        hash.Add(UNITS_KEY, UnitInfoToByteArray(hexPlayer.currentUnits));
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    private byte[] UnitInfoToByteArray(UnitInfo[] uInfo)
    {
        var units = new MemoryStream();
        foreach (UnitInfo u in uInfo)
        {
            byte[] unit_name = Encoding.ASCII.GetBytes(u.unit_name);

            units.WriteByte((byte)unit_name.Length);
            units.Write(unit_name ,0, unit_name.Length);
            units.WriteByte((byte)u.unit_tier);
            units.WriteByte((byte)u.locationX);
            units.WriteByte((byte)u.locationY);
        }
        return units.ToArray();
    }
    private UnitInfo[] ByteArrayToUnitInfo(byte[] b)
    {
        List<UnitInfo> infoList = new List<UnitInfo>();
        var byteStream = new MemoryStream(b);
        while (byteStream.Position < byteStream.Length)
        {
            int nameLength = byteStream.ReadByte();
            byte[] nameBuffer = new byte[nameLength];
            byteStream.Read(nameBuffer, 0, nameLength);
            string name = Encoding.ASCII.GetString(nameBuffer);
            int tier = byteStream.ReadByte();
            int x = byteStream.ReadByte();
            int y = byteStream.ReadByte();
            infoList.Add(new UnitInfo(name, tier, x, y));
        }
        return infoList.ToArray();
    }

    private void GetOpponentUnits(string nickname)
    {
        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            if (p.NickName.Equals(nickname))
            {
                byte[] b = (byte[])p.CustomProperties[UNITS_KEY];
                if (b != null)
                {
                    UnitInfo[] u = ByteArrayToUnitInfo(b);
                    hexPlayer.setEnemyBoard(u);
                }
                return;
            }
        }
    }

    public static bool isShoppingRound()
    {
        return round == 0;
    }

    public static bool isBattleRound()
    {
        return round == 2;
    }

    public static double getRoundTimer()
    {
        return roundTimeLeft;
    }

    private void getNextOpponentName()
    {
        string opponent = "";
        PhotonPlayer[] list = PhotonNetwork.playerList;
        if (list.Length > 2)
        {
            do
            {
                opponent = list[Random.Range(0, list.Length)].NickName;
            } while (opponent.Equals(PhotonNetwork.playerName) || opponent.Equals(lastPlayerFought));
        } else
        {
            foreach (PhotonPlayer p in list)
            {
                if (!p.NickName.Equals(PhotonNetwork.playerName))
                    opponent = p.NickName;
            }
        }
        lastPlayerFought = opponent;
    }
}
