using System;
using System.IO;
using System.Linq;

namespace CopyChildrenToTopOutPutFolder_Fx
{
    class Program
    {
        static void Main(string[] args)
        {
            var ignoreFile = new[]
            {
                ".git",
                ".gitkeep",
                "CopyChildrenToTopOutPutFolder_Fx.exe",
                "CopyChildrenToTopOutPutFolder_Fx.exe.config",
                "CopyChildrenToTopOutPutFolder_Fx.pdb"
            };

            try
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;

                var outPutPath = Path.Combine(basePath, "OutPut");

                if (Directory.Exists(outPutPath))
                    Directory.Delete(outPutPath, true);

                Directory.CreateDirectory(outPutPath);

                var files = Directory.GetFiles(basePath, "*", SearchOption.AllDirectories);

                int count = 0;

                foreach (var item in files)
                {
                    if (ignoreFile.Any(t => item.Contains(t)))
                        continue;

                    var newPath = Path.Combine(outPutPath, Path.GetFileName(item));

                    if (File.Exists(newPath))
                        continue;

                    File.Copy(item, newPath);

                    count++;

                    Console.WriteLine($"[{count}] Copy {item} -> {newPath}\n");
                }

                Console.WriteLine($"finished! {count} files copied.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("execute error!\n");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("press any key to exit ...\n");
            Console.ReadKey();
        }
    }
}
