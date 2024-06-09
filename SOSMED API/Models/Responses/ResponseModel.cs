using SOSMED_API.Models.Commons;

namespace SOSMED_API.Models.Responses
{
    public class ResponseModel
    {
        public class LoginResponse : ResponseBaseModel
        {
            public UserLoginDataModel UserData { get; set; }
        }

        public class FormDataResponse : ResponseBaseModel
        {
            public List<FormModel> Content { get; set; }
        }

        public class GroupDataResponse : ResponseBaseModel
        {
            public List<GroupModel> Content { get; set; }
        }

        public class GroupAccessDataResponse : ResponseBaseModel
        {
            public List<GroupAccessModel> Content { get; set; }
        }

        public class PostingDataResponse : ResponseBaseModel
        {
            public List<PostingModel> Content { get; set; }
        }

        public class PostLimitDataResponse : ResponseBaseModel
        {
            public List<PostLimitModel> Content { get; set; }
        }

        public class UserDataResponse : ResponseBaseModel
        {
            public List<UserModel> Content { get; set; }
        }
    }
}
