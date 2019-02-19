﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using OcampoElective2Project.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OcampoElective2Project.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddClothesPage : ContentPage
	{
		public AddClothesPage ()
		{
			InitializeComponent ();
            BindingContext = App.Locator.AddClothesViewModel;
        }
	    protected override void OnAppearing()
	    {
            
	        base.OnAppearing();
	        var currentPageKeyString = ServiceLocator.Current
	            .GetInstance<INavigationService>()
	            .CurrentPageKey;
	        Debug.WriteLine("Current page key: " + currentPageKeyString);
            NamePrice.Text = "";
            NameEntry.Text = "";
        }
	    public AddClothesPage(UserAccount user)
	    {
	        InitializeComponent();

	        App.Locator.AddClothesViewModel.User = user;
	        this.BindingContext = App.Locator.AddClothesViewModel;
	    }
    }
}