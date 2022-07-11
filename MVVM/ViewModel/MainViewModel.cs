using Magacin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTEKA.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ShowBooksViewCommand { get; set; }
        public RelayCommand SignOutViewCommand { get; set; }
        public RelayCommand ShowLibrariansViewCommand { get; set; }
        public RelayCommand AddLibrarianViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public RelayCommand StudentBookCommand { get; set; }
        public RelayCommand FileViewCommand { get; set; }
        public RelayCommand ManageStudentViewCommand { get; set; }
        public RelayCommand ShowRentedBooksViewCommand { get; set; }
        public RelayCommand AddBookViewCommand { get; set; }
        public RelayCommand AddBookTypeViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public ShowBooksViewModel ShowBooksVM { get; set; }
        public SignOutViewModel SignOutVM { get; set; }
        public ShowLibrariansViewModel ShowLibrariansVM { get; set; }
        public AddLibrarianViewModel AddLibrarianVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public ShowStudentBookViewModel StudentBookVM { get; set; }
        public FileViewModel FileVM { get; set; }
        public ManageStudentViewModel ManageStudentVM { get; set; }
        public ShowRentedBooksViewModel ShowRentedBooksVM { get; set; }
        public AddBookViewModel AddBookVM { get; set; }
        public AddBookTypeViewModel AddBookTypeVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            ShowBooksVM = new ShowBooksViewModel();
            SignOutVM = new SignOutViewModel();
            ShowLibrariansVM = new ShowLibrariansViewModel();
            AddLibrarianVM = new AddLibrarianViewModel();
            ProfileVM = new ProfileViewModel();
            StudentBookVM = new ShowStudentBookViewModel();
            FileVM = new FileViewModel();
            ManageStudentVM = new ManageStudentViewModel();
            ShowRentedBooksVM = new ShowRentedBooksViewModel();
            AddBookVM = new AddBookViewModel();
            AddBookTypeVM = new AddBookTypeViewModel();

            if (GLOBALS.TYPE.Equals("student"))
            {
                CurrentView = ShowBooksVM;
            }
            else if (GLOBALS.TYPE.Equals("bibliotekar"))
            {
                CurrentView = ShowBooksVM;
            }
            else if (GLOBALS.TYPE.Equals("administrator"))
            {
                CurrentView = HomeVM;
            }
            else
            {
                CurrentView = HomeVM;
            }

            HomeViewCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
                
            });

            ShowBooksViewCommand = new RelayCommand(o => {
                CurrentView = ShowBooksVM;

            });

            SignOutViewCommand = new RelayCommand(o => {
                CurrentView = SignOutVM;

            });

            ShowLibrariansViewCommand = new RelayCommand(o => {
                CurrentView = ShowLibrariansVM;

            });

            AddLibrarianViewCommand = new RelayCommand(o => {
                CurrentView = AddLibrarianVM;

            });

            ProfileViewCommand = new RelayCommand(o => {
                CurrentView = ProfileVM;

            });

            StudentBookCommand = new RelayCommand(o => {
                CurrentView = StudentBookVM;

            });

            FileViewCommand = new RelayCommand(o => {
                CurrentView = FileVM;

            });

            ManageStudentViewCommand = new RelayCommand(o => {
                CurrentView = ManageStudentVM;

            });

            ShowRentedBooksViewCommand = new RelayCommand(o => {
                CurrentView = ShowRentedBooksVM;

            });

            AddBookViewCommand = new RelayCommand(o => {
                CurrentView = AddBookVM;

            });

            AddBookTypeViewCommand = new RelayCommand(o => {
                CurrentView = AddBookTypeVM;

            });

        }
    }
}
