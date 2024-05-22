using CookiesCookbook.Recipes.Ingredients;

namespace CookiesCookbook.Recipes
{
    public class RecipesRepository : IRecipesRepository
    {
        private const string Separator = ",";
        private readonly IStringsRepository _stringsRepository;
        private readonly IIngredientsRegister _ingredientsRegister;

        public RecipesRepository(IStringsRepository stringsRepository,
            IIngredientsRegister ingredientsRegister)
        {
            _stringsRepository = stringsRepository;
            _ingredientsRegister = ingredientsRegister;
        }

        public List<Recipe> Read(string filePath)
        {
            List<string> recipesFromFile = _stringsRepository.Read(filePath);

            var recipes = new List<Recipe>();

            foreach (var recipeFromFile in recipesFromFile)
            {
                var recipe = RecipeFromString(recipeFromFile);
                recipes.Add(recipe);
            }

            return recipes;
        }

        private Recipe RecipeFromString(string recipeFromFile)
        {
            var textualIds = recipeFromFile.Split(Separator);
            var ingredients = new List<Ingredient>();

            foreach (var textualId in textualIds)
            {
                var id = int.Parse(textualId);
                var ingredient = _ingredientsRegister.GetById(id);
                ingredients.Add(ingredient);
            }

            return new Recipe(ingredients);
        }

        public void Write(string filePath, List<Recipe> allRecipies)
        {
            var recipesAsStrings = new List<string>();
            foreach (var recipe in allRecipies)
            {
                var allIds = new List<int>();
                foreach (var ingredient in recipe.Ingredients)
                {
                    allIds.Add(ingredient.Id);
                }
                recipesAsStrings.Add(string.Join(Separator, allIds));
            }

            _stringsRepository.Write(filePath, recipesAsStrings);
        }
    }


}
