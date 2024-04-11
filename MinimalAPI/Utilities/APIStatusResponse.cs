using MinimalAPI.Model;

namespace MinimalAPI.Utilities
{
    public static class APIStatusResponse
    {
        public static APIResponse GetCreateResponse(object data)
        {
            var response = new APIResponse
            {
                Data = data,
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.Created
            };

            return response;
        }

        public static APIResponse GetOkResponse(object data)
        {
            var response = new APIResponse
            {
                Data = data,
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            return response;
        }

        public static APIResponse GetBadResponse(params string[] errors)
        {
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            response.ErrorMessages.AddRange(errors);

            return response;
        }
    }
}
