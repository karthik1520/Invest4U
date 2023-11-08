/* 
 * Student Name: Karthik Mahadevan Ramesh Kumar
 * Student ID: 23101941
 * Date:08/11/2023
 * Assignment: 3
 * Assignment: Create an application for Invest4U Application of Mad4Money Bank Corp Organization
 */

/* Invest4U Application
 * This application is designed to process client transaction.
 * The application enables users to select differnet investment plans and calculates its interest at the end of term.
 * Application includes features to Search based on transactionID, Customer Name, Date of Transaction and customer emailid
 * It allows for summary view
 */

using System.IO;

namespace Invest4U
{
    public partial class Invest4U : Form
    {
        public Invest4U()
        {
            InitializeComponent();
        }

        // Declaring constant and global variables

        const int PANEL1TERM1 = 1, PANEL1TERM2 = 3, PANEL1TERM3 = 5, PANEL1TERM4 = 10, PANEL2TERM1 = 1, PANEL2TERM2 = 3,
            PANEL2TERM3 = 5, PANEL2TERM4 = 10;
        const double PANEL1TERM1INTEREST = 0.00500, PANEL1TERM2INTEREST = 0.006250, PANEL1TERM3INTEREST = 0.007125, PANEL1TERM4INTEREST = 0.011250,
            PANEL2TERM1INTEREST = 0.00600, PANEL2TERM2INTEREST = 0.007250, PANEL2TERM3INTEREST = 0.008125, PANEL2TERM4INTEREST = 0.012500;
        const int BONUS = 25000;

        int MaxLoginAttempt = 3, LoginAttempt = 0, IncorrectAttempt = 3;
        int PanelTerm, Age = 0;
        double TermInterest = 0, PrincipalInvestment, EndOfTermBalance, InterestEarned;
        const string PASSWORD = "ShowMeTheMoney#";

        string Date = DateTime.Now.ToString("d");


        // Events that happen onload of form and setting tooptips
        private void Invest4U_Load(object sender, EventArgs e)
        {
            Invest4ULogo.Visible = false;
            IncorrectPasswordLabel.Visible = false;
            InvestmentPanel.Visible = false;
            ButtonPanel.Visible = false;
            SearchEngineGroupBox.Visible = false;
            SummaryGroupBox.Visible = false;
            InvestmentUptoListBox.Enabled = false;
            InvestmentMoreThanListBox.Enabled = false;
            IncorrectAttemptLabel.Visible = false;
            BonusLabel.Visible = false;
            InvestmentCategory1RadioButton.Enabled = false;
            InvestmentCategory2RadioButton.Enabled = false;
            SelectedInvestmentPlanGroupBox.Visible = false;
            SearchTransactionListBox.Visible = false;
            TransactionNumberSearchTextBox.Enabled = false;
            NameSearchTextBox.Enabled = false;
            DateSearchTextBox.Enabled = false;
            EmailIDSearchTextBox.Enabled = false;
            EnableSearchRadioButton.Visible = false;
            DisableSearchRadioButton.Visible = false;
            SearchEngineGroupBox.Enabled = false;
            SearchButton.Enabled = false;
            SummaryPanel.Visible = false;

            DisplayButtonToolTip.SetToolTip(DisplayButton, "Click to DIsplay The investment Plan Details");
            ProceedButtonToolTip.SetToolTip(ProceedButton, "CLick to proceed with the selected Investment Plan");
            SubmitButtonToolTip.SetToolTip(SubmitButton, "Click to Submit the Selected Investment Plan");
            SummaryButtonToolTip.SetToolTip(SummaryButton, "Click to View the Summary of Investments");
            SearchButtonToolTip.SetToolTip(SearchButton, "Click to Search for particular Investment Records");
            EndButtonToolTip.SetToolTip(EndButton, "Click to End/Clear the current Transaction");
            ExitButtonToolTip.SetToolTip(ExitButton, "Click to Close the Application");

        }

