using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NbpCurrencyTracker.Services;

namespace NbpCurrencyTracker
{
    public partial class Form1 : Form
    {
        private readonly ExchangeRateService _exchangeRateService;

        public Form1()
        {
            InitializeComponent();
            _exchangeRateService = new ExchangeRateService();

            cmbMonth.Items.AddRange(new string[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });
            cmbMonth.SelectedIndex = 0;

            dgvRates.Columns.Add("Date", "Date");
            dgvRates.Columns.Add("Currency", "Currency");
            dgvRates.Columns.Add("Code", "Code");
            dgvRates.Columns.Add("Rate", "Rate");
        }

        private async void btnCurrent_Click(object sender, EventArgs e)
        {
            await LoadRatesAsync(isArchive: false);
        }

        private async void btnArchive_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Enter a valid year.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var month = cmbMonth.SelectedIndex + 1;
            await LoadRatesAsync(isArchive: true, year, month);
        }

        private async Task LoadRatesAsync(bool isArchive, int year = 0, int month = 0)
        {
            try
            {
                var rates = isArchive
                    ? await _exchangeRateService.GetArchiveRatesAsync(year, month)
                    : await _exchangeRateService.GetCurrentRatesAsync();

                dgvRates.Rows.Clear();

                foreach (var rate in rates)
                {
                    dgvRates.Rows.Add(rate.Date, rate.Currency, rate.Code, rate.Rate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
