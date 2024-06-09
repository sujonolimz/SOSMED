using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IForm
    {
        FormDataResponse GetFormData();
        ResponseBaseModel InsertFormData(FormModel _formModel);
        ResponseBaseModel UpdateFormData(FormModel _formModel);
        ResponseBaseModel DeleteFormData(string formID);
    }
}