        /* Actions performed on click of LoginButton
        * Checking if the passwordtextbox matches the default password
        * onlogin, display contents to perform investment actions
        */
        private void LoginButton_Click(object sender, EventArgs e)
        {
            DisplayButton.Enabled = false;
            ProceedButton.Enabled = false;
            SubmitButton.Enabled = false;
            if (PasswordTextBox.Text == PASSWORD)
            {
                InvestmentPanel.Visible = true;
                ButtonPanel.Visible = true;
                BonusLabel.Visible = true;
                LoginPanel.Visible = false;
                SearchEngineGroupBox.Visible = true;
                SummaryGroupBox.Visible = false;
                SelectedInvestmentPlanGroupBox.Visible = false;
                Invest4ULogo.Visible = true;
                EnableSearchRadioButton.Visible = true;
                DisableSearchRadioButton.Visible = true;
            }
            else
            {
                IncorrectPasswordLabel.Visible = true;
                IncorrectAttempt -= 1;
                IncorrectAttemptLabel.Visible = true;
                IncorrectAttemptLabel.Text = $"You have {IncorrectAttempt} attempts left";
                LoginAttempt += 1;

                if (LoginAttempt >= MaxLoginAttempt)
                {
                    MessageBox.Show("You have exceeded the maximum limit! The applcation will close now");
                    this.Close();
                }
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
            }
        }

        // Method to calculate interest for the selected investment plans
        public double CalculateInterest(double Investment, double InterestRate, int Duration)
        {
            PrincipalInvestment = double.Parse(InvestmentAmountTextBox.Text);
            EndOfTermBalance = 0;
            int year = PanelTerm, months = 12;
            for (int i = 1; i <= year; i++)
            {
                double formulapart = 1 + InterestRate / (100 * months);
                double formulapower = 12 * year;
                EndOfTermBalance = PrincipalInvestment * (Math.Pow(formulapart, formulapower));
                if (PrincipalInvestment > 1000000 && year >= 5)
                {
                    EndOfTermBalance = EndOfTermBalance + BONUS;
                }
            }
            return EndOfTermBalance;
        }

        // Actions performed onclick of Display Button.
        private void DisplayButton_Click(object sender, EventArgs e)
        {
            SelectedInvestmentPlanGroupBox.Visible = false;
            SummaryGroupBox.Visible = true;
            double PrincipalAmount = double.Parse(InvestmentAmountTextBox.Text);
            int InvestmentAmount = int.Parse(InvestmentAmountTextBox.Text);
            OutputInvestmentAmountTextBox.Text = InvestmentAmount.ToString("C0");
            if (InvestmentCategory1RadioButton.Checked)
            {
                for (int i = 0; i < InvestmentUptoListBox.Items.Count; i++)
                {
                    switch (i)
                    {
                        case 0: PanelTerm = PANEL1TERM1; TermInterest = PANEL1TERM1INTEREST; break;
                        case 1: PanelTerm = PANEL1TERM2; TermInterest = PANEL1TERM2INTEREST; break;
                        case 2: PanelTerm = PANEL1TERM3; TermInterest = PANEL1TERM3INTEREST; break;
                        case 3: PanelTerm = PANEL1TERM4; TermInterest = PANEL1TERM4INTEREST; break;
                    }
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    DisplayUserValues(i, PanelTerm, TermInterest, InterestCalculation);
                }
            }
            if (InvestmentCategory2RadioButton.Checked)
            {
                for (int i = 0; i < InvestmentMoreThanListBox.Items.Count; i++)
                {
                    switch (i)
                    {
                        case 0: PanelTerm = PANEL2TERM1; TermInterest = PANEL2TERM1INTEREST; break;
                        case 1: PanelTerm = PANEL2TERM2; TermInterest = PANEL2TERM2INTEREST; break;
                        case 2: PanelTerm = PANEL2TERM3; TermInterest = PANEL2TERM3INTEREST; break;
                        case 3: PanelTerm = PANEL2TERM4; TermInterest = PANEL2TERM4INTEREST; break;
                    }
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    DisplayUserValues(i, PanelTerm, TermInterest, InterestCalculation);
                }
            }
        }

