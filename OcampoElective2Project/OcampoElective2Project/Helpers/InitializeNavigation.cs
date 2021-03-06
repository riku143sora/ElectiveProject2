﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OcampoElective2Project.Models;
using OcampoElective2Project.Services;
using OcampoElective2Project.ViewModels;
using OcampoElective2Project.Views;
using SQLite;
using Xamarin.Forms;

namespace OcampoElective2Project.Helpers
{

    public class InitializeNavigation
    {
        private static string dbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OcampoElective.db3");
        public NavigationService navigationService { get; }
      
        public InitializeNavigation()
        {
            navigationService = new NavigationService();
            SimpleIoc.Default.Reset();
            SetPages();
        
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            InitializeData();
            


        }

         public void InitializeData()
        {
            //  if (IsTestMode == true)

      
            
            {
                var connection = new SQLiteAsyncConnection(dbPath);
                

                connection.CreateTableAsync<UserAccount>();
                connection.CreateTableAsync<Clothes>();
                connection.CreateTableAsync<Food>();
                connection.CreateTableAsync<Others>();
                connection.CreateTableAsync<Transportation>();
                connection.CreateTableAsync<Income>();

                //connection.DeleteAllAsync<Clothes>();
                //connection.DeleteAllAsync<Food>();
                //connection.DeleteAllAsync<Others>();
                //connection.DeleteAllAsync<Transportation>();
                //connection.DeleteAllAsync<UserAccount>();

                
                var food= connection.Table<Food>();
                var clothes = connection.Table<Clothes>();
                var others = connection.Table<Others>();
                var transportation = connection.Table<Transportation>();
                var useraccount = connection.Table<UserAccount>();
                var income = connection.Table<Income>();

                var listofaccount = useraccount.ToListAsync();
                var listOfFood = food.ToListAsync();
                var listOfOthers = others.ToListAsync();
                var listOfClothes = clothes.ToListAsync();
                var listOfTransportation = transportation.ToListAsync();
                var listOfIncome = income.ToListAsync();

                //  CreateMockDataSQL(connection);


                //var user = connection.Table<UserAccount>();
                //var listOfUser = user.ToList();


            }

        }
         private void CreateMockDataSQL(SQLiteAsyncConnection connection)
         {
             for (int i = 0; i < 5; i++)
             {
                 connection.InsertAsync(new Clothes()
                 {
                     Name = $" Clothes {i}",
                     Price = i * 100,
                     UserId = 0+i
                 });
             }


             for (int i = 0; i < 3; i++)
             {
                 connection.InsertAsync(new Food()
                 {
                     NameOfFood= $" Food {i}",
                     Price = i * 100,
                     UserId = 0 + i
                 });
             }

             for (int i = 0; i < 3; i++)
             {
                 connection.InsertAsync(new Others()
                 {
                     Name = $" Other {i}",
                     Price = i * 100,
                     UserId = 0 + i
                 });
             }

             for (int i = 0; i < 3; i++)
             {
                 connection.InsertAsync(new Transportation()
                 {
                     Name = $" Transportation {i}",
                     Price = i * 100,
                     UserId = 0 + i
                 });
             }

            for (int i = 0; i < 3; i++)
            {
                connection.InsertAsync(new UserAccount()
                {
                    FirstName = $"ata {i}",
                   // AccountId = i + 2,
                    Username = $"{i + 2}",
                    Password = $"{i + 2}",
                    

                });
            }

            connection.InsertAsync(new Clothes()
            {
                UserId = 75,
                Name = "Clothes ni 75",
                Price = 1000
            });
         }

        public void SetPages()
        {
            navigationService.Configure(ViewModelLocator.LogInPage, typeof(LogInPage));
            navigationService.Configure(ViewModelLocator.ClothesPage, typeof(ClothesPage));
            navigationService.Configure(ViewModelLocator.FoodPage, typeof(FoodPage));
            navigationService.Configure(ViewModelLocator.HomePage, typeof(HomePage));
            navigationService.Configure(ViewModelLocator.MenuPage, typeof(MenuPage));
            navigationService.Configure(ViewModelLocator.OthersPage, typeof(OthersPage));
            navigationService.Configure(ViewModelLocator.RegistrationPage, typeof(RegistrationPage));
            navigationService.Configure(ViewModelLocator.TransportationPage, typeof(TransportationPage));
            navigationService.Configure(ViewModelLocator.AddClothesPage, typeof(AddClothesPage));
            navigationService.Configure(ViewModelLocator.AddFoodPage, typeof(AddFoodPage));
            navigationService.Configure(ViewModelLocator.AddOthersPage, typeof(AddOthersPage));
            navigationService.Configure(ViewModelLocator.AddTransportationPage, typeof(AddTransportationPage));
            navigationService.Configure(ViewModelLocator.ExpensePage, typeof(ExpensePage));;
            navigationService.Configure(ViewModelLocator.IncomePage, typeof(IncomePage));
            navigationService.Configure(ViewModelLocator.AddIncomePage,typeof(AddIncomePage));




        }

        public MasterDetailPage SetMasterDetailMainPage()
        {
            bool isGestureEnabled;
            var navigationPage = new NavigationPage();
            var user = new UserAccount();
            if (SettingsImplementation.IsLoggedIn)
            {
                var userJsonString = JToken.Parse(SettingsImplementation.User).ToString();
                user = JsonConvert.DeserializeObject<UserAccount>(userJsonString);
                navigationPage = new NavigationPage(new HomePage(user));
                isGestureEnabled = true;

            }
            else
            {
                navigationPage = new NavigationPage(new LogInPage());
                isGestureEnabled = false;
                
            }
            var masterDetailPage = new MasterDetailPage
            {
                Detail = navigationPage,
                Master = new MenuPage(user) { Title = "Menu" }
            };
            navigationService.Initialize(navigationPage);
            masterDetailPage.IsGestureEnabled = isGestureEnabled;



            return masterDetailPage;

        }
        //public NavigationPage Intropage()
        //{
        //    bool isGestureEnabled;
        //    var navigationPage = new NavigationPage();
        //    var user = new UserAccount();
        //    if (SettingsImplementation.IsLoggedIn)
        //    {
        //        // var userJsonString = JToken.Parse(SettingsImplementation.User).ToString();
        //        //  user = JsonConvert.DeserializeObject<UserAccount>(userJsonString);
        //        navigationPage = new NavigationPage(new HomePage(user));
        //        isGestureEnabled = true;
        //    }
        //    else
        //    {
        //        navigationPage = new NavigationPage(new LogInPage());
        //        isGestureEnabled = false;
        //    }

        //    var state = new NavigationPage();
        //    return state;
        //}

    }
}
