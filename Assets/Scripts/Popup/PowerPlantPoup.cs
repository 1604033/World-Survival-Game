using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlantPoup : MonoBehaviour
{
    public class Dialog
    {
        public string Title = "Power Plant";
        public string Message = "Generates power ";
        public string Output = "Output 10 Wh";
        public string Input = "The current input stock is 0";
    }

    private Dialog dialog = new Dialog();


    [SerializeField] private GameObject canvas;
    [SerializeField] private Text titleTextUI;
    [SerializeField] private Text messageTextUI;
    [SerializeField] private Text outputTextUI;
    [SerializeField] private Text inputTextUI;
    [SerializeField] private List<PokemonDisplay> _pokemonDisplay;
    [SerializeField] private List<PokemonDisplay> _pokemonDisplayNight;
    [SerializeField] private List<PokemonDisplay> assignPokemonDisplay;
    [SerializeField] private PowerPlant powerPlant;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button closeButtonAssignPopup;

    [SerializeField] private Button assignBeast1Day;
    [SerializeField] private Button assignBeast2Day;
    [SerializeField] private Button assignBeast1Night;
    [SerializeField] private Button assignBeast2Night;

    DayTimeController.WorkingShifts currentAssignShift;

    [SerializeField] private List<Button> assignButtons;

    [SerializeField] private GameObject basePowerPlantPopup;
    [SerializeField] private GameObject assignKreechuPopup;

    private void Awake()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);

        closeButtonAssignPopup.onClick.RemoveAllListeners();
        closeButtonAssignPopup.onClick.AddListener(HideAssignPopup);

        assignBeast1Day.onClick.RemoveAllListeners();
        assignBeast2Day.onClick.RemoveAllListeners();
        assignBeast1Night.onClick.RemoveAllListeners();
        assignBeast2Night.onClick.RemoveAllListeners();

        assignBeast1Day.onClick.AddListener(ShowAssignPopupDay);
        assignBeast2Day.onClick.AddListener(ShowAssignPopupDay);
        assignBeast1Night.onClick.AddListener(ShowAssignPopupNight);
        assignBeast2Night.onClick.AddListener(ShowAssignPopupNight);
    }


    public PowerPlantPoup SetTitleAndDescription(string title, string message)
    {
        dialog.Title = title;
        dialog.Message = message;
        return this;
    }

    // public LoggingCampPopup SetInputAndOutputAndLogging(string input, string output , LoggingCamp loggingCamp)
    // {
    //     dialog.Input =  input;
    //     dialog.Output =  output;
    //     _loggingCamp = loggingCamp;
    //     return this;
    // }

    public PowerPlantPoup SetPokemonsDisplay(List<Pokemon> pokemons, List<Pokemon> nightBeasts)
    {
        for (int i = 0; i < _pokemonDisplay.Count && i < pokemons.Count; i++)
        {
            Pokemon pokemon = pokemons[i];
            PokemonDisplay display = _pokemonDisplay[i];
            display.SetDisplay(pokemon.name, pokemon.icon, pokemon);
            if (display.gameObject.TryGetComponent(out Button button))
            {
                button.enabled = false;
            }
        }

        for (int i = 0; i < _pokemonDisplayNight.Count && i < nightBeasts.Count; i++)
        {
            Pokemon pokemon = nightBeasts[i];
            PokemonDisplay display = _pokemonDisplayNight[i];
            display.SetDisplay(pokemon.name, pokemon.icon, pokemon);
            if (display.gameObject.TryGetComponent(out Button button))
            {
                button.enabled = false;
            }
        }

        return this;
    }

    public void Show()
    {
        titleTextUI.text = dialog.Title;
        messageTextUI.text = dialog.Message;
        inputTextUI.text = dialog.Input;
        outputTextUI.text = dialog.Output;
        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
        dialog = new Dialog();
    }

    public void ShowAssignPopupNight()
    {
        basePowerPlantPopup.gameObject.SetActive(false);
        assignKreechuPopup.gameObject.SetActive(true);
        SetAssignPokemonsDisplay(DayTimeController.WorkingShifts.Night);
    }

    public void ShowAssignPopupDay()
    {
        basePowerPlantPopup.gameObject.SetActive(false);
        assignKreechuPopup.gameObject.SetActive(true);
        SetAssignPokemonsDisplay(DayTimeController.WorkingShifts.Day);
    }

    private void HideAssignPopup()
    {
        assignKreechuPopup.gameObject.SetActive(false);
        basePowerPlantPopup.gameObject.SetActive(true);
        if (powerPlant != null) SetPokemonsDisplay(powerPlant.assignedPokemons, powerPlant.assignedPokemonsNight);
    }

    public void SetAssignPokemonsDisplay(DayTimeController.WorkingShifts shift)
    {
        if (shift == DayTimeController.WorkingShifts.Day)
        {
            List<Pokemon> pokemons = AnimalsManager.Instance.GetAllPokemons();
            List<Pokemon> pokemonsNeeded = pokemons
                .Where(pokemon =>
                    pokemon != null &&
                    pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.WorkAtDay) &&
                    (pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.Water) ||
                     pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.Heat)))
                .ToList();

            for (int i = 0; i < assignPokemonDisplay.Count && i < pokemonsNeeded.Count; i++)
            {
                Pokemon pokemon = pokemonsNeeded[i];
                PokemonDisplay display = assignPokemonDisplay[i];
                display.SetDisplay(pokemon.name, pokemon.icon, pokemon);
            }
        }

        if (shift == DayTimeController.WorkingShifts.Night)
        {
            List<Pokemon> pokemons = AnimalsManager.Instance.GetAllPokemons();
            List<Pokemon> pokemonsNeeded = pokemons
                .Where(pokemon =>
                    pokemon != null &&
                    pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.WorkAtNight) &&
                    (pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.Water) ||
                     pokemon.beastAbilites.Contains(Pokemon.BeastAbilities.Heat)))
                .ToList();

            for (int i = 0; i < assignPokemonDisplay.Count && i < pokemonsNeeded.Count; i++)
            {
                Pokemon pokemon = pokemonsNeeded[i];
                PokemonDisplay display = assignPokemonDisplay[i];
                display.SetDisplay(pokemon.name, pokemon.icon, pokemon);
            }
        }
    }

    public void AssignPokemon(PokemonDisplay pokemonDisplay)
    {
        if (powerPlant != null)
            if (pokemonDisplay.pokemon != null)
            {
                powerPlant.AssignPokemon(pokemonDisplay.pokemon, pokemonDisplay.pokemon.beastAbilites);
                HideAssignPopup();
            }
    }
}