        // Method to display user values in Summary Group Box on Click of Display Button
        private void DisplayUserValues(int TermIndex, int TermDuration, double TermInterest, double TermBalance)
        {
            InterestEarned = TermBalance - PrincipalInvestment;
            switch (TermIndex)
            {
                case 0:
                    Term1DurationTextBox.Text = TermDuration.ToString();
                    Term1InterestRateTextBox.Text = TermInterest.ToString();
                    Term1InterestEarnedTextBox.Text = InterestEarned.ToString("C");
                    DisplayDatetextBox.Text = Date;
                    Term1EndofTermBalanceTextBox.Text = TermBalance.ToString("C");
                    break;
                case 1:
                    Term2DurationTextBox.Text = TermDuration.ToString();
                    Term2InterestRateTextBox.Text = TermInterest.ToString();
                    Term2InterestEarnedTextBox.Text = InterestEarned.ToString("C");
                    DisplayDatetextBox.Text = Date;
                    Term2EndofTermBalanceTextBox.Text = TermBalance.ToString("C");
                    break;
                case 2:
                    Term3DurationTextBox.Text = TermDuration.ToString();
                    Term3InterestRateTextBox.Text = TermInterest.ToString();
                    Term3InterestEarnedTextBox.Text = InterestEarned.ToString("C");
                    DisplayDatetextBox.Text = Date;
                    Term3EndofTermBalanceTextBox.Text = TermBalance.ToString("C");
                    break;
                case 3:
                    Term4DurationTextBox.Text = TermDuration.ToString();
                    Term4InterestRateTextBox.Text = TermInterest.ToString();
                    Term4InterestEarnedTextBox.Text = InterestEarned.ToString("C");
                    DisplayDatetextBox.Text = Date;
                    Term4EndofTermBalanceTextBox.Text = TermBalance.ToString("C");
                    break;
            }
        }

