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
    public class MatchWindowViewModel : ObservableRecipient
    {
        public RestCollection<Match> Matches { get; set; }

        private Match selectedMatch;

        public Match SelectedMatch
        {
            get { return selectedMatch; }
            set
            {
                if (value != null)
                {
                    selectedMatch = new Match()
                    {
                        Id = value.Id,
                        User_1 = value.User_1,
                        User_2 = value.User_2
                    };

                    OnPropertyChanged();

                    ((RelayCommand)AddMatchCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)RemoveMatchCommand).NotifyCanExecuteChanged();
                    ((RelayCommand)UpdateMatchCommand).NotifyCanExecuteChanged();
                }    
            }
        }

        public static bool IsInDesignMode
        {
            get { return (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(System.Windows.FrameworkElement)).Metadata.DefaultValue; }
        }

        public ICommand AddMatchCommand { get; set; }
        public ICommand RemoveMatchCommand { get; set; }
        public ICommand UpdateMatchCommand { get; set; }

        public MatchWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Matches = new RestCollection<Match>("http://localhost:48623/", "match");

                AddMatchCommand = new RelayCommand(
                    () => Matches.Add(new Match()
                        {
                            Id = SelectedMatch.Id,
                            User_1 = SelectedMatch.User_1,
                            User_2 = SelectedMatch.User_2
                        }),
                    () => SelectedMatch != null);

                RemoveMatchCommand = new RelayCommand(
                    () => Matches.Delete(SelectedMatch.Id),
                    () => SelectedMatch != null);

                UpdateMatchCommand = new RelayCommand(
                    () => Matches.Update(SelectedMatch),
                    () => SelectedMatch != null);

                SelectedMatch = new Match();
            }
        }
    }
}
