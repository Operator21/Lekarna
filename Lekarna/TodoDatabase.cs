using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lekarna
{
    public class TodoDatabase
    {
        private SQLiteAsyncConnection database;
        public TodoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Drug>().Wait();
            database.CreateTableAsync<Customer>().Wait();
            database.CreateTableAsync<Ingredient>().Wait();
            database.CreateTableAsync<DrugContent>().Wait();
            database.CreateTableAsync<CustomerAllergy>().Wait();
            database.CreateTableAsync<Order>().Wait();
        }
        public Task<List<Drug>> GetItemsAsync()
        {
            return database.Table<Drug>().ToListAsync();
        }
        public Task<List<Order>> GetOrdersAsync()
        {
            return database.Table<Order>().ToListAsync();
        }
        public Task<List<Customer>> GetCustomersAsync()
        {
            return database.Table<Customer>().ToListAsync();
        }
        public Task<List<Ingredient>> GetIngredientsAsync()
        {
            return database.Table<Ingredient>().ToListAsync();
        }
        public Task<List<DrugContent>> GetDrugContentAsync()
        {
            return database.Table<DrugContent>().ToListAsync();
        }
        public Task<Drug> GetItemAsync(int id)
        {
            return database.Table<Drug>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Customer> GetActive()
        {
            return database.Table<Customer>().Where(i => i.Active == true).FirstOrDefaultAsync();
        }
        public Task<Customer> GetCustomerAsync(int id)
        {
            return database.Table<Customer>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Drug item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> SaveItemAsync(Order item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> IngredientSave(Ingredient item)
        {
            {
                if (item.ID != 0)
                {
                    return database.UpdateAsync(item);
                }
                else
                {
                    return database.InsertAsync(item);
                }
            }
        }
        public Task<int> DrugIngredientSave(DrugContent item)
        {
            {
                if (item.ID != 0)
                {
                    return database.UpdateAsync(item);
                }
                else
                {
                    return database.InsertAsync(item);
                }
            }
        }
        public Task<int> AllergySave(CustomerAllergy item)
        {
            {
                if (item.ID != 0)
                {
                    return database.UpdateAsync(item);
                }
                else
                {
                    return database.InsertAsync(item);
                }
            }
        }
        public Task<int> Delete(Drug item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> Delete(Order item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> Delete(Customer item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> Delete(Ingredient item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> SaveCustomerAsync(Customer item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public async Task<long> GetLastID()
        {
            string sql = @"select last_insert_rowid()";
            return await database.ExecuteScalarAsync<long>(sql);
        }
        public Task<int> SaveDrugContentAsync(DrugContent item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<List<Drug>> GetItem(int id)
        {
            return database.QueryAsync<Drug>("SELECT * FROM [Drug] WHERE [ID] = " + id);
        }
        public Task<List<Ingredient>> GetAllergy(int id)
        {
            return database.QueryAsync<Ingredient>("SELECT * FROM [Ingredient] WHERE [ID] = " + id);
        }
        public Task<List<CustomerAllergy>> GetAllergyID(int id)
        {
            return database.QueryAsync<CustomerAllergy>("SELECT * FROM [CustomerAllergy] WHERE [CustomerID] = " + id);
        }
        public Task<List<DrugContent>> GetIngredientID(int id)
        {
            return database.QueryAsync<DrugContent>("SELECT * FROM [DrugContent] WHERE [drugID] = " + id);
        }
        public Task<List<DrugContent>> EraseContent(int id)
        {
            return database.QueryAsync<DrugContent>("DELETE FROM [DrugContent] WHERE [drugID] = " + id);
        }
        public Task<List<CustomerAllergy>> EraseAllergy(int id)
        {
            return database.QueryAsync<CustomerAllergy>("DELETE FROM [CustomerAllergy] WHERE [CustomerID] = " + id);
        }
        public Task<List<DrugContent>> GetSelected(int id)
        {
            return database.QueryAsync<DrugContent>("SELECT * FROM [DrugContent] WHERE [drugID] = " + id);
        }
        /*public Task<List<Customer>> GetActive()
        {
            return database.QueryAsync<Customer>("SELECT * FROM [Customer] WHERE [Active] = 'true'");
        }*/
        public Task<List<Customer>> AllDeactivate()
        {
            return database.QueryAsync<Customer>("UPDATE [Customer] SET [Active] = 'false'");
        }
        public Task<List<Customer>> GetLike(string a)
        {
            return database.QueryAsync<Customer>("SELECT * FROM [Customer] WHERE [Name] LIKE '" + a + "%' OR [LastName] LIKE '" + a + "%'");
        }
        public Task<List<Drug>> GetIngredientLike(string a)
        {
            return database.QueryAsync<Drug>("SELECT * FROM [Ingredient] WHERE [Name] LIKE '" + a + "%'");
        }
        /*public Task<List<Drug>> GetDrugIngredients(int id)
        {
            return database.QueryAsync<Drug>("SELECT Drug.ID, DrugContent.contentID, Ingredient.ID FROM Drug INNER JOIN Drug ON DrugContent.contentID=Ingredient.ID AND DrugContent=Drug.ID");
        }*/

    }
}
