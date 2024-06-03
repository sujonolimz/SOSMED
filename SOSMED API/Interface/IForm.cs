using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IForm
    {
        List<FormModel> GetFormData();
        bool InsertFormData(FormModel _formModel);
        bool UpdateFormData(FormModel _formModel);
        bool DeleteFormData(string formID);
    }
}
