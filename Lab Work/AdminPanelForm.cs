﻿using Lab_Work.Data;
using Lab_Work.Entities;
using Lab_Work.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Work
{
    public partial class AdminPanelForm : Form
    {
        DbSet<User> usersDb;
        public AdminPanelForm()
        {
            usersDb = Database.GetUsers();

            InitializeComponent();
            FillUsersList();
        }

        private void FillUsersList()
        {
            foreach (User item in usersDb.Set)
            {
                UsersListBox.Items.Add(item.Login);
            }
        }

        private void UsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdLabel.Text = "ID: " + usersDb.Set[UsersListBox.SelectedIndex].Id;
            LoginLabel.Text = "Login: " + usersDb.Set[UsersListBox.SelectedIndex].Login;
            EmailLabel.Text = "Email " + usersDb.Set[UsersListBox.SelectedIndex].Email;
            IsApprovedLabel.Text = "Is approved: " + usersDb.Set[UsersListBox.SelectedIndex].IsApproved;
            if (usersDb.Set[UsersListBox.SelectedIndex].Client != null)
            { 
                FullNameLabel.Text = "Full name: " + usersDb.Set[UsersListBox.SelectedIndex].Client.FullName;
                PassportSeriesLabel.Text = "Passport series: " + usersDb.Set[UsersListBox.SelectedIndex].Client.PassportSeries;
                PassportNumberLabel.Text = "Passport number: " + usersDb.Set[UsersListBox.SelectedIndex].Client.PassportNumber;
                PhoneNumberLabel.Text = "Phone number: " + usersDb.Set[UsersListBox.SelectedIndex].Client.PhoneNumber;
            
                if (usersDb.Set[UsersListBox.SelectedIndex].Client.Loans.Count != 0)
                {
                    foreach (Loan loan in usersDb.Set[UsersListBox.SelectedIndex].Client.Loans)
                    {
                        LoansListBox.Items.Add(loan.Id);
                    }
                }
            }


            if (!usersDb.Set[UsersListBox.SelectedIndex].IsApproved)
            {
                ApproveButton.Visible = true;
            }
            else
            {
                ApproveButton.Visible = false;
            }
        }

        private void ApproveButton_Click(object sender, EventArgs e)
        {
            usersDb.Set[UsersListBox.SelectedIndex].IsApproved = true;
        }

        private void LoansListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usersDb.Set[UsersListBox.SelectedIndex].Client != null)
            {
                Loan loan = usersDb.Set[UsersListBox.SelectedIndex].Client.Loans[LoansListBox.SelectedIndex];
                LoanIdLabel.Text = "ID: " + loan.Id;
                LoanAmountLabel.Text = "Amount: " + loan.Amount;
                LoanTermLabel.Text = "Term: " + loan.Term;
                LoanPercentLabel.Text = "Percent: " + loan.Percent;
                LoanCreateTimeLabel.Text = "Create time: " + loan.CreateTime;
                LoanEndTimeLabel.Text = "End time: " + loan.EndTime;
                LoanIsApprovedLabel.Text = "Is approved: " + loan.IsApproved;
                
                if (!loan.IsApproved)
                {
                    LoanIsApprovedButton.Visible = true;
                }
                else
                {
                    LoanIsApprovedButton.Visible = false;
                }
            }
        }
    }
}
