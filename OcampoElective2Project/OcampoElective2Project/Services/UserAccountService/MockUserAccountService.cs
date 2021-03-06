﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OcampoElective2Project.Models;
using OcampoElective2Project.Repository;
using OcampoElective2Project.Repository.LocalRepository;

namespace OcampoElective2Project.Services.UserAccountService
{
    public class MockUserAccountService : IUserAccountService
    {
        private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OcampoElective.db3");

        private IRepository _repository;
        public MockUserAccountService()
        {
            _repository = new LocalRepository();
        }
      

        public  void UpdateUser(UserAccount oldUser, UserAccount newUser)
        {
            Task.Delay(150);
           _repository.UserAccount.Update(c => c.AccountId == newUser.AccountId, newUser);
        }
    }
}
