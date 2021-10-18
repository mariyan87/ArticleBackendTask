
namespace WebApi.Dto
{
    /// <summary>
    /// Wrapper object for all responses returned by the API.
    /// </summary>
    public class SimpleResponseDto
    {
        /// <summary>
        /// 200 for successful responses, 500 for unsuccessful
        /// </summary>
        public int Status { get; set; } = 200;
    }

}
