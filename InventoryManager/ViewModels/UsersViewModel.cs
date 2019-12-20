using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using InventoryManager.Models;
using InventoryManager.Views;
namespace InventoryManager.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public Command LoadUsersCommand { get; set; }
        public UsersViewModel()
        {
            Title = "View Users";
            Users = new ObservableCollection<User> ();
            LoadUsersCommand = new Command(async () => ExecuteLoadUsersCommand());
            MessagingCenter.Subscribe<AddUserPage, User>(this, "AddUser", (Action<AddUserPage, User>)(async (obj, user) =>
            {
                var newUser = user as User;
                Users.Add((User)newUser);
                await UserDataStore.AddUserAsync((User)newUser);

            }));
        }
        async Task ExecuteLoadUsersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Users.Clear();
                var users = await UserDataStore.GetUsersAsync(true);
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
