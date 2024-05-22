using CookiesCookbook.Recipes;

namespace CookiesCookbook.App
{
    public class CookieRecipesApp
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IRecipesUserInteraction _recipesUserInteraction;
        public CookieRecipesApp(IRecipesRepository recipesRepository,
            IRecipesUserInteraction recipesUserInteraction)
        {
            _recipesRepository = recipesRepository;
            _recipesUserInteraction = recipesUserInteraction;
        }

        internal void Run(string filePath)
        {
            var allRecipies = _recipesRepository.Read(filePath);
            _recipesUserInteraction.PrintExistingRecipes(allRecipies);

            _recipesUserInteraction.PromptToCreateRecipe();

            var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

            if (ingredients.Count() > 0)
            {
                var recipe = new Recipe(ingredients);
                allRecipies.Add(recipe);
                _recipesRepository.Write(filePath, allRecipies);

                _recipesUserInteraction.ShowMessage("Recipe added:");
                _recipesUserInteraction.ShowMessage(recipe.ToString());
            }
            else
            {
                _recipesUserInteraction.ShowMessage(
                    "No ingredients have been selected. " +
                    "Recipe will not be saved.");
            }

            _recipesUserInteraction.Exit();
        }
    }
}