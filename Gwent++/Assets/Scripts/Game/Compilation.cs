using System.Collections;
using System.Collections.Generic;
using LogicalSide;
using TMPro;
using Compiler;
using UnityEngine;
using Unity.VisualScripting;

public class Compilation : MonoBehaviour
{
    public GameObject LittleErrors;
    public TextMeshProUGUI Console;
    [SerializeField] public List<UnityCard> CardsPlayer1;
    [SerializeField] public List<UnityCard> CardsPlayer2;

    public string FilePath= "C:/Pro/Gwent-Potterhead-/Gwent++/Assets/Scripts/Compilation/Tools/Example.txt";


    public void PathUpdate(GameObject s)
    {
        FilePath = s.GetComponent<TMPro.TMP_InputField>().text;
    }

    public void CloseConsole()
    {
        LittleErrors.SetActive(false);
        Console.text = "";
    }

    public int Player;

    public void SetPlayer(int player)
    {
        Player = player;
    }
    public void CompilerPortal()
    {
        List<UnityCard> cards = new List<UnityCard>();
        List<Card> cardscomp = new List<Card>();

        try
        {
            cardscomp = Compilator.GetCards(FilePath);
            
        }
        catch(System.Exception e) 
        {
            LittleErrors.SetActive(true);
            Console.text = e.Message;
        }
        if (Errors.List.Count >0)
        {
            foreach (var item in Errors.List)
            {
                Console.text += item.ToString() + "\n";
            }
            LittleErrors.SetActive(true);
        }
        if(Errors.List.Count == 0)
        {
            Console.text = "Compilacion exitosa";
            LittleErrors.SetActive(true);
        }

        bool b;
        if(Player == 1)
        {
            b = true;
            CardsPlayer1 = cards;
        }
        else{ b = false;
            CardsPlayer2 = cards;
        }

        foreach (var card in cardscomp)
        {
            if (card.Faction == "Gryffindor" || card.Faction == "Slytherin")
                cards.Add(GenerateCard(card, b));
            else
            {
                Console.text = "";
                Console.text += $"La faccion de la carta {card.Name} no existe, debe ser Gryffindor o Slytherin";
                LittleErrors.SetActive(true);
            }
                
        }

        Processor.ParamsRequiered.Clear();
        Processor.Effects.Clear();
    }

    private UnityCard GenerateCard(Card card, bool DownBoard)
    {
        #region Finding Out Type Unit
        KindofCard unit = KindofCard.None;
        Effect eff = Effect.None;
        string image = null;
        string Type="";

        switch (card.Type)
        {
            case "Clima":
                Type = "C";
                eff = Effect.Weather;
                image = "Weather";
                break;
            case "Plata":
                unit = KindofCard.Silver;
                Type = "U";
                image = "Unit";
                break;
            case "Oro":
                unit = KindofCard.Golden;
                Type = "U";
                image = "Gold unit";
                break;
            case "Aumento":
                unit = KindofCard.None;
                foreach(char c in card.Range)
                {
                    Type += "A"+c;
                }
                eff = Effect.Raise;
                image = "Increasement";
                break;
            case "Lider":
                Type = "L";
                image = "Ravenclaw leader";
                break;
            case "Despeje":
                Type = "C";
                eff = Effect.Cleaner;
                image = "Clearance";
                break;
        }


        #endregion
        UnityCard UnityCard = new(DownBoard, card.Name, card.Power, null, unit, Type, eff, card.Range, Resources.Load<Sprite>(image), $"Carta de tipo {card.Type} Compilada");
        UnityCard.Effects= card.Effects;
        UnityCard.Faction= card.Faction;
        return UnityCard;
    }

}