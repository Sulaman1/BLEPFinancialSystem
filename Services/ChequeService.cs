using Microsoft.EntityFrameworkCore;
using BLEPFinancialSystem.Data;
using BLEPFinancialSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BLEPFinancialSystem.Services
{
    public interface IChequeService
    {
        Task<Cheque> CreateChequeAsync(Cheque cheque);
        Task<Cheque> GetChequeAsync(int id);
        Task<List<Cheque>> GetChequesAsync();
        Task<Cheque> UpdateChequeAsync(Cheque cheque);
        Task<bool> DeleteChequeAsync(int id);
        Task<ChequeTemplate> CreateTemplateAsync(ChequeTemplate template);
        Task<ChequeTemplate> GetTemplateAsync(int id);
        Task<List<ChequeTemplate>> GetTemplatesAsync();
        Task<ChequeTemplate> UpdateTemplateAsync(ChequeTemplate template);
        Task<string> ConvertAmountToWords(decimal amount);
        Task<byte[]> GenerateChequePdf(Cheque cheque);
    }

    public class ChequeService : IChequeService
    {
        private readonly ApplicationDbContext _context;

        public ChequeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cheque> CreateChequeAsync(Cheque cheque)
        {
            // Generate cheque number if not provided
            if (string.IsNullOrEmpty(cheque.ChequeNumber))
            {
                cheque.ChequeNumber = await GenerateChequeNumberAsync(cheque.BankAccountId);
            }

            // Convert amount to words if not provided
            if (string.IsNullOrEmpty(cheque.AmountInWords))
            {
                cheque.AmountInWords = await ConvertAmountToWords(cheque.Amount);
            }

            _context.Cheques.Add(cheque);
            await _context.SaveChangesAsync();
            return cheque;
        }

        public async Task<Cheque> GetChequeAsync(int id)
        {
            return await _context.Cheques
                .Include(c => c.Template)
                .Include(c => c.BankAccount)
                .Include(c => c.Payment)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cheque>> GetChequesAsync()
        {
            return await _context.Cheques
                .Include(c => c.Template)
                .Include(c => c.BankAccount)
                .Include(c => c.Payment)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Cheque> UpdateChequeAsync(Cheque cheque)
        {
            var existingCheque = await _context.Cheques.FindAsync(cheque.Id);
            if (existingCheque == null)
                throw new ArgumentException("Cheque not found");

            _context.Entry(existingCheque).CurrentValues.SetValues(cheque);
            await _context.SaveChangesAsync();
            return existingCheque;
        }

        public async Task<bool> DeleteChequeAsync(int id)
        {
            var cheque = await _context.Cheques.FindAsync(id);
            if (cheque == null)
                return false;

            _context.Cheques.Remove(cheque);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ChequeTemplate> CreateTemplateAsync(ChequeTemplate template)
        {
            _context.ChequeTemplates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<ChequeTemplate> GetTemplateAsync(int id)
        {
            return await _context.ChequeTemplates.FindAsync(id);
        }

        public async Task<List<ChequeTemplate>> GetTemplatesAsync()
        {
            return await _context.ChequeTemplates.Where(t => t.IsActive).ToListAsync();
        }

        public async Task<ChequeTemplate> UpdateTemplateAsync(ChequeTemplate template)
        {
            var existingTemplate = await _context.ChequeTemplates.FindAsync(template.Id);
            if (existingTemplate == null)
                throw new ArgumentException("Template not found");

            _context.Entry(existingTemplate).CurrentValues.SetValues(template);
            await _context.SaveChangesAsync();
            return existingTemplate;
        }

        public async Task<string> ConvertAmountToWords(decimal amount)
        {
            return await Task.Run(() =>
            {
                long wholeAmount = (long)Math.Floor(amount);
                long decimalAmount = (long)Math.Round((amount - wholeAmount) * 100);

                string words = NumberToWords(wholeAmount) + " Rupees";

                if (decimalAmount > 0)
                {
                    words += " and " + NumberToWords(decimalAmount) + " Paise";
                }

                return words + " Only";
            });
        }

        private string NumberToWords(long number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        public async Task<byte[]> GenerateChequePdf(Cheque cheque)
        {
            // This would use a PDF generation library like iTextSharp or QuestPDF
            // For now, we'll return a placeholder
            await Task.Delay(100);
            return new byte[0];
        }

        private async Task<string> GenerateChequeNumberAsync(int bankAccountId)
        {
            var lastCheque = await _context.Cheques
                .Where(c => c.BankAccountId == bankAccountId)
                .OrderByDescending(c => c.ChequeNumber)
                .FirstOrDefaultAsync();

            if (lastCheque == null || string.IsNullOrEmpty(lastCheque.ChequeNumber))
            {
                return "000001";
            }

            if (int.TryParse(lastCheque.ChequeNumber, out int lastNumber))
            {
                return (lastNumber + 1).ToString("D6");
            }

            return "000001";
        }
    }
}