

using System.IO;
using System.Threading;
using System.Diagnostics;

namespace globa

{
    class Program

    {
        static void Main()
        {
            BuildProject buildProject = new BuildProject();
            Project project = new Project(buildProject);
            project.Start();
        }
    }

    public class Project
    {
        static String projectName;
        static String projectStack;
        static readonly String[] stacks = ["Javascript", "Typescript"];
        BuildProject builder;


        public Project(BuildProject builder)
        {
            this.builder = builder;
        }
        public void Start()
        {


            PickProjectName();
            PickProjectStack();
            ResumeProjecToUser();

        }

        static void PickProjectName()
        {
            Console.WriteLine("Choose a new project name :");
            projectName = Console.ReadLine();
        }
        static void PickProjectStack()
        {

            Console.WriteLine("Select the langue of the project: ");
            for (int i = 0; i < stacks.Length; i++)
            {
                Console.WriteLine(i + 1 + " - " + stacks[i]);

            }
            string projectSelectIndex = Console.ReadLine();
            int parsedIndexProjectSelected = int.Parse(projectSelectIndex);
            projectStack = stacks[parsedIndexProjectSelected - 1];

        }
        void ResumeProjecToUser()
        {
            Console.WriteLine("Project name: " + projectName);
            Console.WriteLine("Stack : " + projectStack);
            Console.WriteLine("Creating project...");
            this.builder.CreateFolder(projectName, projectStack);
            Console.WriteLine("Creating project...");

        }

    }

    public class BuildProject

    {


        public void CreateFolder(string projectName, string ProjectStack)

        {
            string sourceFolder = @"C:\\Users\\Antôno\\Documents\\" + projectName;
             
            NewFile[] files = [new NewFile("\\package.json",sourceFolder, "{\r\n  \"name\": \"my-express-app\",\r\n  \"version\": \"1.0.0\",\r\n  \"description\": \"A basic Express.js application\",\r\n  \"main\": \"app.js\",\r\n  \"scripts\": {\r\n    \"start\": \"node app.js\",\r\n    \"dev\": \"nodemon app.js\",\r\n    \"lint\": \"eslint .\"\r\n  },\r\n  \"dependencies\": {\r\n    \"express\": \"^4.17.1\",\r\n    \"nodemon\": \"^2.0.15\",\r\n    \"eslint\": \"^8.2.0\"\r\n  }\r\n}\r\n"),
             new NewFile("\\index.js",sourceFolder, "// Importing required modules\r\nconst express = require('express');\r\n\r\n// Creating an instance of Express\r\nconst app = express();\r\n\r\n// Define a route\r\napp.get('/', (req, res) => {\r\n  res.send('Hello, World!');\r\n});\r\n\r\n// Start the server\r\nconst PORT = process.env.PORT || 3000;\r\napp.listen(PORT, () => {\r\n  console.log(`Server is running on port ${PORT}`);\r\n})")];

            Console.WriteLine(sourceFolder);

            try
            {
                Directory.CreateDirectory(sourceFolder);
                string codeCommand = GetCodeCommand();

                Process.Start(@"C:\Users\Antôno\AppData\Local\Programs\Microsoft VS Code\Code.exe", sourceFolder);

                for (int i = 0;i < files.Length;i++)
                {
                    CreateFile(files[i]);
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }

        }
        static string GetCodeCommand()
        {
            // Determine the correct 'code' command based on the operating system
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return "code.cmd";
            }
            else
            {
                return "code";
            }
        }
        public void CreateFile(NewFile NewFile)
        {

            try
            {

                string filePath = NewFile.path + NewFile.name;
                Console.WriteLine(filePath);
                using (FileStream fs = File.Create(filePath));
                WriteFile(filePath, NewFile.content); 
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void WriteFile(String filePath,String fileContent)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamWriter file = File.CreateText(filePath))
                    {
                        file.WriteLine(fileContent);
                        file.Close();
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + "here");
            }
        }
        
    }

    public class NewFile
    {
        public String name;
        public String path;
        public String content;

        public NewFile(String name,String path,String Content)
        {
            this.name = name;
            this.path = path;
            this.content = Content;


        }

    }


}







