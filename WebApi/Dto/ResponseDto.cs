namespace WebApi.Dto
{
    /// <summary>
    /// Wrapper object for all responses returned by the API that require to return data.
    /// </summary>
    /// <typeparam name="T">the type of optional object that is returned with the response.</typeparam>
    public class ResponseDto<T> : SimpleResponseDto
    {
        /// <summary>
        /// Optional object that is returned with the response.
        /// </summary>
        public T Dto { get; set; }

        /// <summary>
        /// Instantiates a new Response dto.
        /// </summary>
        public ResponseDto()
        {

        }

        /// <summary>
        /// Instantiates a new Response dto.
        /// </summary>
        /// <param name="dto">The object that is returned with the response.</param>
        public ResponseDto(T dto)
        {
            Dto = dto;
        }
    }
}
