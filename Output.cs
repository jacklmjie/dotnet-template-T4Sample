using System.IO;
using System.Threading.Tasks;

namespace T4Sample
{
    public interface IOutput
    {
        Task Output(string context, string fileName, Output output);
    }

    public class FileOutput : IOutput
    {
        public async Task Output(string context, string fileName, Output output)
        {
            var filePath = Path.Combine(output.Path, fileName + output.Extension);
            var fileExists = File.Exists(filePath);
            if (fileExists)
            {
                switch (output.Mode)
                {
                    case CreateMode.None:
                    case CreateMode.Incre:
                        {
                            break;
                        }
                    case CreateMode.Full:
                        {
                            File.Delete(output.Path);
                            break;
                        }
                }
            }
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                await streamWriter.WriteAsync(context);
            }
        }
    }

    public class Output
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件创建模式
        /// </summary>
        public CreateMode Mode { get; set; } = CreateMode.None;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; set; }
    }

    /// <summary>
    /// 文件创建模式
    /// </summary>
    public enum CreateMode
    {
        None = 0,
        /// <summary>
        /// 增量创建，如果存在则忽略
        /// </summary>
        Incre = 1,
        /// <summary>
        /// 全量创建，如果存在则重新创建
        /// </summary>
        Full = 2
    }
}
