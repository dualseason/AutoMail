namespace AutoMail.Models.ViewModels
{
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class BaseResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
