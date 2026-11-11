using Dapper;
using DataManagement.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class DbBuilder : DataManager
    {
        public bool CheckAndBuildDatabase()
        {
            SqlConnection defaultConnection = ConnectionBuilder.GetSqlConnection();
            string databaseName = defaultConnection.Database;
            string connectionString = $"Server = {defaultConnection.DataSource};Trusted_Connection = true;" +
           $"TrustServerCertificate = true;";

            string query = $"SELECT * FROM sys.databases WHERE Name = '{databaseName}'";

            bool databaseExists = false;

            using (var connection = new SqlConnection(connectionString))
            {
                var rows = connection.QuerySingleOrDefault(query);

                databaseExists = (rows != null);

                if (databaseExists == false)
                {
                    query = $"Create Database {databaseName}";
                    connection.Execute(query);
                }
            }
            return databaseExists;
        }

        public void BuildDatabaseTables()
        {
            BuildMembersTable();
            BuildCategoriesTable();
            BuildItemsTable();
            BuildLoansTable();
        }

        private void BuildMembersTable()
        {
            string query = "CREATE TABLE Members " + "(" + "MemberId INT PRIMARY KEY IDENTITY(1,1), " +
                "FirstName NVARCHAR(80) NOT NULL, " +
                "LastName NVARCHAR(80) NOT NULL, " +
                "Phone NVARCHAR(15) NOT NULL, " +
                "Email NVARCHAR(50) NOT NULL, " +
                ")";
            using (var connection = ConnectionBuilder.GetSqlConnection())
            {
                connection.Execute(query);
            }
        }

        private void BuildCategoriesTable()
        {
            string query = "CREATE TABLE Categories " +
                "(" + 
                "CategoryId INT PRIMARY KEY IDENTITY(1,1), " +
                "CategoryName NVARCHAR(80) NOT NULL, " +
                ")";
            using (var connection = ConnectionBuilder.GetSqlConnection())
            {
                connection.Execute(query);
            }
        }

        private void BuildItemsTable()
        {
            string query = "CREATE TABLE Items " +
                "(" +
                "ItemId INT PRIMARY KEY IDENTITY(1,1), " +
                "CategoryId INT NOT NULL, " +
                "ItemName NVARCHAR(50) NOT NULL, " +
                "Description NVARCHAR(MAX) NOT NULL, " +
                "CONSTRAINT Fk_Items_Categories FOREIGN KEY (CategoryId) " +
                "REFERENCES Categories(CategoryId) " +
                ")";
            using (var connection = ConnectionBuilder.GetSqlConnection())
            {
                connection.Execute(query);
            }
        }
        // Added new comment here for 449 Assessment
        private void BuildLoansTable()
        {
            string query = "CREATE TABLE Loans " +
                "(" +
                "LoanId INT PRIMARY KEY IDENTITY(1,1)," +
                "ItemId INT NOT NULL, " +
                "MemberId INT NOT NULL, " +
                "LoanDate DATETIME NOT NULL, " +
                "DueDate DATETIME NOT NULL, " +
                "ReturnedDate DATETIME NULL, " +
                "CONSTRAINT Fk_Loans_Items FOREIGN KEY (ItemId) " +
                "REFERENCES Items(ItemId), " +
                "CONSTRAINT Fk_Loans_Members FOREIGN KEY (MemberId) " +
                "REFERENCES Members(MemberId) " +
                ")";
            using (var connection = ConnectionBuilder.GetSqlConnection())
            {
                connection.Execute(query);
            }
        }

        public void SeedDatabaseTables()
        {
            SeedMembersTable();
            SeedCategoriesTable();
            SeedItemsTable();
            SeedLoanTable();
        }

        private void SeedMembersTable()
        {
            List<Member> membersList = new List<Member>
            {
                new Member{ FirstName = "Troy", LastName = "Vaughn", Phone = "0456263598", Email = "troy.vaughn@tafeqld.ed.au"},
                new Member{ FirstName = "Jane", LastName = "Doe", Phone = "0452255228", Email = "janeygirl19@gmail.com"},
                new Member{ FirstName = "Bob", LastName = "La Builder", Phone = "0423262854", Email = "hecanbuildit@hoymail.coms"},
            };

            foreach (var member in membersList)
            {
                CreateMember(member);
            }
        }

        private void SeedCategoriesTable()
        {
            List<Category> categoryList = new List<Category>
            {
                new Category{CategoryName = "Poor Tools"},
                new Category{CategoryName = "Generators"},
                new Category{CategoryName = "Escavators"},
                new Category{CategoryName = "Ladders & Scaff"},
                new Category{CategoryName = "Hand Tools"},

            };
            foreach (var category in categoryList)
            {
                CreateCategory(category);
            }
        }

        private void SeedItemsTable()
        {
            List<Item> itemsList = new List<Item>
            {
                new Item{ItemName = "Makita Hammer Drill", CategoryId = 1, Description = "In case with power supply and charger"},
                new Item{ItemName = "Toyota Mini Escavator", CategoryId = 3, Description = "12HP, 1159kg operating weight"},
                new Item{ItemName = "5.1M Gorilla", CategoryId =4, Description = "Fibreglass extension ladder"},
            };
            foreach (var item in itemsList)
            {
                CreateItem(item);
            }
        }

        private void SeedLoanTable()
        {
            List<Loan> loanList = new List<Loan>
            {
                new Loan{MemberId = 1, ItemId = 2, LoanDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) },
                new Loan{MemberId = 2, ItemId = 1, LoanDate = DateTime.Now.AddDays(-7), DueDate = DateTime.Now },

            };

            foreach (var loan in loanList)
            {
                CreateLoan(loan);
            }
        }


    }




}
