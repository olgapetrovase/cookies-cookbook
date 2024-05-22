using CookiesCookbook.App;
using CookiesCookbook.FileAccess;
using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;

const FileFormat Format = FileFormat.Json;

IStringsRepository stringsRepository = Format == FileFormat.Json ?
    new StringsJsonRepository() :
    new StringsTextualRepository();

const string FileName = "recipes";

FileMetadata fileMetadata = new(FileName, FileFormat.Json);

var ingredientsRegister = new IngredientsRegister();
var cookieRecipesApp = new CookieRecipesApp(
    new RecipesRepository(stringsRepository, ingredientsRegister),
    new RecipesConsoleUserInteraction(ingredientsRegister));

cookieRecipesApp.Run(fileMetadata.ToPath());











