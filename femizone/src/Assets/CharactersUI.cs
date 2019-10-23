using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersUI : MonoBehaviour
{
    public CharacterUI[] all;

    void Start()
    {
        int id = 1;
        foreach (CharacterUI cui in all)
        {
            cui.Init(id);
            id++;
        }
        Events.OnAddScore += OnAddScore;
        Events.OnKeyPress += OnKeyPress;
        Events.OnHeroHitted += OnHeroHitted;
        Events.GrabPowerUp += GrabPowerUp;
    }
    void OnDestroy()
    {
        Events.OnAddScore -= OnAddScore;
        Events.OnKeyPress -= OnKeyPress;
        Events.OnHeroHitted -= OnHeroHitted;
        Events.GrabPowerUp -= GrabPowerUp;
    }
    void OnAddScore(int characterID, int score)
    {
        GetUI(characterID).SetScore(score);
    }

    void OnKeyPress(int characterID)
    {
        if (World.Instance.state == World.states.GAME_OVER)
            return;
        CharacterUI cui = GetUI(characterID);
        if (cui.id == characterID && (
            cui.state == CharacterUI.states.WAITING ||
             (cui.state == CharacterUI.states.DEAD &&
            UI.Instance.lifesManager.GetLifes()>0))
            )
        {
            cui.SetState(CharacterUI.states.PLAYING);
            Events.AddHero(characterID);
        }
    }
    void OnHeroHitted(int characterID, float force)
    {
        CharacterUI cui = GetUI(characterID);
        cui.OnHeroHitted(force);
    }
    void GrabPowerUp(Hero hero, Powerup powerup)
    {
        CharacterUI cui = GetUI(hero.id);
        cui.GrabPowerUp(powerup);
    }
    CharacterUI GetUI(int characterID)
    {
        foreach (CharacterUI cui in all)
            if (cui.id == characterID)
                return cui;
        return null;
    }
    public CharacterUI GetHiscoreCharacterUI()
    {
        CharacterUI cuiWinner = all[0];
        foreach (CharacterUI cui in all)
            if (cui.score > cuiWinner.score)
                cuiWinner = cui;
        return cuiWinner;
    }

}
