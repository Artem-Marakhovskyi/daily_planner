﻿namespace DailyPlanner.Api.ViewDtos
{
    public class ParameterErrorDto : ErrorDto
    {
        public ParameterErrorDto(params string[] parameterNames)
        {
            Message = $"Invalid parameter ({string.Join(",", parameterNames)}) value(s)";
        }
    }
}
