namespace CreateInterfaceVSIX
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            string path = docView.FilePath;
            path = path.Replace(@"\", "/");
            string[] paths = path.Split('/');

            int pathLength = paths.Length;
            for (int i = 0; i < (pathLength - 1); i++)
            {
                if (i > 0)
                {
                    path = path + "/" + paths[i];
                }
                else
                {
                    path = paths[i];
                }
            }

            string projectName = paths[pathLength - 3];
            string selectedFileName = paths[pathLength - 1];
            path = path + "/I" + selectedFileName;

            int selectedFileLength = selectedFileName.Length;

            string fileName = "I" + selectedFileName.Substring(0, (selectedFileLength - 3));

            string[] contents =
            {
                "using System;",
                "using System.Collections.Generic;",
                "using System.Linq;",
                "using System.Text;",
                "using System.Threading.Tasks;",
                "namespace " + projectName,
                "{",
                "    public interface " + fileName,
                "    {",
                "    }",
                "}"
            };

            System.IO.File.AppendAllLines(path, contents);

        }
    }
}
