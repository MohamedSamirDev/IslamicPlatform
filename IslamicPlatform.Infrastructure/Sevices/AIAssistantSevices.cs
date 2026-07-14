using IslamicPlatform.Application.Common;
using IslamicPlatform.Application.DTOs.AI;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Sevices
{
    public class AIAssistantSevices : IAIAssistantSevices
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;

        public AIAssistantSevices
            (IConfiguration configuration,
            HttpClient httpClient,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<AIResponseDto>> AskQuestionAsync(string question)
        {
            // Step 1 — دور في الـ DB الأول
            var dbContext = await SearchDatabaseAsync(question);

            // Step 2 — جهز الـ Prompt للـ AI
            var prompt = BuildPrompt(question, dbContext);

            // Step 3 — اسأل Claude
            var aiAnswer = await AskGroqAsync(prompt);

            if (aiAnswer == null)
                return ApiResponse<AIResponseDto>.Fail("حدث خطأ في الاتصال بالمساعد الذكي");

            return ApiResponse<AIResponseDto>.Ok(new AIResponseDto
            {
                Answer = aiAnswer,
                IsFromDatabase = dbContext.HasResults,
                RelatedAyahs = dbContext.RelatedAyahs,
                RelatedHadiths = dbContext.RelatedHadiths,
                IsKnown = !aiAnswer.Contains("لا أعلم") && !aiAnswer.Contains("لا أستطيع")
            });
        }

        //Step 1 =>Search in DB
        private async Task<DatabaseContext> SearchDatabaseAsync(string question)
        {
            var content = new DatabaseContext();

            var ayahs = await _unitOfWork.Ayahs.SearchAsync(question);
            var ayahList = ayahs.Take(3).ToList();

            if (ayahList.Any())
            {
                content.HasResults = true;
                content.RelatedAyahs = ayahList.Select(a =>
                $"{a.TextArabic}: {a.NumberInSurah} اية- {a.SurahId}سورة رقم").ToList();

            }

            var hadiths = await _unitOfWork.Hadiths.SearchAsync(question);
            var hadithList = hadiths.Take(3).ToList();
            if (hadithList.Any())
            {
                content.HasResults = true;
                content.RelatedHadiths = hadithList.Select(h =>
                $"{h.Narrator}:الراوي-{h.TextArabic}:{h.Number}:حديث رقم ").ToList();
            }

            return content;
        }
        //part 2=>Bulid prompt  
        private string BuildPrompt(string question, DatabaseContext dbContext)
        {
            var prompt = new StringBuilder();

            prompt.AppendLine("أنت مساعد إسلامي متخصص. التزم بالقواعد التالية بدقة تامة:");
            prompt.AppendLine("1. أجب باللغة العربية الفصحى فقط، ومهما كان السؤال لا تستخدم أي لغة أخرى.");
            prompt.AppendLine("2. اعتمد فقط على القرآن الكريم والسنة النبوية الصحيحة.");
            prompt.AppendLine("3. إذا لم تكن متأكداً، قل: 'لا أعلم، يرجى سؤال عالم متخصص'.");
            prompt.AppendLine("4. لا تُفتِ في المسائل الخلافية الكبيرة.");
            prompt.AppendLine("5. اذكر المصدر دائماً (اسم السورة والآية، أو اسم كتاب الحديث).");
            prompt.AppendLine("6. لا تتحدث في أي موضوع خارج نطاق الإسلام.");
            prompt.AppendLine("7. إذا سُئلت عن شيء غير إسلامي، اعتذر بأدب وأعد توجيه المحادثة.");
            prompt.AppendLine("8. اكتب الإجابة من اليمين لليسار بترتيب منطقي.");
            // لو لقينا حاجة في الـ DB نبعتها كـ Context
            if (dbContext.HasResults)
            {
                prompt.AppendLine("وجدت في قاعدة البيانات المعلومات التالية ذات الصلة:");

                if (dbContext.RelatedAyahs.Any())
                {
                    prompt.AppendLine("آيات قرآنية ذات صلة:");
                    foreach (var ayah in dbContext.RelatedAyahs)
                        prompt.AppendLine($"- {ayah}");
                }

                if (dbContext.RelatedHadiths.Any())
                {
                    prompt.AppendLine("أحاديث نبوية ذات صلة:");
                    foreach (var hadith in dbContext.RelatedHadiths)
                        prompt.AppendLine($"- {hadith}");
                }

                prompt.AppendLine("استخدم هذه المعلومات في إجابتك إن كانت مناسبة.");
                prompt.AppendLine();
            }

            prompt.AppendLine($"السؤال: {question}");

            return prompt.ToString();
        }

       
        private async Task<string?> AskGroqAsync(string prompt)
        {
            try
            {
                var apiKey = _configuration["Groq:ApiKey"];
                var url = "https://api.groq.com/openai/v1/chat/completions";

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                new { role = "user", content = prompt }
            },
                    temperature = 0.3,
                    max_tokens = 1024
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Groq Error {response.StatusCode}: {err}");
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

                return result
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AI Error: " + ex.Message);
                return null;
            }
        }


        public class DatabaseContext
        {
            public bool HasResults { get; set; } = false;
            public List<string> RelatedAyahs { get; set; } = new();
            public List<string> RelatedHadiths { get; set; } = new();
        }

        public class ClaudeResponse
        {
            public List<ClaudeContent>? content { get; set; }
        }
        public class ClaudeContent
        {
            public string? Text { get; set; }
        }
    }

}