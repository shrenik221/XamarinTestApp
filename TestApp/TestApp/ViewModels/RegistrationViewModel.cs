using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TestApp.DTO;
using TestApp.Services;

namespace TestApp.ViewModels
{
    class RegistrationViewModel: INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegistrationViewModel() {
            GetUserAsync();
        }

        private ObservableCollection<User> _users;

        public event PropertyChangedEventHandler PropertyChanged;

        public  ObservableCollection<User> Users {
            get { return _users; }
            set { _users = value;
                OnPropertyChanged();
            } 
        }

        public async static Task AddUser(string name, string gender, byte[] image)
        {
            await SqliteConnection.AddUser(name, gender, image);
        }
        
         public async Task GetUserAsync()
        {
            var users = await SqliteConnection.GetUser();
            Users = new ObservableCollection<User>(users);
        }
    }
}
