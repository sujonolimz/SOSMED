using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.SqlServer.Server;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using System.Data;

namespace SOSMED_API.Services
{
    public class FormService : IForm
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public FormService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<FormModel> GetFormData()
        {
            var datalist = new List<FormModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select FormID, FormDesc, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TForm";

                    datalist = con.Query<FormModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertFormData(FormModel _formModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"Insert into TForm (FormID, FormDesc, CreatedBy, CreatedDate)
                                values (@FormID, @FormDesc, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _formModel);

                    if (result > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }

        public bool UpdateFormData(FormModel _formModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TForm set FormDesc = @FormDesc, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where FormID = @FormID ";

                    var result = con.Execute(sql, _formModel);

                    if (result > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }

        public bool DeleteFormData(string formID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TForm where FormID = @FormID ";

                    var result = con.Execute(sql, new { FormID = formID });

                    if (result > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }
    }
}