        /* Actions Performed on Click of Proceed Button
        * Validation of User inputs such as emailid, customer name, telephone number and age 
        * Submit button enabled only when a particular term radio button is clicked and data validation takes place
        */
        private void ProceedButton_Click(object sender, EventArgs e)
        {
            DisplayButton.Enabled = false;
            SelectedInvestmentPlanGroupBox.Visible = true;
            SummaryGroupBox.Visible = false;
            PlanDetailsInvestmentAmountTextBox.Text = InvestmentAmountTextBox.Text;
            int InvestmentAmount = int.Parse(InvestmentAmountTextBox.Text);
            OutputInvestmentAmountTextBox.Text = InvestmentAmount.ToString("C0");
            double PrincipalAmount = double.Parse(InvestmentAmountTextBox.Text);
            int year = PanelTerm;
            SearchTransactionListBox.Enabled = false;


            if (string.IsNullOrWhiteSpace(CustomerNameTextBox.Text))
            {
                SummaryGroupBox.Visible = true;
                SelectedInvestmentPlanGroupBox.Visible = false;
                MessageBox.Show("Kindly Enter your Name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CustomerNameTextBox.Focus();
                return;
            }

            string EmailId = CustomerEmailIDTextBox.Text;
            if (!EmailId.Contains("@") && !EmailId.Contains(".com") || EmailId == "")
            {
                SummaryGroupBox.Visible = true;
                SelectedInvestmentPlanGroupBox.Visible = false;
                MessageBox.Show("Kindly Enter a valid Email Id", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CustomerEmailIDTextBox.Focus();
                return;
            }

            if (!int.TryParse(CustomerAgeTextBox.Text, out Age) || Age <= 0)
            {
                SummaryGroupBox.Visible = true;
                SelectedInvestmentPlanGroupBox.Visible = false;
                MessageBox.Show("Kindly Enter your Age", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CustomerAgeTextBox.Focus();
                return;
            }

            string PhoneNumber = DisplayTelephoneNumberTextBox.Text;
            if (PhoneNumber.Length < 10 || !int.TryParse(PhoneNumber, out _))
            {
                SummaryGroupBox.Visible = true;
                SelectedInvestmentPlanGroupBox.Visible = false;
                MessageBox.Show("Kindly Enter Valid Phone Number", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisplayTelephoneNumberTextBox.Focus();
                return;
            }

            if (InvestmentCategory1RadioButton.Checked)
            {
                if (Term1RadioButton.Checked)
                {
                    PanelTerm = PANEL1TERM1; TermInterest = PANEL1TERM1INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term1InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }

                if (Term2RadioButton.Checked)
                {
                    PanelTerm = PANEL1TERM2; TermInterest = PANEL1TERM2INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term2InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();

                }
                if (Term3RadioButton.Checked)
                {
                    PanelTerm = PANEL1TERM3; TermInterest = PANEL1TERM3INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term3InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }
                if (Term4RadioButton.Checked)
                {
                    PanelTerm = PANEL1TERM4; TermInterest = PANEL1TERM4INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term4InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }
            }

            if (InvestmentCategory2RadioButton.Checked)
            {
                if (Term1RadioButton.Checked)
                {
                    PanelTerm = PANEL2TERM1; TermInterest = PANEL2TERM1INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term1InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }
                if (Term2RadioButton.Checked)
                {
                    PanelTerm = PANEL2TERM2; TermInterest = PANEL2TERM2INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term2InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();

                }
                if (Term3RadioButton.Checked)
                {
                    PanelTerm = PANEL2TERM3; TermInterest = PANEL2TERM3INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term3InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }
                if (Term4RadioButton.Checked)
                {
                    PanelTerm = PANEL2TERM4; TermInterest = PANEL2TERM4INTEREST;
                    double InterestCalculation = CalculateInterest(PrincipalAmount, TermInterest, PanelTerm);
                    InterestEarned = InterestCalculation - InvestmentAmount;
                    NameTextBox.Text = CustomerNameTextBox.Text;
                    DateTextBox.Text = Date;
                    EmailIDTextBox.Text = CustomerEmailIDTextBox.Text;
                    AgeTextBox.Text = CustomerAgeTextBox.Text;
                    Term4InterestEarnedTextBox.Text = InterestEarned.ToString();
                    TelephoneNumberTextBox.Text = DisplayTelephoneNumberTextBox.Text;
                    PlanDetailTermDurationTextBox.Text = PanelTerm.ToString();
                    PlanDurationInterestRateTextBox.Text = TermInterest.ToString();
                    PlanDurationEndOfTermBalanceTextBox.Text = InterestCalculation.ToString();
                }
            }
            if (PrincipalInvestment > 1000000 && year >= 5)
            {
                BonusAmountLabel.Visible = true;
            }
            else
            {
                BonusAmountLabel.Visible = false;
            }
            TransactionNumberTextBox.Text = UniqueTransactionNumber().ToString();
            SubmitButton.Enabled = true;
        }

        /* Actions performed on click of submit button.
        * On click of submit button, TransactionDetails text file opens and the contents of the text boxes are written 
        * into the text file and the file is closed. This method aslo checks for unique transaction number generated.
        */
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            DialogResult Confirmation = MessageBox.Show("Do you wish to go ahead with the selected investment plan?", "Confirmation", MessageBoxButtons.YesNo);
            if (Confirmation == DialogResult.Yes)
            {
                int TransactionNumber = UniqueTransactionNumber();
                StreamWriter TransactionFile;
                try
                {
                    TransactionFile = File.AppendText("TransactionDetails.txt");
                    TransactionFile.WriteLine(TransactionNumberTextBox.Text);
                    TransactionFile.WriteLine(NameTextBox.Text);
                    TransactionFile.WriteLine(DateTextBox.Text);
                    TransactionFile.WriteLine(EmailIDTextBox.Text);
                    TransactionFile.WriteLine(AgeTextBox.Text);
                    TransactionFile.WriteLine(TelephoneNumberTextBox.Text);
                    TransactionFile.WriteLine(PlanDetailsInvestmentAmountTextBox.Text);
                    TransactionFile.WriteLine(PlanDetailTermDurationTextBox.Text);
                    TransactionFile.WriteLine(PlanDurationInterestRateTextBox.Text);
                    TransactionFile.WriteLine(PlanDurationEndOfTermBalanceTextBox.Text);
                    TransactionFile.Close();
                    DialogResult NewTransaction = MessageBox.Show("Transaction Completed Successfully." +
                        "\n" + "\n" + "Kindly end the current transaction to start a new transaction", "Information", MessageBoxButtons.OK);
                    SelectedInvestmentPlanGroupBox.Visible = false;
                    InvestmentAmountTextBox.Focus();
                    BonusLabel.Text = "The transaction has been completed successfully.\r\nTo start a new transaction, kindly end the current transaction";
                    BonusLabel.ForeColor = Color.Green;

                }
                catch
                {
                    MessageBox.Show("An error occurred while writing to the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to generate uniqie transaction number
        private int UniqueTransactionNumber()
        {
            Random TransactionID = new Random();
            int RandomNumber;

            StreamReader TransactionIDSearchFile;
            TransactionIDSearchFile = File.OpenText("TransactionDetails.Txt");

            RandomNumber = TransactionID.Next(1, 999999);

            // Checking if transaction ID already exixts by opening file and reading the inputs
            bool TransactionIdExists = false;
            string RandomNumberCheck = RandomNumber.ToString();
            while (!TransactionIDSearchFile.EndOfStream)
            {
                string Transaction_ID = TransactionIDSearchFile.ReadLine();
                for (int i = 1; i <= 8; i++)
                {
                    TransactionIDSearchFile.ReadLine();
                }
                // if random number generated is same as the one in file then return true and exit
                if (RandomNumberCheck == Transaction_ID)
                {
                    TransactionIdExists = true;
                    break;
                }
            }
            TransactionIDSearchFile.Close();
            // if random number exists return -1, else return random number
            if (TransactionIdExists)
            {
                return -1;
            }
            else
            {
                return RandomNumber;
            }
        }

        /* Actions performed on click of Summary Button
        * TransactionDetails text file is opened and the records are read in the streamreader and displayed in the ListBox
        * Summary Button is used to calculate the average Term and average investment details, Total Transaction, Total investments
        * Total Interest and total terms
        * */
        private void SummaryButton_Click(object sender, EventArgs e)
        {
            SearchTransactionListBox.Items.Clear();
            SummaryPanel.Visible = true;
            StreamReader SearchFile;
            SearchFile = File.OpenText("TransactionDetails.txt");

            int TotalTranasctionCount = 0;
            double TotalInterest = 0, TotalTerm = 0;
            double InvestedAmountTotal = 0;
            double AverageTerm = 0, AverageAmountInvested = 0;

            while (!SearchFile.EndOfStream)
            {
                string TransactionIDSearch = SearchFile.ReadLine();
                SearchFile.ReadLine();
                SearchFile.ReadLine();
                SearchFile.ReadLine();
                SearchFile.ReadLine();
                SearchFile.ReadLine();
                int InvestmentAmount = int.Parse(SearchFile.ReadLine());
                int TermSearch = int.Parse(SearchFile.ReadLine());
                SearchFile.ReadLine();
                double InterestSearch = double.Parse(SearchFile.ReadLine());

                SearchTransactionListBox.Items.Add("Transaction ID's: " + TransactionIDSearch + "\n");

                InvestedAmountTotal += InvestmentAmount;
                TotalTerm += TermSearch;
                TotalInterest += InterestSearch;
                TotalTranasctionCount++;
            }
            AverageTerm = Math.Round((TotalTerm * 12 / TotalTranasctionCount), 2);
            AverageAmountInvested = Math.Round((InvestedAmountTotal / TotalTranasctionCount), 2);

            SummaryTotalTransactionTextBox.Text = TotalTranasctionCount.ToString();
            SummaryTotalAmountInvestedTextBox.Text = InvestedAmountTotal.ToString();
            SummaryTotalInterestTextBox.Text = TotalInterest.ToString();
            SummaryTotalTermTextBox.Text = TotalTerm.ToString();
            SummaryAverageTermTextBox.Text = AverageTerm.ToString();
            SummaryAverageInvestmentTextBox.Text = AverageAmountInvested.ToString();
            SelectedInvestmentPlanGroupBox.Visible = false;
            SearchTransactionListBox.Visible = true;
            SearchFile.Close();
        }

        /* Actions performed on click of Search Button
        * TransactionDetails text file is opened and the records are read in the streamreader and displayed in the ListBox
        * based on the search term the details are displayed in the listbox
        * if any error arises, error message is displayed
        */
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string TransactionSearch = TransactionNumberSearchTextBox.Text;
            string NameSearch = NameSearchTextBox.Text;
            string DateSearch = DateSearchTextBox.Text;
            string EmailIdSearch = EmailIDSearchTextBox.Text;

            bool RecordsFound = false;
            if (TransactionSearch != "" || NameSearch != "" || DateSearch != "" || EmailIdSearch != "")
            {
                SearchTransactionListBox.Items.Clear();
                StreamReader SearchFile;
                SearchFile = File.OpenText("TransactionDetails.txt");

                while (!SearchFile.EndOfStream)
                {
                    string SearchByTransactionID = SearchFile.ReadLine();
                    string SearchByName = SearchFile.ReadLine();
                    string SearchByDate = SearchFile.ReadLine();
                    string SearchByEmailID = SearchFile.ReadLine();
                    string SearchByAge = SearchFile.ReadLine();
                    string SearchByTelephoneNumber = SearchFile.ReadLine();
                    string SearchByInvestment = SearchFile.ReadLine();
                    string SearchByTerm = SearchFile.ReadLine();
                    string SearchByInterest = SearchFile.ReadLine();
                    string SearchByBalance = SearchFile.ReadLine();

                    if (TransactionSearch != "")
                    {
                        if (SearchByTransactionID.Equals(TransactionSearch))
                        {
                            SearchTransactionListBox.Items.Add(SearchDisplay(SearchByTransactionID, SearchByName, SearchByDate,
                            SearchByEmailID, SearchByAge, SearchByTelephoneNumber, SearchByInvestment, SearchByTerm,
                            SearchByInterest, SearchByBalance));
                            RecordsFound = true;
                        }
                        else
                        {
                            MessageBox.Show("Transactions for the Transaction ID entered doesnt exist, " + "\n" +
                                "Kindly Check the Transaction ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchTransactionListBox.Visible = false;
                        }
                    }

                    else if (NameSearch != "")
                    {
                        if (SearchByName.Equals(NameSearch))
                        {
                            SearchTransactionListBox.Items.Add(SearchDisplay(SearchByTransactionID, SearchByName, SearchByDate,
                            SearchByEmailID, SearchByAge, SearchByTelephoneNumber, SearchByInvestment, SearchByTerm,
                            SearchByInterest, SearchByBalance));
                            RecordsFound = true;
                        }
                        else
                        {
                            MessageBox.Show("Transactions for the Name entered doesnt exist, " + "\n" +
                                "Kindly Check the Name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchTransactionListBox.Visible = false;
                        }
                    }

                    else if (DateSearch != "")
                    {
                        if (SearchByDate.Equals(DateSearch))
                        {
                            SearchTransactionListBox.Items.Add(SearchDisplay(SearchByTransactionID, SearchByName, SearchByDate,
                            SearchByEmailID, SearchByAge, SearchByTelephoneNumber, SearchByInvestment, SearchByTerm,
                            SearchByInterest, SearchByBalance));
                            RecordsFound = true;
                        }

                        else
                        {
                            MessageBox.Show("Transaction for the Date entered doesnt exist, " + "\n" +
                                "Kindly Check the Date", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchTransactionListBox.Visible = false;
                        }
                    }

                    else
                    {
                        if (SearchByEmailID.Equals(EmailIdSearch))
                        {
                            SearchTransactionListBox.Items.Add(SearchDisplay(SearchByTransactionID, SearchByName, SearchByDate,
                            SearchByEmailID, SearchByAge, SearchByTelephoneNumber, SearchByInvestment, SearchByTerm,
                            SearchByInterest, SearchByBalance));
                            RecordsFound = true;
                        }

                        else
                        {
                            MessageBox.Show("Transactions for the Email ID entered doesnt exist, " + "\n" +
                                "Kindly Check the Email ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            SearchTransactionListBox.Visible = false;
                        }
                    }
                }
                SearchFile.Close();
                if(!RecordsFound)
                {
                    SearchTransactionListBox.Visible=false;
                }
                else
                {
                    SearchTransactionListBox.Visible = true;
                }
                SelectedInvestmentPlanGroupBox.Visible = false;
            }
            else
            {
                MessageBox.Show("An error occurred while searching, Kindly select atleast one search option", 
                    "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Method to display the records in listbox when search button is clicked
         * This method is called when the search record is found on click of search button
        */
        private string SearchDisplay(string SearchByTransactionID, string SearchByName, string SearchByDate,
            string SearchByEmailID, string SearchByAge,
            string SearchByTelephoneNumber, string SearchByInvestment, string SearchByTerm,
            string SearchByInterest, string SearchByBalance)
        {
            return
                "Transaction ID: " + SearchByTransactionID + " " +
                "Customer Name: " + SearchByName + " " +
                "Transaction Date: " + SearchByDate + " " +
                "Customer EmailID: " + SearchByEmailID + " " +
                "Customer Age: " + SearchByAge + " " +
                "Customer Telephone Number: " + SearchByTelephoneNumber + " " +
                "Investment Amount " + SearchByInvestment + " " +
                "Investment Term: " + SearchByTerm + " " +
                "Interest Earned: " + SearchByInterest + " " +
                "End of Term Balance: " + SearchByBalance + "\n";
        }

        // Action performed on click of exit button - application is closed
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Actions performed on InvestmentCategory1RadioButton checked changed property
        private void InvestmentCategory1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (InvestmentCategory1RadioButton.Checked)
            {
                DisplayButton.Enabled = true;
                InvestmentUptoListBox.Enabled = false;
                InvestmentMoreThanListBox.Enabled = false;
            }
        }

        // Actions performed on InvestmentCategory2RadioButton checked changed property
        private void InvestmentCategory2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (InvestmentCategory2RadioButton.Checked)
            {
                DisplayButton.Enabled = true;
                InvestmentMoreThanListBox.Enabled = false;
                InvestmentUptoListBox.Enabled = false;
            }
        }

        // Actions performed on InvestmentAmount TextBox Text changed property
        private void InvestmentAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            InvestmentCategory1RadioButton.Enabled = true;
            if (InvestmentAmountTextBox.Text != "")
            {
                DisplayButton.Enabled = true;
            }
            if (string.IsNullOrWhiteSpace(InvestmentAmountTextBox.Text))
            {
                InvestmentCategory1RadioButton.Enabled = false;
            }
            if (int.TryParse(InvestmentAmountTextBox.Text, out int InvestmentAmount))
            {
                if (InvestmentAmount > 100000)
                {
                    InvestmentCategory2RadioButton.Enabled = true;
                    InvestmentCategory2RadioButton.Checked = true;
                    InvestmentCategory1RadioButton.Enabled = false;
                }
                else
                {
                    InvestmentCategory2RadioButton.Enabled = false;
                    InvestmentCategory1RadioButton.Checked = true;
                }
            }

        }

        /* Actions performed on click of end button
        * This performs the clear operation and removes existing values in textboxes and ends the current transaction
        */
        private void EndButton_Click(object sender, EventArgs e)
        {
            BonusLabel.Text = "Investments more than 1 Million for 5 Years  or more\r\nreceive an additional bonus of 25000 on term completion\r\n";
            BonusLabel.ForeColor = Color.Red;
            DisableSearchRadioButton.Checked = true;
            ProceedButton.Enabled = false;
            DisplayButton.Enabled = false;
            SubmitButton.Enabled = false;
            InvestmentAmountTextBox.Text = "";
            Term1InterestEarnedTextBox.Text = Term1DurationTextBox.Text = Term1EndofTermBalanceTextBox.Text = "";
            Term2InterestEarnedTextBox.Text = Term2DurationTextBox.Text = Term2EndofTermBalanceTextBox.Text = "";
            Term3InterestEarnedTextBox.Text = Term3DurationTextBox.Text = Term3EndofTermBalanceTextBox.Text = "";
            OutputInvestmentAmountTextBox.Text = CustomerNameTextBox.Text = CustomerEmailIDTextBox.Text = CustomerAgeTextBox.Text =
                DisplayTelephoneNumberTextBox.Text = DisplayDatetextBox.Text = "";
            InvestmentAmountTextBox.Focus();
            SummaryGroupBox.Visible = false;
            Term1RadioButton.Checked = Term2RadioButton.Checked = Term3RadioButton.Checked = Term4RadioButton.Checked = false;
            Term1DetailsPanel.Enabled = Term2DetailsPanel.Enabled = Term3DetailsPanel.Enabled = Term4DetailsPanel.Enabled = false;
            InvestmentCategory1RadioButton.Checked = false;
            InvestmentCategory2RadioButton.Checked = false;
            SelectedInvestmentPlanGroupBox.Visible = false;
            InvestmentCategory1RadioButton.Enabled = false;
            InvestmentCategory2RadioButton.Enabled = false;
            SearchTransactionListBox.Visible = false;
            SummaryPanel.Visible = false;
        }

        // Actions performed on Term1RadioButton mouseclick property
        private void Term1RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Term1DetailsPanel.Enabled = true;
            Term2DetailsPanel.Enabled = false; Term3DetailsPanel.Enabled = false; Term4DetailsPanel.Enabled = false;

        }

        // Actions performed on Term2RadioButton mouseclick property
        private void Term2RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Term2DetailsPanel.Enabled = true;
            Term1DetailsPanel.Enabled = false; Term3DetailsPanel.Enabled = false; Term4DetailsPanel.Enabled = false;
        }

        // Actions performed on Term3RadioButton mouseclick property
        private void Term3RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Term3DetailsPanel.Enabled = true;
            Term1DetailsPanel.Enabled = false; Term2DetailsPanel.Enabled = false; Term4DetailsPanel.Enabled = false;
        }

        // Actions performed on Term4RadioButton mouseclick property
        private void Term4RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Term4DetailsPanel.Enabled = true;
            Term1DetailsPanel.Enabled = false; Term2DetailsPanel.Enabled = false; Term3DetailsPanel.Enabled = false;
        }

        // Actions performed on Term1RadioButton checked changed property
        private void Term1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Term1RadioButton.Checked)
            {
                ProceedButton.Enabled = true;
            }
        }

        // Actions performed on Term2RadioButton checked changed property
        private void Term2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Term2RadioButton.Checked)
            {
                ProceedButton.Enabled = true;
            }
        }

        // Actions performed on Term3RadioButton checked changed property
        private void Term3RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Term3RadioButton.Checked)
            {
                ProceedButton.Enabled = true;
            }
        }

