using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminProgram.Models;
using AdminProgram.ViewModels;
using AdminProgram.Views;

namespace AdminProgram.ViewModels
{
    public class DataBaseControl : AppDbContext
    {
        public void AddCar(Car car)
        {
            Cars.Add(car);
            SaveChanges();
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
            SaveChanges();
        }
        public void RemoveUser(User user)
        {
            Users.Remove(user);
            SaveChanges();
        }
        /*
        public static List<User> UsersForView()
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                return ctx.users.Include(p => p.RoleEntity).ToList();
            }
        }
        
        public static void RegisterUser(User user)
        {
            using (AppDbContext ctx = new AppDbContext())
            {
                ctx.users.Add(user);
                ctx.SaveChanges();
            }
        }*/ // Мб это всё юзлесс параша, пусть в комменте побудет
    }
}
