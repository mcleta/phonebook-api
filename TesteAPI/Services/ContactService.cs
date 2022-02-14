using ContactApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApi.Service
{
    public class ContactService
    {
       private readonly IMongoCollection<Contact> _contacts;

        public ContactService(
            IOptions<ContactDatabaseSettings> cDSettings
        ){
            var mongoClient = new MongoClient(cDSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(cDSettings.Value.Database);

            _contacts = mongoDatabase.GetCollection<Contact>(cDSettings.Value.CollectionName);
        }
        public async Task CreateAsync(Contact newContact) =>
            await _contacts.InsertOneAsync(newContact);

        public async Task<List<Contact>> GetAsync() =>
            await _contacts.Find(_ => true).ToListAsync();


        public async Task<Contact?> GetAsync(string id) =>
            await _contacts.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Contact updateContact) =>
            await _contacts.ReplaceOneAsync(x => x.Id == id, updateContact);

        public async Task DeleteAsync(string id) =>
            await _contacts.DeleteOneAsync(x => x.Id == id);
    }
}
