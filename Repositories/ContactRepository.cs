using ApiTools.Model;

namespace ApiTools.Repositories
{
    public static class ContactRepository
    {
        public static Contact Get(string username)
        {
            var contact = new List<Contact>();
            contact.Add(new Contact { ContactId = 1, Username = "user" });
            contact.Add(new Contact { ContactId = 2, Username = "pverly" });
            return contact.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
        }
    }
}