using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

var cookieRecipesApp = new CookieRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction(new IngredientsRegister()));

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

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count() > 0)
        {
            var recipe = new Recipe(ingredients);
            allRecipies.Add(recipe);
            //_recipesRepository.Write(filePath, allRecipies);

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

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipies);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}
public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }
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

        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        bool shallStop = false;
        var ingredients = new List<Ingredient>();

        while(!shallStop)
        {
            Console.WriteLine("Add an ingredient by its ID, " +
                "or type anything else if finished.");

            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetById(id);
                if (selectedIngredient is not null)
                {
                    ingredients.Add(selectedIngredient);
                }
            }
            else
            {
                shallStop = true;
            }
        }

        return ingredients;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public class IngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };

    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            { return ingredient; }
        }

        return null;
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







