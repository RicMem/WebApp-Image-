using MyWebApplication.Models.DB;
using MyWebApplication.Models.ViewModel;

namespace MyWebApplication.Models.EntityManager
{
    public class UserManagerBase
    {

        public void UpdateUserAccount(UserModel user)
        {
            using (MyDBContext db = new MyDBContext())
            {
                // Check if a user with the given login name already exists
                SystemUsers existingSysUser = db.SystemUsers.FirstOrDefault(u => u.LoginName == user.LoginName);
                Users existingUser = db.Users.FirstOrDefault(u => u.UserID == existingSysUser.UserID);

                if (existingSysUser != null && existingUser != null)
                {
                    // Update the existing user
                    existingSysUser.ModifiedBy = 1; // This has to be updated
                    existingSysUser.ModifiedDateTime = DateTime.Now;


                    // You can also update other properties of the user as needed
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Gender = user.Gender;

                    db.SaveChanges();
                }
                else
                {
                    // Add a new user since the user doesn't exist
                    SystemUsers newSysUser = new SystemUsers
                    {
                        LoginName = user.LoginName,
                        CreatedBy = 1,
                        PasswordEncryptedText = user.Password, // Update this to handle encryption
                        CreatedDateTime = DateTime.Now,
                        ModifiedBy = 1,
                        ModifiedDateTime = DateTime.Now
                    };

                    db.SystemUsers.Add(newSysUser);
                    db.SaveChanges();

                    int newUserId = newSysUser.UserID;

                    Users newUser = new Users
                    {
                        UserID = newUserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = "1",
                        CreatedBy = 1,
                        CreatedDateTime = DateTime.Now,
                        ModifiedBy = 1,
                        ModifiedDateTime = DateTime.Now,
                        AccountImage = user.AccountImage
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    int roleId = db.Role.First(r => r.RoleName == "Member").RoleID;

                    UserRole userRole = new UserRole
                    {
                        UserID = newUserId,
                        LookUpRoleID = roleId,
                        IsActive = true,
                        CreatedBy = newUserId,
                        CreatedDateTime = DateTime.Now,
                        ModifiedBy = newUserId,
                        ModifiedDateTime = DateTime.Now,
                    };

                    db.UserRole.Add(userRole);
                    db.SaveChanges();
                }
            }
        }
       
    }
}