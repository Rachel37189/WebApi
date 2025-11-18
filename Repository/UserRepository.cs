using System.Reflection.Metadata;
using System.Text.Json;
using Entities;
namespace Repository
{
    public class UserRepository
    {
        private readonly string _filePath = "newUsers.txt";
        public User GetUserById(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user != null && user.Id == id)
                        return user;
                }
            }
            return null;
        }

        public User AddUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            user.Id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(_filePath, userJson + Environment.NewLine);
            return user;
        }
        
        public void UpdateUser(int id, User user)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? existingUser = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (existingUser != null && existingUser.Id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(_filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(user));
                System.IO.File.WriteAllText(_filePath, text);
            }
        }
        
        public User Login(User user)
        {
            using (StreamReader reader = System.IO.File.OpenText(_filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User? userFromFile = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (userFromFile != null && userFromFile.UserName == user.UserName && userFromFile.Password == user.Password)
                        return userFromFile;
                }
            }
            return null;
        }
    }
}
