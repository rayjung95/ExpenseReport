using AKSoftware.WebApi.Client;
using StoreManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Shared.Services
{
    public class ReportsService
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public ReportsService(string url)
        {
            _baseUrl = url;
        }

        public string AccessToken
        {
            get => client.AccessToken;
            set
            {
                client.AccessToken = value;
            }
        }

        // ExpenseClaim Service
        public async Task<ReportsPagingResponse> GetAllReportsByPageAsync(string status = "", int pageNumber = 1, int pageSize = 20)
        {
            var response = await client.GetProtectedAsync<ReportsPagingResponse>($"{_baseUrl}/api/v1/ExpenseClaim?pageNumber={pageNumber}&pageSize={pageSize}&status={status}");
            return response.Result;
        }
        public async Task<ReportCreateResponse> PostReportAsync(CreateReportRequest model)
        {
            var response = await client.PostProtectedAsync<ReportCreateResponse>($"{_baseUrl}/api/v1/ExpenseClaim/Report", model);
            return response.Result;
        }
        public async Task<ReportCreateResponse> UpdateReportAsync(ExpenseClaim model, int Id)
        {
            var response = await client.PutProtectedAsync<ReportCreateResponse>($"{_baseUrl}/api/v1/ExpenseClaim/{Id}", model);
            return response.Result;
        }
        public async Task<GetReportByIdResponse> GetReportByIdAsync(int expenseClaimId)
        {
            var response = await client.GetProtectedAsync<GetReportByIdResponse>($"{_baseUrl}/api/v1/ExpenseClaim/{expenseClaimId}/Report");
            return response.Result;
        }
        public async Task<ReportUpdateResponse> UpdateReporStatusAsync(ExpenseClaim model, int Id)
        {
            var response = await client.PutProtectedAsync<ReportUpdateResponse>($"{_baseUrl}/api/v1/ExpenseClaim/{Id}/changeStatus", model);
            return response.Result;
        }
        public async Task<ReportCreateResponse> DeleteReportAsync(int Id)
        {
            var response = await client.DeleteProtectedAsync<ReportCreateResponse>($"{_baseUrl}/api/v1/ExpenseClaim/{Id}");
            return response.Result;
        }


        //Expense Category Service
        public async Task<ReportsPagingResponse> GetAllExpenseCategoryByPageAsync(int pageNumber = 1, int pageSize = 100)
        {
            var response = await client.GetProtectedAsync<ReportsPagingResponse>($"{_baseUrl}/api/v1/ExpenseCategory?pageNumber={pageNumber}&pageSize={pageSize}");
            return response.Result;
        }
    }
}
