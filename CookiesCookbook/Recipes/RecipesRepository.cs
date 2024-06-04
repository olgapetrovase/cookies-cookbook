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
            return _stringsRepository.Read(filePath)
                .Select(RecipeFromString)
                .ToList();
        }

        private Recipe RecipeFromString(string recipeFromFile)
        {
            var ingredients = recipeFromFile.Split(Separator)
                .Select(int.Parse)
                .Select(_ingredientsRegister.GetById);

            return new Recipe(ingredients);
        }

        public void Write(string filePath, List<Recipe> allRecipies)
        {
            var recipesAsStrings = allRecipies
                .Select(recipe =>
                {
                    var allIds = recipe.Ingredients
                    .Select(ingredient => ingredient.Id);

                    return string.Join(Separator, allIds);
                })
                .ToList();

            _stringsRepository.Write(filePath, recipesAsStrings);
        }
    }


}
