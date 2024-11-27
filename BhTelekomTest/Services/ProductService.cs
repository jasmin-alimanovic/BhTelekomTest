using BhTelekomTest.Interfaces;
using BhTelekomTest.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace BhTelekomTest.Services;

public class ProductService(FirebaseClient firebaseClient) : IProductService
{
    private const string CollectionName = "products";
    public async Task<Product> CreateAsync(Product product)
    {

        var savedProduct = await firebaseClient
            .Child("products")
            .PostAsync(product);

        return savedProduct.Object;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await firebaseClient
            .Child(CollectionName)
            .OnceAsync<Product>();

        return products.Select(x => new Product
        {
            Id = x.Key,
            Name = x.Object.Name,
            Description = x.Object.Description,
            ManufacturerName = x.Object.ManufacturerName,
            EntryDate = x.Object.EntryDate,
            ModifiedDate = x.Object.ModifiedDate
        }).ToList();
    }

    public async Task<Product> GetAsync(PrimaryKey<Product> primaryKey)
    {
        var product = await firebaseClient
            .Child(CollectionName)
            .Child(primaryKey.Model.Id)
            .OnceSingleAsync<Product>();

        return product;
    }

    public async Task DeleteAsync(PrimaryKey<Product> primaryKey)
    {
        await firebaseClient.Child(CollectionName).Child(primaryKey.Model.Id).DeleteAsync();
    }

    public async Task EditAsync(PrimaryKey<Product> primaryKey, Product entity)
    {
        await firebaseClient.Child(CollectionName).Child(primaryKey.Model.Id).PutAsync(entity);
    }

}
