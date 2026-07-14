using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Application.Interfaces.Services
{
    public interface IAIAssistantSevices
    {
        Task<ApiResponse<AIResponseDto>> AskQuestionAsync(string question);
    }
}
