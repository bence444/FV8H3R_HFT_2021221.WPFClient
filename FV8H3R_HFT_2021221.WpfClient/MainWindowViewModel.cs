using FV8H3R_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FV8H3R_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<User> Users { get; set; }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != null)
                {
                    selectedUser = new User()
                    {
                        Id = value.Id,
                        AvailableLikes = value.AvailableLikes,
                        Bio = value.Bio,
                        Matches = value.Matches,
                        Name = value.Name,
                        RegDate = value.RegDate
                    };

                    OnPropertyChanged();

                    ((RelayCommand)AddUserCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)RemoveUserCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)UpdateUserCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand RemoveUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }

        public static bool IsInDesignMode
        {
            get { return (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(System.Windows.FrameworkElement)).Metadata.DefaultValue; }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:48623/", "user");

                AddUserCommand = new RelayCommand(
                    () => Users.Add(new User()
                        {
                            Id = SelectedUser.Id,
                            AvailableLikes = SelectedUser.AvailableLikes,
                            Bio = SelectedUser.Bio,
                            Matches = SelectedUser.Matches,
                            Name = SelectedUser.Name,
                            RegDate = SelectedUser.RegDate
                        }),
                    () => SelectedUser != null); ;

                RemoveUserCommand = new RelayCommand(
                    () => Users.Delete(SelectedUser.Id),
                    () => SelectedUser != null);

                UpdateUserCommand = new RelayCommand(
                    () => Users.Update(SelectedUser),
                    () => SelectedUser != null);

                SelectedUser = new User();
            }
        }
    }
}
