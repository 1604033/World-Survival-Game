using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggingCampPopup : MonoBehaviour
{
    public class Dialog
    {
        public string Title =  "Logging camp";
        public string Message = "Generates logs from trees";
        public string Output = "Output 1 wood after 3s";
        public string Input = "The current input stock is 0";
    }

    private Dialog dialog = new Dialog();

    
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text titleTextUI;
    [SerializeField] private Text messageTextUI;
    [SerializeField] private Text outputTextUI;
    [SerializeField] private Text inputTextUI;
    [SerializeField] private List<PokemonDisplay> _pokemonDisplay;
    [SerializeField] private List<PokemonDisplay> assignPokemonDisplay;
    private LoggingCamp _loggingCamp;
    
    [SerializeField] private Button closeButton;
    [SerializeField] private Button closeButtonAssignPopup;
    
    [SerializeField] private Button assignKreechu1;
    [SerializeField] private Button assignKreechu2;

    [SerializeField] private List<Button> assignButtons; 

    [SerializeField] private GameObject baseLoggingPopup;
    [SerializeField] private GameObject assignKreechuPopup;
    private void Awake()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide); 
        
        closeButtonAssignPopup.onClick.RemoveAllListeners();
        closeButtonAssignPopup.onClick.AddListener(HideAssignPopup); 
        
        assignKreechu2.onClick.RemoveAllListeners();
        assignKreechu2.onClick.AddListener(ShowAssignPopup);
        
        assignKreechu1.onClick.RemoveAllListeners();
        assignKreechu1.onClick.AddListener(ShowAssignPopup);

    }

    

    public LoggingCampPopup SetTitleAndDescription(string title,string message)
    {
       dialog.Title = title;
        dialog.Message = message;
        return this;
    } 
    
    public LoggingCampPopup SetInputAndOutputAndLoggingCamp(string input, string output , LoggingCamp loggingCamp)
    {
        dialog.Input =  input;
        dialog.Output =  output;
        _loggingCamp = loggingCamp;
        return this;
    }
    
    public LoggingCampPopup SetPokemonsDisplay(List<Pokemon> pokemons)
    {

        for (int i = 0; i < _pokemonDisplay.Count && i < pokemons.Count; i++)
        {
            Pokemon pokemon = pokemons[i];
            PokemonDisplay display = _pokemonDisplay[i];
            display.SetDisplay(pokemon.name, pokemon.icon,pokemon);
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

    public void ShowAssignPopup()
    {
       baseLoggingPopup.gameObject.SetActive(false); 
       assignKreechuPopup.gameObject.SetActive(true); 
        SetAssignPokemonsDisplay();
    }
    private void HideAssignPopup()
    {
        assignKreechuPopup.gameObject.SetActive(false);
        baseLoggingPopup.gameObject.SetActive(true);
        SetPokemonsDisplay(_loggingCamp.assignedPokemons);
    }
    public void SetAssignPokemonsDisplay()
    {
        List<Pokemon> pokemons = AnimalsManager.Instance.GetAllPokemons();
        for (int i = 0; i < assignPokemonDisplay.Count && i < pokemons.Count; i++)
        {
            Pokemon pokemon = pokemons[i];
            PokemonDisplay display = assignPokemonDisplay[i];
            display.SetDisplay(pokemon.name, pokemon.icon,pokemon);
        }
    }
    public void AssignPokemon(PokemonDisplay pokemonDisplay)
    {
        if (_loggingCamp != null)
            if (pokemonDisplay.pokemon != null)
            {
                Debug.Log(pokemonDisplay);
                _loggingCamp.AssignPokemon(pokemonDisplay.pokemon);
                HideAssignPopup();
            }
        
    }
    
}
