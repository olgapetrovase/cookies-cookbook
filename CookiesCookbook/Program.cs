using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

var cookieRecipesApp = new CookieRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction());

cookieRecipesApp.Run("recipe.txt");

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

        //var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        //if (ingredients.Count > 0) 
        //{
        //    var recipe = new Recipe(ingredients);
        //    allRecipies.Add(recipe);
        //    _recipesRepository.Write(filePath, allRecipies);

        //    RecipesConsoleUserInteraction.ShowMessage("Recipe added:");
        //    _recipesUserInteraction.ShowMessage(recipe.ToString());
        //}
        //else
        //{
        //    RecipesConsoleUserInteraction.ShowMessage(
        //        "No ingredients have been selected. " +
        //        "Recipe will not be saved.");
        //}

        //RecipesConsoleUserInteraction.Exit();
    }
}

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipies);
    void PromptToCreateRecipe();
}
public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipies)
    {
        if (allRecipies.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);

            var counter = 1;
            foreach (var recipe in allRecipies)
            {
                Console.WriteLine($"***** {counter} *****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }

    }

    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cokkie recipe! " +
            "Available ingredients are:");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
}
public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>
        {
            new Recipe(new List<Ingredient>
            {
                new WheatFlour(),
                new Butter(),
                new Sugar()
            }),
            new Recipe(new List<Ingredient>
            {
                new CocoaPowder(),
                new CoconutFlour(),
                new Cinnamon()
            }),
        };
    }
}

enum FileFormat
{
    Json,
    Txt
}







