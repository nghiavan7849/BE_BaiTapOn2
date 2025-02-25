namespace BE_BTO2_Demo.DTOs.Response
{
    public class ApiResponse<T>
    {
        public int? Code { get; set; } // 0: Success, 1: Error
        public string? Status { get; set; } // "success" hoặc "error"
        public T? Data { get; set; } 
        public string? Description { get; set; }
        public int? Total { get; set; }
        public int? PageTotal { get; set; } 
        public int? PageSize { get; set; } 
        public int? PageCurrent { get; set; }
         

        public static ApiResponse<T> Success(T? data = default, string? description = null, int? pageTotal = null, int? pageSize = null, int? pageCurrent = null, int? total = null)
        {
            return new ApiResponse<T>
            {
                Code = 0,
                Status = "success",
                Data = data,
                Description = description,
                PageTotal = pageTotal,
                PageSize = pageSize,
                PageCurrent = pageCurrent,
                Total = total
            };
        }

        public static ApiResponse<T> Error(string description)
        {
            return new ApiResponse<T>
            {
                Code = 1,
                Status = "error",
                Data = default, 
                Description = description
            };
        }
        public static ApiResponse<T> NotFound(string description)
        {
            return new ApiResponse<T>
            {
                Code = 2,
                Status = "error",
                Data = default,
                Description = description
            };
        }

        public static ApiResponse<T> Forbidden()
        {
            return new ApiResponse<T>
            {
                Code = 3,
                Status = "error",
                Data = default,
                Description = "Không có quyền truy cập"
            };
        }

    }
}
