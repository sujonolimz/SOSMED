using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.SqlServer.Server;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using System.Data;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class FormService : IForm
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public FormService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public FormDataResponse GetFormData()
        {
            var datalist = new List<FormModel>();
            var response = new FormDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select FormID, FormDesc, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TForm order by FormID asc";

                    datalist = con.Query<FormModel>(sql).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get Form data error!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public ResponseBaseModel InsertFormData(FormModel _formModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is data exist
                    sql = @"select FormID from TForm where FormID = @FormID";
                    var existingData = con.QueryFirstOrDefault(sql, new { _formModel.FormID });

                    if (existingData != null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Data already exist with Form ID '{0}'!", _formModel.FormID);
                        return response;
                    }
                    
                    // Insert data to database
                    sql = @"Insert into TForm (FormID, FormDesc, CreatedBy, CreatedDate)
                                values (@FormID, @FormDesc, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _formModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data Form failed!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public ResponseBaseModel UpdateFormData(FormModel _formModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TForm set FormDesc = @FormDesc, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where FormID = @FormID ";

                    var result = con.Execute(sql, _formModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Update data Form failed!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public ResponseBaseModel DeleteFormData(string formID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TForm where FormID = @FormID ";

                    var result = con.Execute(sql, new { FormID = formID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data Form failed!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
    }
}
