using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildingPokemonPopup : BasePopup
{
    public List<PokemonDisplay> PokemonDisplays = new List<PokemonDisplay>();
      public class Dialog
        {
            public string Title =  "Title";
            public string Description = "You are now view the popup message";
        }
    
        private Dialog dialog = new Dialog();
    
        //public Action OnCancelOrDemolishClick;
        
        [SerializeField] private GameObject canvas;
        [SerializeField] private Text titleTextUI;
        [SerializeField] private Text messageTextUI;
        [SerializeField] private Button closeButton;
    
        private void Awake()
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(Hide);
        }
    
        public BuildingPokemonPopup SetTitle(string title)
        {
            Debug.Log("Called the inherit function");
            dialog.Title = title;
            return this;
        } 
        public BuildingPokemonPopup SetPokemonsDisplay(List<Pokemon> pokemons)
        {

            for (int i = 0; i < PokemonDisplays.Count; i++)
            {
                Pokemon pokemon = pokemons[i];
               PokemonDisplay display = PokemonDisplays[i];
               display.SetDisplay(pokemon.name, pokemon.icon, pokemon);
            }
            
            return this;
        } 
        public BuildingPokemonPopup SetMessage(string message)
        {
            dialog.Description = message;
            return this;
        }
    
        public void Show()
        {
            titleTextUI.text = dialog.Title;
            messageTextUI.text = dialog.Description;
           canvas.SetActive(true);
        }
        public void Hide()
        {
            Debug.Log("Hide CALLED"); 
            canvas.SetActive(false);
            dialog = new Dialog();
        }
        
}