        // Actions performed on Term4RadioButton checked changed property
        private void Term4RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Term4RadioButton.Checked)
            {
                ProceedButton.Enabled = true;
            }
        }

        /* Actions performed on TransactionNumberSearchRadioButton checked changed property
         * takes care of enabling textbox for transaction number search during search operation
        */
        private void TransactionNumberSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TransactionNumberSearchRadioButton.Checked)
            {
                TransactionNumberSearchTextBox.Enabled = true;
            }
            else
            {
                TransactionNumberSearchTextBox.Enabled = false;
            }
        }

        /* Actions performed on NameSearchRadioButton checked changed property
         * takes care of enabling textbox for name search during search operation
        */
        private void NameSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NameSearchRadioButton.Checked)
            {
                NameSearchTextBox.Enabled = true;
            }
            else
            {
                NameSearchTextBox.Enabled = false;
            }
        }

        /* Actions performed on DateSearchRadioButton checked changed property
         * takes care of enabling textbox for date search during search operation
        */
        private void DateSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DateSearchRadioButton.Checked)
            {
                DateSearchTextBox.Enabled = true;
            }
            else
            {
                DateSearchTextBox.Enabled = false;
            }
        }

        /* Actions performed on EmailSearchRadioButton checked changed property
         * takes care of enabling textbox for email search during search operation
        */
        private void EmailIDSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (EmailIDSearchRadioButton.Checked)
            {
                EmailIDSearchTextBox.Enabled = true;
            }
            else
            {
                EmailIDSearchTextBox.Enabled = false;
            }
        }

        /* Actions performed on enable of search button.
         * By defualt the search group box is disabled and is enabled only on click of enable search radio button
         */
        private void EnableSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSearchRadioButton.Checked)
            {
                DisplayButton.Enabled = false;
                InvestmentAmountTextBox.Enabled = false;
                SelectedInvestmentPlanGroupBox.Visible = false;
                SearchButton.Enabled = true;
                SearchEngineGroupBox.Enabled = true;
            }
            else { SearchEngineGroupBox.Enabled = false; }
        }

        /* Actions performed on enable of search button.
         * Default search radio Button disables the search option and users can now either 
         * close the application or start a new transaction
         */
        private void DisableSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DisableSearchRadioButton.Checked)
            {
                InvestmentAmountTextBox.Enabled = true;
                InvestmentAmountTextBox.Focus();
                InvestmentAmountTextBox.Text = "";
                CustomerNameTextBox.Text = CustomerAgeTextBox.Text = CustomerEmailIDTextBox.Text = TelephoneNumberTextBox.Text = "";
                DisplayButton.Enabled = true;
                SubmitButton.Enabled = false;
                ProceedButton.Enabled = false;
                SearchButton.Enabled = false;
                SearchTransactionListBox.Visible = false;
                SearchEngineGroupBox.Enabled = false;
                TransactionNumberSearchRadioButton.Checked = NameSearchRadioButton.Checked = DateSearchRadioButton.Checked =
                    EmailIDSearchRadioButton.Checked = false;
                TransactionNumberSearchTextBox.Text = NameSearchTextBox.Text = DateSearchTextBox.Text = EmailIDSearchTextBox.Text = "";
            }
            else
            { SearchEngineGroupBox.Enabled = true; }
        }
    }

}
