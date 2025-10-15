using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDbContextDataSeeding
    {
       public static bool SeedData(GymDbContext dbContext)
        {
           try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategory = dbContext.Categories.Any();

                if (HasPlans && HasCategory) return false;

                if (!HasPlans)
                {
                    var plans = LoadDataFromJasonFile<Plan>("plans.json");
                    if (plans.Any())
                        dbContext.Plans.AddRange(plans);

                }
                if (!HasCategory)
                {
                    var Category = LoadDataFromJasonFile<Category>("categories.json");
                    if (Category.Any())
                        dbContext.Categories.AddRange(Category);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Seedong failed {Ex}");
                return false;
            }
        }

        private static List<T> LoadDataFromJasonFile<T>(string fileName)
        {
            // C:\Users\Bassem\Downloads\GymManagementSystemSolution\GymManagementPl\  , Directory.GetCurrentDirectory()
            // wwwroot\Files , part 2 fl code "wwwroot\\Files"
            // \categories.json , da hwa el file name 
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);

            if (!File.Exists(FilePath)) throw new FileNotFoundException();

            string Data = File.ReadAllText(FilePath);
            var Option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true, // 34an lw fe e5tlaf fe asma2 el attributes ben el code w el data 
            };

            return JsonSerializer.Deserialize<List<T>>(Data , Option) ?? new List<T>();
        }
    }
}
