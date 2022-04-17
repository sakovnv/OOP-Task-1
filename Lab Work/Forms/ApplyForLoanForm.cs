﻿using Lab_Work.Data;
using Lab_Work.Entities;
using Lab_Work.Entities.UserStruct;
using Lab_Work.Forms.ClientForms;
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
    public partial class ApplyForLoanForm : Form
    {
        private ClientAccountsForm clientPanelForm;
        private int accountId;
        private DbSet<Bank> banksDb;
        private DbSet<User> usersDb;
        public ApplyForLoanForm(ClientAccountsForm form)
        {
            InitializeComponent();

            banksDb = Database.GetBanks();
            usersDb = Database.GetUsers();

            accountId = Convert.ToInt32(form.BankAccountsListBox.SelectedItem);
            AccountNumberLabel.Text += accountId;
           
            clientPanelForm = form;
            form.Hide();
        }

        private void ChoosePercentTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChoosePercentTypeComboBox.SelectedIndex == 1)
            {
                IndividualPercentTextBox.Visible = true;
            }
            else
            {
                IndividualPercentTextBox.Visible = false;
            }
        }

        private void TermComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TermComboBox.SelectedIndex == 4)
            {
                TermNumericUpDown.Visible = true;
            }
            else
            {
                TermNumericUpDown.Visible = false;
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (ChoosePercentTypeComboBox.SelectedIndex != -1 && TermComboBox.SelectedIndex != -1
                && PurposeOfLoanTextBox.Text.Length > 0 && AmountTextBox.Text.Length > 0)
            {
                Loan loan = null;
                if (ChoosePercentTypeComboBox.SelectedIndex == 1 && TermComboBox.SelectedIndex == 4)
                {
                    loan = Logged.Client.CreateLoan(Convert.ToDecimal(AmountTextBox.Text), Convert.ToInt32(IndividualPercentTextBox.Text), (int)TermNumericUpDown.Value);
                }
                else if (ChoosePercentTypeComboBox.SelectedIndex == 1)
                {
                    int term = Convert.ToInt32(TermComboBox.SelectedItem.ToString().Trim(new char[] { ' ', 'm', 'o', 'n', 't', 'h' }));

                    loan = Logged.Client.CreateLoan(Convert.ToDecimal(AmountTextBox.Text), Convert.ToInt32(IndividualPercentTextBox.Text), term);
                }
                else if (TermComboBox.SelectedIndex == 4)
                {
                    loan = Logged.Client.CreateLoan(Convert.ToDecimal(AmountTextBox.Text), Convert.ToInt32(ChoosePercentTypeComboBox.SelectedItem), (int)TermNumericUpDown.Value);
                }
                else
                {
                    int term = Convert.ToInt32(TermComboBox.SelectedItem.ToString().Trim(new char[] { ' ', 'm', 'o', 'n', 't', 'h' }));
                    loan = Logged.Client.CreateLoan(Convert.ToDecimal(AmountTextBox.Text), Convert.ToInt32(ChoosePercentTypeComboBox.SelectedItem), term);
                }
                loan.AccountId = accountId;
                banksDb.Save();
                usersDb.Save();

                MessageBox.Show("your loan application has been successfully submitted.\n Wait for confirmation.", "Success");
                
                clientPanelForm.Show();
                Close();
            }
        }
    }
}